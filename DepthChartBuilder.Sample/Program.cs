using DepthChartBuilder;
using DepthChartBuilder.Models;
using DepthChartBuilder.Sports;

var players = new List<Player>
{
    new Player { Name = "Tom Brady", Position = "QB", Number = 12 },
    new Player { Name = "OJ Howard", Position = "TE", Number = 80 },
    new Player { Name = "Mike Evans", Position = "LWR", Number = 13 },
    new Player { Name = "Jaelon Darden", Position = "LWR", Number = 1 },
    new Player { Name = "Scott Miller", Position = "LWR", Number = 10 },
    new Player { Name = "Leonard Fournette", Position = "RB", Number = 7 }
};

var team = new Team
{
    Name = "Tampa Bay Buccaneers",
    Location = "Tampa, FL",
    Players = players
};

var manager = new DepthChartManager(team);
//var manager = new DepthChartManager(); // Use this line if you don't want to pass a team to the manager
var reporter = new DepthChartReporter(manager);

var player1 = new Player { Name = "Tom Brady", Position = "QB", Number = 12 };
var player2 = new Player { Name = "OJ Howard", Position = "TE", Number = 80 };
var player3 = new Player { Name = "Mike Evans", Position = "LWR", Number = 13 };
var player4 = new Player { Name = "Jaelon Darden", Position = "LWR", Number = 1 };
var player5 = new Player { Name = "Scott Miller", Position = "LWR", Number = 10 };
var player6 = new Player { Name = "Leonard Fournette", Position = "RB", Number = 7 };

manager.AddPlayerToDepthChart(player1, "QB", 1);
manager.AddPlayerToDepthChart(player2, "TE", 1);
manager.AddPlayerToDepthChart(player3, "LWR", 1);
manager.AddPlayerToDepthChart(player4, "LWR", 2);
manager.AddPlayerToDepthChart(player5, "LWR", 3);
manager.AddPlayerToDepthChart(player6, "RB", 1);

Console.WriteLine($"Remove Players: {player2.Name} && {player1.Name}...");
foreach (var removedPlayer in manager.RemovePlayerFromDepthChart("TE", player2))
{
    Console.WriteLine($"Removed: #{removedPlayer.Number} - {removedPlayer.Name}");
}

foreach (var removedPlayer in manager.RemovePlayerFromDepthChart("RB", player1))
{
    Console.WriteLine($"Removed: #{removedPlayer.Number} - {removedPlayer.Name}");
}

Console.WriteLine($"Backups for {player3.Name}:");
foreach (var backup in manager.GetBackups("LWR", player3))
{
    Console.WriteLine($"#{backup.Number} - {backup.Name}");
}

Console.WriteLine($"Backups for {player6.Name}:");
foreach (var backup in manager.GetBackups("RB", player6))
{
    Console.WriteLine($"#{backup.Number} - {backup.Name}");
}

Console.WriteLine($"Backups for {player5.Name}:");
foreach (var backup in manager.GetBackups("TE", player5))
{
    Console.WriteLine($"#{backup.Number} - {backup.Name}");
}

Console.WriteLine();
Console.WriteLine("Get Full NFL Depth Chart:");
Console.WriteLine(reporter.GetFullDepthChart());

// setup depth chart for NBA
var nbaDepthChartManager = new DepthChartManager(null, new NBA());
var nbaDepthChartReporter = new DepthChartReporter(nbaDepthChartManager);

//create nba players
var lebronJames = new Player { Name = "LeBron James", Position = "SF", Number = 6 };
var anthonyDavis = new Player { Name = "Anthony Davis", Position = "PF", Number = 3 };
var russellWestbrook = new Player { Name = "Russell Westbrook", Position = "PG", Number = 0 };
var carmeloAnthony = new Player { Name = "Carmelo Anthony", Position = "SF", Number = 7 };
var dwightHoward = new Player { Name = "Dwight Howard", Position = "C", Number = 39 };

//add players to the depth chart
nbaDepthChartManager.AddPlayerToDepthChart(lebronJames, "SF", 0);
nbaDepthChartManager.AddPlayerToDepthChart(anthonyDavis, "PF", 0);
nbaDepthChartManager.AddPlayerToDepthChart(russellWestbrook, "PG", 0);
nbaDepthChartManager.AddPlayerToDepthChart(carmeloAnthony, "SF", 1);
nbaDepthChartManager.AddPlayerToDepthChart(dwightHoward, "C", 0);

Console.WriteLine();
Console.WriteLine($"Get Full NBA Depth Chart:");
Console.WriteLine(nbaDepthChartReporter.GetFullDepthChart());