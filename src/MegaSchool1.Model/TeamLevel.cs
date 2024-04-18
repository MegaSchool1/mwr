namespace MegaSchool1.Model;

public class TeamLevel
{
    private readonly TeamLevel? _previousLevel;

    public TeamLevel(string name, int personalEnrollments, TeamLevel? previousLevel)
    {
        _previousLevel = previousLevel;
        Name = name;
        PersonalEnrollments = personalEnrollments;
        QualifiedMarketingDirectorTotal = personalEnrollments * _previousLevel?.QualifiedMarketingDirectorTotal ?? personalEnrollments;

        // every QMD should have 1 customer
        CustomerTotal = QualifiedMarketingDirectorTotal;
    }

    public string Name { get; }
    public int PersonalEnrollments { get; }
    public int QualifiedMarketingDirectorTotal { get; }
    public int CustomerTotal { get; }
    public int TeamMembersTotal => QualifiedMarketingDirectorTotal + CustomerTotal + (_previousLevel?.TeamMembersTotal ?? 0);

    public static TeamLevel[] GetTeamDistribution(int[] enrollmentDistribution)
    {
        var teamDistribution = new List<TeamLevel>();

        for (var i = 0; i < enrollmentDistribution.Length; i++)
        {
            if (i == 0)
            {
                teamDistribution.Add(new("You", enrollmentDistribution[i], null));
            }
            else
            {
                teamDistribution.Add(new($"Level {i + 1}", enrollmentDistribution[i], teamDistribution[i - 1]));
            }
        }

        return teamDistribution.ToArray();
    }
}
