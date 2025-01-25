using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.Common
{

    public interface IKafkaProducer<in TKey, in TValue> where TValue : class
    {
        Task ProduceAsync(string topic, TKey key, TValue value);
    }
}
