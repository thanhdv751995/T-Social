
using Extention.Management.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Extention.Management.StatusAttachments
{
    public class StatusAttachmentAppService : ManagementAppService, IStatusAttachmentAppService
    {
        private readonly StatusAttachmentManager _statusAttachmentManager;
        public IStatusAttachmentRepository _statusAttachmentRepository;

        public StatusAttachmentAppService(
         StatusAttachmentManager statusAttachmentManager,
        IStatusAttachmentRepository statusAttachmentRepository

         )
        {
            _statusAttachmentManager = statusAttachmentManager;
            _statusAttachmentRepository = statusAttachmentRepository;
        }
        public async Task<StatusAttachmentDto> CreateAsync(CreateUpdateStatusAttachment input)
        {
            var att = _statusAttachmentManager.CreateAsync(input.IdUser, input.URL);

            await _statusAttachmentRepository.InsertAsync(att);
            return ObjectMapper.Map<StatusAttachment, StatusAttachmentDto>(att);
        }
        public async Task CreateByListAttachment(CreateByListAttachmentDto input)
        {
            foreach (var url in input.URL)
            {
                var att = _statusAttachmentManager.CreateAsync(input.IdUser, url);
                await _statusAttachmentRepository.InsertAsync(att);
            }

        }
        //    public async Task<PagedResultDto<GetListAttachmentDto>> GetListAsync()
        //    {
        //        var query = from client in _clientRepository
        //                    join att in _statusAttachmentRepository on client.Id equals att.IdUser
        //                    select new GetListAttachmentDto()
        //                    {
        //                        clientName = client.UserName,
        //                        idClient = client.Id,
        //                        URL = att.URL
        //                    };
        //        var totalCount = query.Count();
        //        return new PagedResultDto<GetListAttachmentDto>(
        //                    0,
        //                   query.ToList()
        //                );
        //    }
        //    public async Task<GetAttachmentByUserDto> GetAttachmentByIdUser(Guid idUser)
        //    {
        //        var client = await _clientRepository.GetAsync(idUser);
        //        return new GetAttachmentByUserDto
        //        {
        //            UserName = client.UserName,
        //            IdClient = client.Id,
        //            URL = _statusAttachmentRepository.GetListAsync(x => x.IdUser == idUser).Result.Select(x => x.URL).ToList()
        //        };
        //    }
    }
}
