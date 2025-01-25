using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Application.Kafka.Interface
{
    public interface ILeaveKafkaProducer
    {
        Task AddLeaveBalanceProduce(string key, Employee message);
    }
}
