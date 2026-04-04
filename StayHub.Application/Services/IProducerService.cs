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
        Task SendExportFileCommand(int userId, int id, string name);
    }
}
