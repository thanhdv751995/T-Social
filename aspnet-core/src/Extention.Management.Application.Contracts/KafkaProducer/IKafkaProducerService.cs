using Extention.Management.KafkaProducer;
using Extention.Management.Logs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Extention.Management.Kafka
{
    public interface IKafkaProducerService : IApplicationService
    {
        Task ProducerMQLog(CreateProxyClientLogDto createProxyClientLogDto);
        Task ProducerClientActivities(KafkaActiviteChartDto kafkaActiviteChartDto);
    }
}
