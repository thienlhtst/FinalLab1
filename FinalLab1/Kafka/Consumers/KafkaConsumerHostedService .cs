using Confluent.Kafka;
using FinalLab1.Hubs;
using FinalLab1.Services.Redis;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;

namespace FinalLab1.Kafka.Consumers
{
    public class KafkaConsumerHostedService : BackgroundService
    {
        private readonly EventQueueRedisService _queueService;
        private readonly ActiveEventRedisService _activequeueService;
        private readonly IHubContext<QueueHub> _hubContext;
        private readonly IConsumer<Null, string> _consumer;
        private readonly ILogger<KafkaConsumerHostedService> _logger;
        private const int MAX_ACTIVE_USERS = 2;

        public KafkaConsumerHostedService(
            EventQueueRedisService eventQueueRedisService, ActiveEventRedisService activequeueService,
            IConfiguration config,
            IHubContext<QueueHub> hubContext,
            ILogger<KafkaConsumerHostedService> logger)
        {
            _queueService = eventQueueRedisService;
            _activequeueService = activequeueService;
            _hubContext = hubContext;
            _logger = logger;

            var conf = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = "event-queue-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(conf).Build();
            _consumer.Subscribe("event_queue_topic");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () =>
            {
                _logger.LogInformation("Kafka consumer started.");

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = _consumer.Consume(stoppingToken);

                            var data = JsonConvert.DeserializeObject<QueueMessage>(cr.Message.Value);
                            if (data == null)
                                continue;

                            // Kiểm tra số lượng người đang truy cập
                            var activeCount = await _activequeueService.GetActiveUserCountAsync(data.EventId);

                            if (activeCount < MAX_ACTIVE_USERS)
                            {
                                // Lấy đúng người đầu tiên trong hàng đợi Redis (bất kể message gửi từ ai)
                                var nextUserId = await _queueService.DequeueAsync(data.EventId);

                                if (nextUserId.HasValue)
                                {
                                    await _activequeueService.AddActiveUserAsync(data.EventId, nextUserId.Value);

                                    await _hubContext.Clients.User(nextUserId.Value.ToString())
                                        .SendAsync("AccessGranted", data.EventId);

                                    _logger.LogInformation($"User {nextUserId.Value} granted access to event {data.EventId}");
                                }
                                else
                                {
                                    _logger.LogInformation($"Queue for event {data.EventId} is empty.");
                                }
                            }
                            else
                            {
                                _logger.LogInformation($"Event {data.EventId} already has {activeCount} active users.");
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            _logger.LogError($"Kafka consume error: {ex.Error.Reason}");
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Kafka processing error: {ex.Message}");
                        }

                        await Task.Delay(100, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Kafka consumer fatal error: {ex.Message}");
                }
                finally
                {
                    _consumer.Close();
                }
            });
        }
    }

    public class QueueMessage
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}