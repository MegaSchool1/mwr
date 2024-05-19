namespace MegaSchool1.Model;

public record ProductionDuplicationLevel(int PersonalMemberEnrollments, ProductionDuplicationLevel? PreviousProgress)
{
    public int TeamMembershipEnrollments { get; init; }
        =
        PersonalMemberEnrollments
        +
        // Downline membership enrollments (the entire team got 1 enrollment)
        (PreviousProgress?.TeamMembershipEnrollments ?? 0)
        +
        // Previous team membersip enrollments
        (PreviousProgress?.TeamMembershipEnrollments ?? 0);
}
