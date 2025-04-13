﻿using FinalLab1.Kafka;
using FinalLab1.Services.Redis;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FinalLab1.Hubs
{
    public class QueueHub : Hub
    {
        private readonly RedisQueueService _queueService;
        private readonly KafkaProducerService _kafkaProducer;
        private readonly IHubContext<QueueHub> _hubContext;

        public QueueHub(RedisQueueService queueService, KafkaProducerService kafkaProducer, IHubContext<QueueHub> hubContext)
        {
            _queueService=queueService;
            _kafkaProducer=kafkaProducer;
            _hubContext=hubContext;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"User {Context.UserIdentifier} connected");
            return base.OnConnectedAsync();
        }

        public async Task LeaveEvent(int eventId, int userId)
        {
            await _queueService.RemoveActiveUserAsync(eventId, userId);

            // Gọi kafka để xử lý người tiếp theo trong hàng chờ
            var message = JsonConvert.SerializeObject(new QueueMessage
            {
                EventId = eventId,
                UserId = userId
            });

            await _kafkaProducer.SendMessageAsync("event_queue_topic", message);

            // Cập nhật vị trí cho những user đang trong hàng chờ
            var waitingUsers = await _queueService.GetWaitingUsersAsync(eventId);
            for (int i = 0; i < waitingUsers.Count; i++)
            {
                var uid = waitingUsers[i];
                await _hubContext.Clients.User(uid.ToString())
                    .SendAsync("QueuePositionUpdated", new
                    {
                        EventId = eventId,
                        Position = i + 1
                    });
            }
        }
    }
}