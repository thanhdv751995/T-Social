using ActiviteChart.MongoDB;
using Xunit;

namespace ActiviteChart
{
    [CollectionDefinition(ActiviteChartTestConsts.CollectionDefinitionName)]
    public class ActiviteChartApplicationCollection : ActiviteChartMongoDbCollectionFixtureBase
    {

    }
}
