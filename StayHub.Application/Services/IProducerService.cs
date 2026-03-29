using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Services
{
    public interface IProducerService
    {
        Task SendEvent(string topic, Message<int,string> message);
    }
}
