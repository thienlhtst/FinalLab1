using Confluent.Kafka;
using FinalLab1.Hubs;
using FinalLab1.Services;
using FinalLab1.Services.Redis;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using static FinalLab1.Services.TicketService;

namespace FinalLab1.Kafka.Consumers
{
    public class TicketKafkaConsumerService : BackgroundService
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TicketKafkaConsumerService> _logger;
        private readonly IHubContext<QueueHub> _hubContext;
        private readonly string _topic = "ticket_request_topic";

        public TicketKafkaConsumerService(
            IConfiguration config,
            IServiceProvider serviceProvider,
            ILogger<TicketKafkaConsumerService> logger,
            IHubContext<QueueHub> hubContext)
        {
            var conf = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = "ticket-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(conf).Build();
            _serviceProvider = serviceProvider;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () =>
            {
                _consumer.Subscribe(_topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);
                        var ticketRequest = JsonConvert.DeserializeObject<TicketRequest>(consumeResult.Message.Value);

                        var seatId = ticketRequest.SeatId;
                        var userId = ticketRequest.UserId;

                        // ✅ Tạo scope để dùng các service Scoped
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var ticketService = scope.ServiceProvider.GetRequiredService<TicketService>();
                            var ticketQueueService = scope.ServiceProvider.GetRequiredService<TicketQueueService>();

                            // Kiểm tra hàng đợi Redis
                            var isTicketAvailable = await ticketQueueService.AddTicketToQueueAsync(seatId, userId);

                            if (!isTicketAvailable)
                            {
                                _logger.LogInformation($"User {userId} không thể đặt vé vì vé đã hết.");
                                await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveTicketStatus", "Vé đã hết");
                                continue;
                            }

                            // Xử lý đặt vé
                            var result = await ticketService.BookTicketAsync(ticketRequest);
                            _logger.LogInformation($"Processed ticket request for User {userId}: {result}");

                            // Gửi kết quả về client
                            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveTicketStatus", result);

                            // Xoá khỏi hàng đợi
                            await ticketQueueService.RemoveUserFromQueueAsync(seatId, userId);
                        }
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Error while consuming message: {e.Error.Reason}");
                    }
                }

                _consumer.Close();
            });
        }
    }
}