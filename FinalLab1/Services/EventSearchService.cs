using FinalLab1.Data;
using FinalLab1.Dtos;
using FinalLab1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Nest;
using System.Text.Json;

namespace FinalLab1.Services
{
    public class EventSearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly LabDbContext _dbContext;
        private readonly IDistributedCache _cache;

        public EventSearchService(IElasticClient elasticClient, LabDbContext dbContext, IDistributedCache distributedCache)
        {
            _elasticClient=elasticClient;
            _dbContext=dbContext;
            _cache=distributedCache;
        }

        public async Task IndexEventAsync(Event ev)
        {
            var firstSession = ev.EventSessions?.FirstOrDefault();
            var firstLocation = firstSession?.Location;

            var doc = new EventElasticDocument
            {
                Id = ev.Id.ToString(),
                EventName = ev.Name,
                Information = ev.Information ?? "",
                OrganizerName = ev.Organizers?.FirstOrDefault()?.Name ?? "",

                LocationName = firstLocation?.Name ?? "",
                LocationAddress = firstLocation?.Address ?? "",
                LocationCity = firstLocation?.City ?? "",
                LocationCountry = firstLocation?.Country ?? "",

                CategoryNames = ev.Categories?.Select(c => c.Category.Name).ToList() ?? new List<string>(),
                FromTime = firstSession?.FromTime
            };

            await _elasticClient.IndexDocumentAsync(doc);
        }

        public async Task ReIndex()
        {
            var events = await _dbContext.Events
                 .Include(e => e.Organizers)
                 .Include(e => e.EventSessions).ThenInclude(es => es.Location)
                 .Include(e => e.Categories).ThenInclude(c => c.Category)
                 .Include(e => e.Organizer) // nếu bạn có `User Organizer`
                 .ToListAsync();

            foreach (var ev in events)
            {
                await IndexEventAsync(ev);
            }
        }

        public async Task<List<ResultEventSearchDto>> SearchEventsWithCacheAsync(string keyword)
        {
            var cacheKey = $"event_search_{keyword.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var eventIds = JsonSerializer.Deserialize<List<int>>(cachedData);
                return await GetEventsFromDb(eventIds);
            }

            // Nếu không có cache → search Elastic
            var response = await _elasticClient.SearchAsync<EventElasticDocument>(s => s
    .Query(q => q.MultiMatch(m => m
        .Fields(f => f
            .Field(p => p.EventName)
            .Field(p => p.Information)
            .Field(p => p.OrganizerName)
            .Field(p => p.CategoryNames)
            .Field(p => p.LocationName)
            .Field(p => p.LocationAddress)
            .Field(p => p.LocationCity)
            .Field(p => p.LocationCountry))
        .Query(keyword)))
    .Size(50)
);

            var eventIdsFromElastic = response.Documents.Select(d => int.Parse(d.Id)).ToList();

            // Cache lại Redis
            var jsonData = JsonSerializer.Serialize(eventIdsFromElastic);
            await _cache.SetStringAsync(cacheKey, jsonData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return await GetEventsFromDb(eventIdsFromElastic);
        }

        private async Task<List<ResultEventSearchDto>> GetEventsFromDb(List<int> eventIds)
        {
            var events = await _dbContext.Events
                .Include(e => e.Organizers)
                .Include(e => e.EventSessions).ThenInclude(es => es.Location)
                .Include(e => e.Categories).ThenInclude(c => c.Category)
                .Include(e => e.Organizer) // nếu bạn có `User Organizer`
                .Where(e => eventIds.Contains(e.Id))
                .ToListAsync();

            var results = events.Select(ev => new ResultEventSearchDto
            {
                Id = ev.Id,
                UserId = ev.UserId,
                Name = ev.Name,
                Information = ev.Information,
                LogoImage = ev.LogoImage,
                BannerImage = ev.BannerImage,
                MaxSeatsPerUser = ev.MaxSeatsPerUser,
                UserName = ev.Organizer?.Name ?? "", // giả sử Organizer là User

                EventSessions = ev.EventSessions?.Select(session => new EventSessionDto
                {
                    FromTime = session.FromTime,
                    ToTime = session.ToTime,
                    MaxSeat = session.MaxSeat,
                    Type = session.Type,
                    Location = new LocationDto
                    {
                        Name = session.Location?.Name ?? "",
                        Address = session.Location?.Address ?? "",
                        City = session.Location?.City ?? "",
                        Country = session.Location?.Country ?? ""
                    }
                }).ToList() ?? new(),

                Organizers = ev.Organizers?.Select(o => new OrganizerDto
                {
                    Name = o.Name,
                    Information = o.Information,
                    LogoImage = o.LogoImage
                }).ToList() ?? new(),

                CategoryNames = ev.Categories?.Select(c => c.Category.Name).ToList() ?? new()
            }).ToList();

            return results;
        }
    }
}