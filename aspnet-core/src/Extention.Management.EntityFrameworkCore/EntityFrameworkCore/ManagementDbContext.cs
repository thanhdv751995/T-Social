using Microsoft.EntityFrameworkCore;
using Extention.Management.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Extention.Management.Clients;
using Extention.Management.Proxys;
using Extention.Management.Scripts;
using Extention.Management.ExpandScripts;
using Extention.Management.AccountUsingScripts;
using Extention.Management.Logs;
using Extention.Management.AcountAtives;
using Extention.Management.StatusAttachments;
using Extention.Management.ClientInfomations;
using Extention.Management.Statuss;
using Extention.Management.ClientFriends;
using Extention.Management.ClientActivities;
using Extention.Management.CChromeProfile;
using Extention.Management.GroupJoins;
using Extention.Management.StatussStore;
using Extention.Management.Notifications;
using Extention.Management.Histories;
using Extention.Management.Commenteds;
using Extention.Management.Profiles;
using Extention.Management.ProfileOfClients;
using Extention.Management.ScriptDefaultTypes;
using Extention.Management.Seedings;
using Extention.Management.SeedingContents;
using Extention.Management.HangfireJobs;
using Extention.Management.SeedingContentComments;
using Extention.Management.SeedingContentShares;
using Extention.Management.ViewClientUsingScripts;
using Extention.Management.VirtualMachines;
using Extention.Management.CommentedSeedingUrls;
using Extention.Management.ExtensionVariables;
using Extention.Management.GroupTypes;
using Extention.Management.ProfileGroupTypes;
using Extention.Management.ReactedSeedingUrls;
using Extention.Management.PingClientExtensions;

namespace Extention.Management.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See ManagementMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class ManagementDbContext : AbpDbContext<ManagementDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Proxy> Proxys { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<ExpandScript> ExpandScripts { get; set; }
        public DbSet<ClientUsingScript> ClientUsingScripts { get; set; }
        public DbSet<ProxyClientLog> ProxyClientLogs { get; set; }
        public DbSet<AccountActive> AccountActives { get; set; }
        public DbSet<StatusAttachment> StatusAttachments { get; set; }
        public DbSet<ClientInfomation> ClientInfomations { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<ClientFriend> ClientFriends { get; set; }
        public DbSet<ClientActivity> ClientActivities { get; set; }
        public DbSet<CChromeProfile.ChromeProfile> ChromeProfile { get; set; }
        public DbSet<GroupJoin> GroupJoin { get; set; }
        public DbSet<StatusStore> StatusStore { get; set; }
        public DbSet<AttachmentsStore.AttachmentsStore> AttachmentsStore { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Commented> Commenteds { get; set; }
        public DbSet<ClientBelongToProfile> ClientBelongToProfiles { get; set; }
        public DbSet<ProfileClient> ProfileClients { get; set; }
        public DbSet<ScriptDefaultType> ScriptDefaultTypes { get; set; }
        public DbSet<Seeding> Seedings { get; set; }
        public DbSet<SeedingContent> SeedingContents { get; set; }
        public DbSet<SeedingContentComment> SeedingContentComments { get; set; }
        public DbSet<SeedingContentShare> SeedingContentShares { get; set; }
        public DbSet<HangfireJob> HangfireJobs { get; set; }
        public DbSet<VirtualMachine> VirtualMachines { get; set; }
        public DbSet<CommentedSeedingUrl> CommentedSeedingUrls { get; set; }
        public DbSet<ExtensionVariable> ExtensionVariables { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<ProfileGroupType> ProfileGroupTypes { get; set; }
        public DbSet<ReactedSeedingUrl> ReactedSeedingUrls { get; set; }
        public DbSet<PingClientExtension> PingClientExtensions { get; set; }
        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside ManagementDbContextModelCreatingExtensions.ConfigureManagement
         */

        public ManagementDbContext(DbContextOptions<ManagementDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<ClientUsingScriptView>()
                .ToView(nameof(ClientUsingScriptView))
                .HasNoKey();
            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the ManagementEfCoreEntityExtensionMappings class
                 */
            });

            builder.ConfigureManagement();
        }
    }
}
