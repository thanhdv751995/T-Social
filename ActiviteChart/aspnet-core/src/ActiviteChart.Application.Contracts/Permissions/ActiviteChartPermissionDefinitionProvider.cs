using ActiviteChart.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ActiviteChart.Permissions
{
    public class ActiviteChartPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(ActiviteChartPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(ActiviteChartPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ActiviteChartResource>(name);
        }
    }
}
