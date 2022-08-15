using Extention.Management.AccountUsingScripts;
using Extention.Management.AcountAtives;
using Extention.Management.ClientActivities;
using Extention.Management.ClientFriends;
using Extention.Management.ClientInfomations;
using Extention.Management.Clients;
using Extention.Management.Commenteds;
using Extention.Management.CommentedSeedingUrls;
using Extention.Management.ExpandScripts;
using Extention.Management.ExtensionVariables;
using Extention.Management.GroupJoins;
using Extention.Management.GroupTypes;
using Extention.Management.HangfireJobs;
using Extention.Management.Histories;
using Extention.Management.Logs;
using Extention.Management.Notifications;
using Extention.Management.PingClientExtensions;
using Extention.Management.ProfileGroupTypes;
using Extention.Management.ProfileOfClients;
using Extention.Management.Profiles;
using Extention.Management.Proxys;
using Extention.Management.ReactedSeedingUrls;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Scripts;
using Extention.Management.SeedingContentComments;
using Extention.Management.SeedingContents;
using Extention.Management.SeedingContentShares;
using Extention.Management.Seedings;
using Extention.Management.StatusAttachments;
using Extention.Management.Statuss;
using Extention.Management.StatussStore;
using Extention.Management.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Extention.Management.EntityFrameworkCore
{
    public static class ManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureManagement(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.Entity<Client>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Clients",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.NameFacebook).IsRequired().HasMaxLength(128);
                b.Property(x => x.AvatarUrl).IsRequired();
                b.Property(x => x.UserName).IsRequired().HasMaxLength(128);
                b.Property(x => x.Password).IsRequired().HasMaxLength(128);
                b.Property(x => x.SecretKey).IsRequired().HasMaxLength(128);
                b.Property(x => x.Cookie).IsRequired();
                b.Property(x => x.ProxyIp).IsRequired().HasMaxLength(128);
                b.Property(x => x.AccessToken).IsRequired();
            });
            builder.Entity<Proxy>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Proxy",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProxyIp).IsRequired().HasMaxLength(128);
                
            });
            builder.Entity<Script>(b =>
            {

                b.ToTable(ManagementConsts.DbTablePrefix + "Scripts",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Value).IsRequired();
                b.Property(x => x.IsActive).IsRequired();
                b.Property(x => x.IsDefault).IsRequired();
            });
            builder.Entity<ClientUsingScript>(b =>
            {

                b.ToTable(ManagementConsts.DbTablePrefix + "ClientUsingScripts",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ScriptId).IsRequired();
                b.Property(x => x.ClientId).IsRequired();
                b.HasOne<Script>().WithMany().HasForeignKey(x => x.ScriptId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ProxyClientLog>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ProxyClientLogs",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProxyIp).IsRequired().HasMaxLength(128);
            });
            builder.Entity<StatusAttachment>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "StatusAttachments",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdStatus).IsRequired();
                b.Property(x => x.URL).IsRequired().HasMaxLength(128);
                b.HasOne<Status>().WithMany().HasForeignKey(x => x.IdStatus).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Status>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Statuss",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdUser).IsRequired();
                b.Property(x => x.Content).IsRequired().HasMaxLength(5000);
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.IdUser).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ClientActivity>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ClientActivities",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.UserName).IsRequired();
                b.Property(x => x.URL).IsRequired();
                b.Property(x => x.Content).IsRequired();
                b.Property(x => x.ScriptName).IsRequired();
            });
            builder.Entity<ClientFriend>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ClientFriends",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdUser).IsRequired();
                b.Property(x => x.UserName).IsRequired();
                b.Property(x => x.FriendName).IsRequired();
                b.Property(x => x.AvatarUrl).IsRequired();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.IdUser).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ClientInfomation>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ClientInfomation",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.IdUser).IsRequired();
                b.Property(x => x.DayOfBirth).IsRequired();
                b.Property(x => x.NameUser).IsRequired().HasMaxLength(200);
                b.Property(x=>x.ClientId).IsRequired().HasMaxLength(200);
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.IdUser).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<CChromeProfile.ChromeProfile>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ChromeProfile",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
            builder.Entity<GroupJoin>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "GroupJoin",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.HasOne<GroupType>().WithMany().HasForeignKey(x => x.GroupTypeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<StatusStore>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "StatusStore",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
            builder.Entity<AttachmentsStore.AttachmentsStore>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "AttachmentsStore",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
            builder.Entity<Notification>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Notification",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
            });
            builder.Entity<History>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "History",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<Script>().WithMany().HasForeignKey(x => x.ScriptId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Commented>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Commenteds",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ProfileClient>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ProfileClients",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.ProfileName).HasMaxLength(200);
            });
            builder.Entity<ClientBelongToProfile>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ClientBelongToProfile",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
                b.HasOne<ProfileClient>().WithMany().HasForeignKey(x => x.ProfileClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<ScriptDefaultType>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ScriptDefaultType",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Script>().WithMany().HasForeignKey(x => x.ScriptId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
                b.HasOne<ProfileClient>().WithMany().HasForeignKey(x => x.ProfileId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Seeding>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "Seedings",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<GroupType>().WithMany().HasForeignKey(x => x.GroupTypeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<SeedingContent>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "SeedingContents",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Seeding>().WithMany().HasForeignKey(x => x.SeedingId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<SeedingContentComment>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "SeedingContentComments",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Seeding>().WithMany().HasForeignKey(x => x.SeedingId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<SeedingContentShare>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "SeedingContentShares",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Seeding>().WithMany().HasForeignKey(x => x.SeedingId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<HangfireJob>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "HangfireJobs",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
                b.HasOne<Script>().WithMany().HasForeignKey(x => x.ScriptId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<VirtualMachine>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "VirtualMachines",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
            });
            builder.Entity<CommentedSeedingUrl>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "CommentedSeedingUrls",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<ExtensionVariable>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ExtensionVariables",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
            });
            builder.Entity<GroupType>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "GroupTypes",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
            });
            builder.Entity<ProfileGroupType>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ProfileGroupTypes",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<ProfileClient>().WithMany().HasForeignKey(x => x.ProfileId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
                b.HasOne<GroupType>().WithMany().HasForeignKey(x => x.GroupTypeId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<ReactedSeedingUrl>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "ReactedSeedingUrls",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
            builder.Entity<PingClientExtension>(b =>
            {
                b.ToTable(ManagementConsts.DbTablePrefix + "PingClientExtensions",
                    ManagementConsts.DbSchema);
                b.ConfigureByConvention();
                b.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);//auto configure for the base class props
            });
        }
    }
}