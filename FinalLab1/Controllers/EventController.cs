using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using FinalLab1.Services.Redis;
using FinalLab1.Kafka;
using Newtonsoft.Json;

namespace FinalLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : GenericControllerBase<Event, int>
    {
        private readonly IGenericService<Event, int> _genericService;
        private readonly RedisQueueService _queueService;
        private readonly KafkaProducerService _kafkaProducerService;
        private readonly EventSearchService _searchService;

        public EventController(IGenericService<Event, int> service, RedisQueueService redisQueueService, KafkaProducerService kafkaProducerService, EventSearchService searchService) : base(service)
        {
            _genericService = service;
            _queueService = redisQueueService;
            _kafkaProducerService = kafkaProducerService;
            _searchService=searchService;
        }

        [HttpGet("enter-event/{id}")]
        public async Task<IActionResult> GetEventWithQueue([FromRoute] int id, int? userid)
        {
            var eventInfo = await _genericService.GetByIdAsync(id);
            if (eventInfo == null) return NotFound();

            var userId = userid ??
                 (int.TryParse(User.FindFirstValue(JwtRegisteredClaimNames.Sub), out var parsedUserId)
                     ? parsedUserId
                     : 0);
            if (userId == 0)
                return BadRequest("Invalid or missing user ID.");

            // Kiểm tra nếu user đã active rồi thì không cần xử lý lại
            if (await _queueService.IsUserActiveAsync(id, userId))
            {
                return Ok(new { status = "in", eventInfo }); // User đã có quyền rồi
            }

            var activeUserCount = await _queueService.GetActiveUserCountAsync(id);
            if (activeUserCount < 2)
            {
                // Cho phép truy cập và đánh dấu là active
                await _queueService.AddActiveUserAsync(id, userId);
                return Ok(new { status = "in", eventInfo });
            }
            else
            {
                await _queueService.RemoveFromQueueAsync(id, userId);

                // Thêm vào hàng đợi
                await _queueService.EnqueueAsync(id, userId);
                var message = JsonConvert.SerializeObject(new QueueMessage
                {
                    EventId = id,
                    UserId = userId
                });
                await _kafkaProducerService.SendMessageAsync("event_queue_topic", message);

                var position = await _queueService.GetUserPositionAsync(id, userId);

                return Ok(new { status = "waiting", position });
            }
        }

        // Deprecated: dùng SignalR thay thế

        [HttpPost("leave-event/{id}")]
        public async Task<IActionResult> LeaveEvent([FromRoute] int id, int? userid)
        {
            var userId = userid ??
         (int.TryParse(User.FindFirstValue(JwtRegisteredClaimNames.Sub), out var parsedUserId)
             ? parsedUserId
             : 0);
            if (userId == 0)
                return BadRequest("Invalid or missing user ID.");

            // Kiểm tra user có đang active không
            bool isActive = await _queueService.IsUserActiveAsync(id, userId);
            if (!isActive)
                return BadRequest("User is not currently active in this event.");

            // Remove khỏi danh sách active
            await _queueService.RemoveActiveUserAsync(id, userId);

            // Gửi message để kích hoạt xử lý người tiếp theo
            var message = JsonConvert.SerializeObject(new QueueMessage
            {
                EventId = id,
                UserId = 0 // Không quan trọng ở Kafka side
            });
            await _kafkaProducerService.SendMessageAsync("event_queue_topic", message);

            return Ok(new { status = "left" });
        }

        [HttpGet("event/{id}/active-users")]
        public async Task<IActionResult> GetActiveUsers([FromRoute] int id)
        {
            var activeUsers = await _queueService.GetActiveUsersAsync(id);
            var waitingUsers = await _queueService.GetWaitingUsersAsync(id);

            return Ok(new
            {
                eventId = id,
                activeUsers = activeUsers,
                waitingUsers = waitingUsers,
                activeCount = activeUsers.Count,
                waitingCount = waitingUsers.Count
            });
        }

        [HttpGet("event-sreach")]
        public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] string? type)
        {
            var results = await _searchService.SearchEventsAsync(keyword, type);
            return Ok(results);
        }
    }
}