using Extention.Management.KafkaProducer;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ActiviteChart.ActiviteChartDatas
{
    public class ActiviteChartDataAppService : ActiviteChartAppService
    {
        private readonly IActiviteChartDataRepository _activiteChartDataRepository;
        private readonly ActiviteChartDataManager _activiteChartDataManager;
        readonly List<int> labels = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });
        public ActiviteChartDataAppService(IActiviteChartDataRepository activiteChartDataRepository, ActiviteChartDataManager activiteChartDataManager)
        {
            _activiteChartDataRepository = activiteChartDataRepository;
            _activiteChartDataManager = activiteChartDataManager;
        }
        public async Task<DataActiviteChartDto> CreateAsync(CreateActiviteChartDataDto input)
        {
            var activityClient = _activiteChartDataManager.CreateAsync(input.Username,input.ScriptId,input.TypeName, input.Content);
            activityClient.CreationTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

            await _activiteChartDataRepository.InsertAsync(activityClient);
            return ObjectMapper.Map<ActiviteChartData, DataActiviteChartDto>(activityClient);
        }
        public async Task<List<DataActiviteChartDto>> GetListAsync()
        {
            var listActivity = await _activiteChartDataRepository.GetListAsync();
            return ObjectMapper.Map<List<ActiviteChartData>, List<DataActiviteChartDto>>(listActivity);
        }
        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public async Task<DatasetChartActivitiesDto> GetListActivityByClientId(string userName, DateTime? startDate, DateTime? endDate)
        {
            var builder = Builders<ActiviteChartData>.Filter;
            var filter = builder.Empty;
            var Client = new MongoClient("mongodb://localhost:27017/ActiviteChart");

            var dbName = Client.GetDatabase("ActiviteChart");
            var collection = dbName.GetCollection<ActiviteChartData>("ActiviteChart");

            filter &= builder.Eq(x => x.Username, userName);
            if(startDate != null)
            {
                filter &= builder.Gt(x => x.CreationTime, startDate);
            }
            if (endDate != null)
            {
                filter &= builder.Lt(x => x.CreationTime, endDate);
            }
            var result = await collection.Find(filter).ToListAsync();
            var listActivitiesChartDto = ObjectMapper.Map<List<ActiviteChartData>, List<DataActiviteChartDto>>(result);
            var listActivitiesChart = listActivitiesChartDto;

            List<DatasetDto> datasets = new();

            for (var i = 0; i < listActivitiesChart.Count; i++)
            {
                List<int> data = new();
                for (var j = 0; j < 24; j++)
                {
                    data.Add(0);
                }
                int creationTimeHour = listActivitiesChart[i].CreationTime.Hour;
                string[] arrListStr = listActivitiesChart[i].Content.Split('-');

                if (datasets.Any(x => x.label == listActivitiesChart[i].TypeName))
                {
                    int index = datasets.FindIndex(x => x.label == listActivitiesChart[i].TypeName);
                    if (arrListStr[0] == "")
                    {
                        datasets[index].data[creationTimeHour - 1] = datasets[index].data[creationTimeHour - 1] + 1;
                    }
                    else
                    if (IsNumber(arrListStr[0]))
                    {
                        if (Int64.Parse(arrListStr[0]) > 100000)
                        {
                            datasets[index].data[creationTimeHour - 1] = datasets[index].data[creationTimeHour - 1] + 1;
                        }
                        else
                        {
                            datasets[index].data[creationTimeHour - 1] = datasets[index].data[creationTimeHour - 1] + int.Parse(arrListStr[0]);
                        }
                    }
                    else
                    {
                        datasets[index].data[creationTimeHour - 1] = datasets[index].data[creationTimeHour - 1] + 1;
                    }
                }
                else
                {
                    if (arrListStr[0] == "")
                    {
                        data[creationTimeHour - 1] = data[creationTimeHour - 1] + 1;
                    }
                    else if (IsNumber(arrListStr[0]))
                    {
                        if (Int64.Parse(arrListStr[0]) > 100000)
                        {
                            data[creationTimeHour - 1] = data[creationTimeHour - 1] + 1;
                        }
                        else
                        {
                            data[creationTimeHour - 1] = data[creationTimeHour - 1] + int.Parse(arrListStr[0]);
                        }
                    }
                    else
                    {
                        data[creationTimeHour - 1] = data[creationTimeHour - 1] + 1;
                    }

                    DatasetDto datasetDto = new()
                    {
                        label = listActivitiesChart[i].TypeName,
                        data = data,
                        backgroundColor = GetRandColor()
                    };

                    datasets.Add(datasetDto);
                }
            }


            DatasetChartActivitiesDto datasetChartActivitiesDto = new()
            {
                labels = labels,
                datasets = datasets
            };

            return datasetChartActivitiesDto;
        }

        public static string GetRandColor()
        {
            Random rnd = new();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return "#" + hexOutput;
        }
        public async Task<PagedResultDto<LogActivityDto>> GetLogActivity (string userName, int skip, int take, DateTime? startDate, DateTime? endDate)
        {
            var builder = Builders<ActiviteChartData>.Filter;
            var filter = builder.Empty;
            var Client = new MongoClient("mongodb://localhost:27017/ActiviteChart");

            var dbName = Client.GetDatabase("ActiviteChart");
            var collection = dbName.GetCollection<ActiviteChartData>("ActiviteChart");
                        filter &= builder.Eq(x => x.Username, userName);
            if(startDate != null || startDate.ToString() != "")
            {
                filter &= builder.Gt(x => x.CreationTime, startDate);
            }
            if (endDate != null || endDate.ToString() != "")
            {
                filter &= builder.Lt(x => x.CreationTime, endDate);
            }
            var result = await collection.Find(filter).ToListAsync();
            var logSkipTake = result.OrderByDescending(x => x.CreationTime).Skip(skip).Take(take).ToList();
            return new PagedResultDto<LogActivityDto>(result.Count, ObjectMapper.Map<List<ActiviteChartData>, List<LogActivityDto>>(logSkipTake));
        }
    }
}
