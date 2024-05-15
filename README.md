Depth Chart Builder
Description
This project is a depth chart manager for teams of the following sports: NFL, MLB, and NBA. It allows you to add and remove players from a team's depth chart, get a list of backups for a given player as well as print out the Depth Chart. Sport and Teams are optional but can be built instanciated from the specific Sport or Team models in the DepthChartBuilder library. If a team is not specified, players can still be added to the depth chart, however if a team is specified and provided to the DepthChartManager, players added to the depth chart will be validated against the players specified in the team. If a sport is not specified, the DepthChartManager will default to the NFL and validate against NFL Positions and max team member count.

Installation
Clone the repository: git clone https://github.com/yourusername/depthchartbuilder.git
Navigate to the project directory: cd depthchartbuilder
Install the required packages: dotnet restore
Usage
There is a sample project included that can be used to test the Depth Chart Builder. To run the sample project, use the following command: dotnet run --project DepthChartBuilder.Sample Feel free to modify the sample project to test different scenarios.

To use the Depth Chart Builder, create a new instance of the DepthChartManager class and call its methods. For example: var manager = new DepthChartManager(); manager.AddPlayerToDepthChart(player, position, depth);

To create a team and add players to it, use the following code: var team = new Team("Team Name", "Location");

To add players to the team, create a list of players and assign them to the team: var players = new List<Player> { player1, player2, player3 }; team.Players = players;

You can then provide the team to the DepthChartManager when adding players to the depth chart: var manager = new DepthChartManager(team);

To use a different sport, create an instance of the specific sport class and provide it to the DepthChartManager: var sport = new NFL(); var manager = new DepthChartManager(team, sport);

Alternatively if a team is not required team can be null when providing a sport to the DepthChartManager: var sport = new NFL(); var manager = new DepthChartManager(null, sport);

To remove a player from the depth chart, provide the position abbreviation and the player object to the RemovePlayerFromDepthChart() method: manager.RemovePlayerFromDepthChart("LWR", player);

To get a list of backups for a given player, provide the position abbreviation and the player object to the GetBackups() method: var backups = manager.GetBackups("LWR", player);

To print out the depth chart, use the PrintDepthChart() from the DepthChartReporter class method: var reporter = new DepthChartReporter(manager); Console.WriteLine(reporter.PrintDepthChart());

Assumptions
The depth chart is sorted by the depth of the players.
The depth chart does not allow duplicate players at the same position.
The depth chart does not allow players to be added to positions that are not valid for the sport.
The depth chart does not allow players to be added to positions that are already at the maximum team member count.
The depth chart does not allow players to be removed from positions that are not valid for the sport.
The depth chart does not allow players to be removed from positions that are not the player's position.
The depth chart does not allow players to be added to the depth chart if the team is specified and the player is not in the team.
The depth chart does not allow players to be removed from the depth chart if the team is specified and the player is not in the team.
Additional Notes
Given more time, I would have added more unit tests to cover more scenarios.
I would have tried to improve the ways of defining teams and different sports and make the DepthChart classes more generic.
I would have implemented more features such as updating player information, moving players between positions, print all team depth charts etc.
I think I would have implemented storage (database) into the library if I could justifying the need for it.
I would have implemented async await to make the code more efficient.
Design
The project is divided into the following classes:

Player: Represents a player with a name and position.
Team: Represents a team with a name, location, and list of players.
Sport: Represents a sport with a list of positions and maximum team member count.
NFL, MLB, NBA: Represent specific sports with their own positions and maximum team member counts.
DepthChartManager: Manages the depth chart by adding and removing players from positions.
DepthChartReporter: Reports the depth chart by printing it out.
DepthChartBuilder.Sample: Contains the main method to run the program.
DepthChartManagerTests: Contains unit tests for the DepthChartManager class.
DepthChartReporterTests: Contains unit tests for the DepthChartReporter class.
Testing
To run the tests, use the following command: dotnet test
