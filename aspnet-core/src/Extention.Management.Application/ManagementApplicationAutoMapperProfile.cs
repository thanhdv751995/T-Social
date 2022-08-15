using AutoMapper;
using ExtensionsManagement.ClientFacebookDtos;
using ExtensionsManagement.ClientFacebooks;
using ExtensionsManagement.ProxyIpDtos;
using ExtensionsManagement.ProxyIps;
using Extention.Management.BackgroundJob;
using Extention.Management.ChromeProfile;
using Extention.Management.Client;
using Extention.Management.ClientActivities;
using Extention.Management.ClientBelongToProfiles;
using Extention.Management.ClientFriends;
using Extention.Management.ClientInfomations;
using Extention.Management.Clients;
using Extention.Management.Clients.Entity;
using Extention.Management.Commenteds;
using Extention.Management.ExtensionVariables;
using Extention.Management.GroupJoin;
using Extention.Management.GroupTypes;
using Extention.Management.HangfireJob;
using Extention.Management.Histories;
using Extention.Management.Logs;
using Extention.Management.Notifications;
using Extention.Management.Profiles;
using Extention.Management.ProxyUsingScript;
using Extention.Management.Scripts;
using Extention.Management.SeedingContent;
using Extention.Management.Seedings;
using Extention.Management.StatusAttachments;
using Extention.Management.Statuss;
using Extention.Management.VirtualMachines;

namespace Extention.Management
{
    public class ManagementApplicationAutoMapperProfile : Profile
    {
        public ManagementApplicationAutoMapperProfile()
        {
            CreateMap<Clients.Client, ClientDto>();
            CreateMap<Proxys.Proxy, ProxyDto>();
            CreateMap<CreateUpdateProxyDto, Proxys.Proxy>();
            CreateMap<CreateUpdateClientDto, ClientDto>();
            CreateMap<ClientActiveEntity, ListClientActiveDto>();
            CreateMap<ProxyClientLog, ProxyCientLogDto>();
            CreateMap<Script, ScriptDto>();
            CreateMap<AccountUsingScripts.ClientUsingScript, ClientUsingScriptDto>();
            CreateMap<Script, ClientUsingScriptDto>();
            CreateMap<Script, ListClientUsingScriptDto>();
            CreateMap<AccountUsingScripts.ClientUsingScript, ScriptDto>();
            CreateMap<StatusAttachment, StatusAttachmentDto>();
            CreateMap<Status, StatusDto>();
            CreateMap<ClientFriend, ClientFriendDto>();
            CreateMap<ClientInfomation, ClientInfomationDto>();
            CreateMap<CChromeProfile.ChromeProfile, ChromeProfileDto>();
            CreateMap<CreateClientFriendDto, ClientFriend>();
            CreateMap<GroupJoins.GroupJoin, GroupJoinDto>();
            CreateMap<CreateUpdateClientInfomationDto, ClientInfomation>();
            CreateMap<History, HistoryDto>();
            CreateMap<HistoryDto, Clients.Client>();
            CreateMap<Commented, CommentedDto>();
            CreateMap<ProfileClient, ProfileClientDto>();
            CreateMap<ProfileClient, ClientBelongToProfileDto>();
            CreateMap<ClientActivity, ClientActivityDto>();
            CreateMap<ProfileClient, ClientProfileWithScriptDto>();
            CreateMap<ProfileClient,ClientInfoWithStatus>();
            CreateMap<ScriptDto, ClientUsingScriptDto>().ForMember(c => c.ScriptId, s => s.MapFrom(script => script.Id));
            CreateMap<Script, ClientUsingScriptDto>();
            CreateMap<Clients.Client, AccountDto>();
            CreateMap<ClientFriend, CreateClientFriendDto>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<ClientFriend, FriendDto>().ForMember(x => x.Name, f => f.MapFrom(fr => fr.FriendName));
            CreateMap<Seeding, SeedingDto>();
            CreateMap<SeedingContents.SeedingContent, SeedingContentDto>();
            CreateMap<Clients.Client, ClientSeedingDto>();
            CreateMap<HangfireJobs.HangfireJob, HangfireJobDto>();
            CreateMap<Clients.Client, HangfireJobDto>();
            CreateMap<Script, HangfireJobDto>();
            CreateMap<ProfileClient, ListProfileDto>();
            CreateMap<VirtualMachine, VirtualMachineDto>();
            CreateMap<ExtensionVariables.ExtensionVariable, ExtensionVariableDto>();
            CreateMap<GroupType, GroupTypeDto>();
            CreateMap<ProfileClient,UpdateProfileForScriptDto>();
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
        }
    }
}
