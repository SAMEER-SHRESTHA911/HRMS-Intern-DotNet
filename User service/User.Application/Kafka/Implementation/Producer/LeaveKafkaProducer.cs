using Confluent.Kafka;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Common;
using User.Application.Kafka.Interface;
using User.Domain.Entities;

namespace User.Application.Kafka.Implementation.Producer
{
    public class LeaveKafkaProducer : ILeaveKafkaProducer
    {
        private readonly IKafkaProducer<string, Employee> _producer;
        private readonly IKafkaProducer<string, Department> _deptProducer;

        public LeaveKafkaProducer(IKafkaProducer<string, Employee> producer, IKafkaProducer<string, Department> deptProducer)
        {
            _producer = producer;
            _deptProducer = deptProducer;
        }
        public async Task AddLeaveBalanceProduce(string key, Employee message)
        {
            await _producer.ProduceAsync("employee-topic", key, message);
        }
         public async Task AddLeaveRequestProduce(string key, Department message)
        {
            await _deptProducer.ProduceAsync("dept-topic", key, message);
        }

    }
}
