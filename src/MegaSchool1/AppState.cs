using MegaSchool1.Repository.Model;

namespace MegaSchool1;

public class AppState
{
    public TeamMember? User { get; set; }

    public bool AppIsInstallable { get; set; }
}