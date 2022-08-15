using Extention.Management.KafkaProducer;
using Extention.Management.Logs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extention.Management.Controllers.Kafka
{
    [Microsoft.AspNetCore.Mvc.Route("api/kafka")]
    public class KafkaController : ManagementController
    {
        private readonly KafkaProducerAppService _kafkaAppService;
        public KafkaController(KafkaProducerAppService kafkaAppService)
        {
            _kafkaAppService = kafkaAppService;
        }
        /// <summary>
        ///  Add Log to MQ
        /// </summary>
        [HttpPost("Producer-MQ-Log")]
        public async Task Producer([FromBody]CreateProxyClientLogDto createProxyClientLogDto)
        {
            await _kafkaAppService.ProducerMQLog(createProxyClientLogDto);
        }
        [HttpPost("Producer-ActiviteChart")]
        public async Task ProducerClientActivities([FromBody] KafkaActiviteChartDto kafkaActiviteChartDto)
        {
            await _kafkaAppService.ProducerClientActivities(kafkaActiviteChartDto);
        }
    }
}
