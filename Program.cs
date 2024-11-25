// See https://aka.ms/new-console-template for more information

using ArrangeTeam;

const int ODD = 100;

List<Player> upperGuide = new List<Player>();
List<Player> lowerGuide = new List<Player>();

try
{
	//var curDir = Directory.GetCurrentDirectory();
	StreamReader sr = new StreamReader("../../../Input.txt");

	var line = sr.ReadLine();
	var guide = line?.Split(' ');
	int upperCount = int.Parse(guide[0]);
	int lowerCount = int.Parse(guide[1]);
    line = sr.ReadLine();
    while (line != null)
	{
		var tempInput = line.Split(' ');
		var player = new Player(tempInput[0], int.Parse(tempInput[1]), int.Parse(tempInput[2]));
		if (player.Position == 1)
		{
			upperGuide.Add(player);
		}
		else 
		{ 
			lowerGuide.Add(player); 
		}

		line = sr.ReadLine();
	}
	sr.Close();
    Arrange(lowerGuide, lowerCount);
}
catch (Exception)
{

	throw;
}


void Arrange(List<Player> players, int numOfGuide)
{
    // Sort objects by value in descending order
    players = players.OrderByDescending(o => o.Power).ToList();

    // Initialize 4 groups
    List<List<Player>> groups = new List<List<Player>>();
    for (int i = 0; i < numOfGuide; i++)
    {
        groups.Add(new List<Player>());
    }
    int avg = players.Sum(x => x.Power) / numOfGuide;

    // Variables to track the best solution
    List<List<Player>> bestGroups = null;
    int bestDifference = int.MaxValue;

    // Start the backtracking process
    int[] groupSums = new int[numOfGuide]; // Track group totals
    BackTracking(players, groups, groupSums, ref bestDifference, ref bestGroups, avg, 0);
    if(bestGroups == null)
    {
        Console.WriteLine("Cannot arrange");
        return;
    }

    // Output results
    for (int i = 0; i < bestGroups.Count; i++)
    {
        Console.WriteLine($"Group {i + 1}:");
        foreach (var obj in bestGroups[i])
        {
            Console.WriteLine($"  {obj.Name}: {obj.Power}");
        }
        Console.WriteLine($"  Total: {bestGroups[i].Sum(o => o.Power)}");
    }

}


void BackTracking(List<Player> players, List<List<Player>> groups, int[] groupSums, ref int bestDifference, ref List<List<Player>> bestGroups, int avg, int index)
{
    if (index == players.Count)
    {
        // Calculate the current difference between max and min group totals
        int minSum = groupSums.Min();
        int maxSum = groupSums.Max();
        int difference = maxSum - minSum;

        // Update the best solution if this one is better
        if (difference < bestDifference)
        {
            bestDifference = difference;
            bestGroups = groups.Select(g => new List<Player>(g)).ToList();
        }
        return;
    }

    // Try adding the current object to each group
    for (int i = 0; i < groups.Count; i++)
    {
        if (groupSums[i] + players[index].Power > avg + ODD)
            continue;
        groups[i].Add(players[index]);
        groupSums[i] += players[index].Power;
        

        // Backtracking: Go to the next object
        BackTracking(players, groups, groupSums, ref bestDifference, ref bestGroups, avg, index + 1);

        // Undo the addition (backtrack)
        groups[i].RemoveAt(groups[i].Count - 1);
        groupSums[i] -= players[index].Power;
    }


}
