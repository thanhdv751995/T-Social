using Extention.Management.BackgroundJob;
using Extention.Management.Seedings;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.Controllers.Seeding
{
    [Microsoft.AspNetCore.Mvc.Route("api/seeding")]
    public class SeedingController : ManagementController
    {
        private readonly SeedingAppService _seedingAppService;
        public SeedingController(SeedingAppService seedingAppService)
        {
            _seedingAppService = seedingAppService;
        }

        [HttpPost("Create")]
        public async Task CreateAsync([FromBody]CreateSeedingDto createSeedingDto)
        {
            await _seedingAppService.CreateAsync(createSeedingDto);
        }
        /// <summary>
        /// ConditionValue: 
        /// 1: Tác vụ đang chạy
        /// 2: Tác vụ đã xử lý
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="conditionValue"></param>
        /// <returns></returns>
        [HttpGet("Get-List")]
        public async Task<PagedResultDto<SeedingDto>> GetListSeeding(string name, int skip, int take, int conditionValue)
        {
            var rs = await _seedingAppService.GetListSeeding(name, skip, take, conditionValue);

            return rs;
        }
        [HttpGet("Clients-Seeding-By-Name")]
        public async Task<SeedingBackgroundJobDto> ListClientScheduleSeeding(string seedingName)
        {
            var rs = await _seedingAppService.ListClientScheduleSeeding(seedingName);

            return rs;
        }
        [HttpGet("Clients-Seeding-Job")]
        public PagedResultDto<ClientSeedingJobDto> GetListClientSeedingJob(string seedingName, string clientId, int skip, int take)
        {
            var rs =  _seedingAppService.GetListClientSeedingJob(seedingName, clientId , skip, take);

            return rs;
        }
        [HttpGet("Clients-Seeding-Dashboard")]
        public PagedResultDto<DashBoardSeedingDto> GetListSeedingByClientAsync(string clientId, int skip, int take)
        {
            var rs = _seedingAppService.GetListSeedingByClientAsync(clientId, skip, take);

            return rs;
        }
        [HttpGet("List-Name-Seeding")]
        public List<string> GetListNameSeeding()
        {
            return _seedingAppService.GetListNameSeeding();
        }
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _seedingAppService.DeleteAsync(id);
        }
    }
}
