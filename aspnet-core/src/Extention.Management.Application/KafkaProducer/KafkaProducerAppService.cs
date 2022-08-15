using Confluent.Kafka;
using Extention.Management.Clients;
using Extention.Management.Enums;
using Extention.Management.Kafka;
using Extention.Management.Logs;
using Extention.Management.Scripts;
using Extention.Management.Seedings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace Extention.Management.KafkaProducer
{
    public class KafkaProducerAppService : ManagementAppService, IKafkaProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ProducerConfig config;
        private readonly IScriptRepository _scriptRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ISeedingRepository _seedingRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public KafkaProducerAppService(IConfiguration configuration,
            IScriptRepository scriptRepository,
            IClientRepository clientRepository,
            ISeedingRepository seedingRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _configuration = configuration;
            config = new ProducerConfig { BootstrapServers = _configuration.GetSection("Kafka")["BootstrapServer"], LingerMs = 10 };
            _scriptRepository = scriptRepository;
            _clientRepository = clientRepository;
            _seedingRepository = seedingRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task ProducerMQLog(CreateProxyClientLogDto createProxyClientLogDto)
        {
            using var p = new ProducerBuilder<string, string>(config).Build();
            try
            {
                var dr = await p.ProduceAsync(_configuration.GetSection("Kafka")["Topic"],
                    new Message<string, string> { Key = _configuration.GetSection("Kafka")["Key"], Value = JsonConvert.SerializeObject(createProxyClientLogDto) });
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }

        public async Task ProducerClientActivities(KafkaActiviteChartDto kafkaActiviteChartDto)
        {
            using var uow = _unitOfWorkManager.Begin(true);
            using var p = new ProducerBuilder<string, string>(config).Build();
            try
            {
                var script = await _scriptRepository.GetAsync(x => x.Id == kafkaActiviteChartDto.ScriptId);

                if (_seedingRepository.Any(x => script.ScriptName.Contains(x.Name)))
                {
                    var client = await _clientRepository.GetAsync(kafkaActiviteChartDto.ClientId);

                    DataActiviteChartDto dataActiviteChartDto = new()
                    {
                        Username = client.UserName,
                        ScriptId = script.Id,
                        TypeName = EnumAppService.GetNameEnum<Type.Type>(script.Type),
                        Content = script.Value
                    };

                    var dr = await p.ProduceAsync("Activites",
                        new Message<string, string> { Key = "ClientActivites", Value = JsonConvert.SerializeObject(dataActiviteChartDto) });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
            await uow.CompleteAsync();
        }
    }
}
