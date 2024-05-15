using DepthChartBuilder.Models;

namespace DepthChartBuilder.Interfaces
{
    public interface IDepthChartManager
    {
        void AddPlayerToDepthChart(Player player, string position, int? depth);
        List<Player> RemovePlayerFromDepthChart(string position, Player player);
        List<Player> GetBackups(string position, Player player);
        List<PlayerDepth> GetDepthChartData();
    }
}
