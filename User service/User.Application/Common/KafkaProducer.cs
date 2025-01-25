using Confluent.Kafka.Admin;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace User.Application.Common
{

    public class KafkaProducer<TKey, TValue> : IDisposable, IKafkaProducer<TKey, TValue> where TValue : class
    {
        private readonly IProducer<TKey, TValue> _producer;

        private readonly ProducerConfig _config;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<TKey, TValue>(config).SetValueSerializer(new KafkaSerializer<TValue>()).Build();
            _config = config;
        }

        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            try
            {
                string topic2 = topic;
                using (IAdminClient adminClient = new AdminClientBuilder(_config).Build())
                {
                    Metadata setData = adminClient.GetMetadata(TimeSpan.FromSeconds(35.0));
                    if (setData.Topics.All((TopicMetadata x) => x.Topic != topic2))
                    {
                        TopicSpecification topicSpec = new TopicSpecification
                        {
                            Name = topic2,
                            NumPartitions = 1,
                            ReplicationFactor = 3
                        };
                        await adminClient.CreateTopicsAsync(new TopicSpecification[1] { topicSpec });
                    }
                }

                await _producer.ProduceAsync(topic2, new Message<TKey, TValue>
                {
                    Key = key,
                    Value = value
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
    internal sealed class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
            {
                return null;
            }

            if (typeof(T) == typeof(Ignore))
            {
                throw new NotSupportedException("Not Supported.");
            }

            string s = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
