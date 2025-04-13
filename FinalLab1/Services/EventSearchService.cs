using FinalLab1.Data;
using FinalLab1.Dtos;
using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace FinalLab1.Services
{
    public class EventSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly LabDbContext _dbContext;

        public EventSearchService(IElasticClient elasticClient, LabDbContext dbContext)
        {
            _elasticClient=elasticClient;
            _dbContext=dbContext;
        }

        public async Task<List<Event>> SearchEventsAsync(string keyword, string? typeFilter = null)
        {
            // 1. Tìm ID từ Elasticsearch
            var response = await _elasticClient.SearchAsync<EventElasticDocument>(s => s
                .Index("events_index")
                .Size(50)
                .Query(q =>
                    q.Bool(b => b
                        .Must(
                            string.IsNullOrEmpty(keyword) ? null : new Func<QueryContainerDescriptor<EventElasticDocument>, QueryContainer>(m =>
                                m.MultiMatch(mm => mm
                                    .Fields(f => f
                                        .Field(p => p.EventName)
                                        .Field(p => p.OrganizerName)
                                        .Field(p => p.LocationName)
                                        .Field(p => p.CategoryNames)
                                    )
                                    .Query(keyword)
                                )
                            ),
                            string.IsNullOrEmpty(typeFilter) ? null : new Func<QueryContainerDescriptor<EventElasticDocument>, QueryContainer>(t =>
                                t.Term(term => term.Field(f => f.EventType).Value(typeFilter))
                            )
                        )
                    )
                )
            );

            var eventIds = response.Documents.Select(d => int.Parse(d.Id)).ToList();

            if (!eventIds.Any()) return new List<Event>();

            // 2. Truy vấn dữ liệu thật từ DB
            var events = await _dbContext.Events
                .Include(e => e.Organizers)
                .Include(e => e.Categories).ThenInclude(ec => ec.Category)
                .Include(e => e.EventSessions).ThenInclude(es => es.Location)
                .Where(e => eventIds.Contains(e.Id))
                .ToListAsync();

            // Optional: Giữ thứ tự theo Elasticsearch (nếu cần)
            events = events.OrderBy(e => eventIds.IndexOf(e.Id)).ToList();

            return events;
        }
    }
}