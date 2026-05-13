using File.Infrastructure.Consumer;
using MassTransit;
using Shared.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure.Service.Message
{
       public class ProducerService(ITopicProducer<int,FileExportedEvent> producer)
    {
        public async Task PublishExportedFileEvent(int UserId, int id, string fileName, DateTime dateTime)
        {
            await producer.Produce(id, new FileExportedEvent
            {
                UserId = UserId,
                Id = id,
                FileName = fileName,
                ExportedAt = dateTime
            });
        }
    }
}
