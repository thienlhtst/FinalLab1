using Confluent.Kafka;

namespace FinalLab1.Kafka.Producers
{
    public class KafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducerService(IConfiguration config)
        {
            var conf = new ProducerConfig { BootstrapServers = config["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<Null, string>(conf).Build();
        }

        public async Task SendMessageAsync(string topic, string message)
        {
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        }
    }
}