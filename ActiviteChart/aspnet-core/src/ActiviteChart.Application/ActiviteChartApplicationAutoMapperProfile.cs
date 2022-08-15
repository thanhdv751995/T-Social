using ActiviteChart.ActiviteChartDatas;
using AutoMapper;
using Extention.Management.KafkaProducer;

namespace ActiviteChart
{
    public class ActiviteChartApplicationAutoMapperProfile : Profile
    {
        public ActiviteChartApplicationAutoMapperProfile()
        {
            CreateMap<ActiviteChartData, DataActiviteChartDto>();
            CreateMap<ActiviteChartData, LogActivityDto>();
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}
