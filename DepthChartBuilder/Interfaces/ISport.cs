using DepthChartBuilder.Models;

namespace DepthChartBuilder.Interfaces
{
    public interface ISport
    {
        IEnumerable<Position> GetPositions();
        int GetMaxDepth();
        string ValidatePositionAbbreviation(string position);
    }
}
