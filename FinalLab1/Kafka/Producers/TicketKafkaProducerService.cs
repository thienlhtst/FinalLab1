using Confluent.Kafka;
using Newtonsoft.Json;
using static FinalLab1.Services.TicketService;

namespace FinalLab1.Kafka.Producers
{
    public class TicketKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public TicketKafkaProducerService(IConfiguration config)
        {
            var conf = new ProducerConfig { BootstrapServers = config["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<Null, string>(conf).Build();
        }

        public async Task SendTicketRequestAsync(TicketRequest data)
        {
            var message = JsonConvert.SerializeObject(data);
            await _producer.ProduceAsync("ticket_request_topic", new Message<Null, string> { Value = message });
        }
    }
}