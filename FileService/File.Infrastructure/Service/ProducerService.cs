using File.Infrastructure.Consumer;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Infrastructure.Service
{
    public class FileExportedEvent
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime ExportedAt { get; set; }
    }
    public class ProducerService(ITopicProducer<FileExportedEvent> producer)
    {
        public async Task PublishExportedFileEvent(int id, string fileName, DateTime dateTime)
        {
            await producer.Produce(new FileExportedEvent
            {
                Id = id,
                FileName = fileName,
                ExportedAt = dateTime
            });
        }
    }
}
