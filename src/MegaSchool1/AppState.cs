using MegaSchool1.Model.Repository;

namespace MegaSchool1;

public class AppState
{
    public TeamMember? User { get; set; }

    public bool AppIsInstallable { get; set; }
}