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
        private readonly TicketService _ticketService;
        private readonly TicketQueueService _ticketQueueService;
        private readonly ILogger<TicketKafkaConsumerService> _logger;
        private readonly IHubContext<QueueHub> _hubContext; // Inject HubContext
        private readonly string _topic = "ticket_request_topic";

        public TicketKafkaConsumerService(IConfiguration config, TicketService ticketService, TicketQueueService ticketQueueService, ILogger<TicketKafkaConsumerService> logger, IHubContext<QueueHub> hubContext)
        {
            var conf = new ConsumerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                GroupId = "ticket-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(conf).Build();
            _ticketService = ticketService;
            _ticketQueueService = ticketQueueService;
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

                        // Lấy UserId và SeatId từ TicketRequest
                        var seatId = ticketRequest.SeatId;
                        var userId = ticketRequest.UserId;

                        // Kiểm tra hàng đợi Redis để đảm bảo user là người đầu tiên trong hàng đợi
                        var isTicketAvailable = await _ticketQueueService.AddTicketToQueueAsync(seatId, userId);

                        if (!isTicketAvailable)
                        {
                            _logger.LogInformation($"User {userId} không thể đặt vé vì vé đã hết.");
                            // Gửi thông báo không thành công đến client
                            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveTicketStatus", "Vé đã hết");
                            continue; // Không tiếp tục nếu vé đã hết
                        }

                        // Gọi TicketService để xử lý yêu cầu đặt vé
                        var result = await _ticketService.BookTicketAsync(ticketRequest);
                        _logger.LogInformation($"Processed ticket request for User {ticketRequest.UserId}: {result}");

                        // Gửi kết quả thành công đến client
                        await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveTicketStatus", result);

                        // Sau khi xử lý, xóa người dùng khỏi hàng đợi Redis
                        await _ticketQueueService.RemoveUserFromQueueAsync(seatId, userId);
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