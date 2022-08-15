using ActiviteChart.MongoDB;
using Xunit;

namespace ActiviteChart
{
    [CollectionDefinition(ActiviteChartTestConsts.CollectionDefinitionName)]
    public class ActiviteChartDomainCollection : ActiviteChartMongoDbCollectionFixtureBase
    {

    }
}
