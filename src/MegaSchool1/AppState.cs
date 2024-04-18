namespace MegaSchool1;

public class AppState
{
    public const string DefaultUsername = "MS1";

    public string UserName { get; set; } = DefaultUsername;

    public bool AppIsInstallable { get; set; }
}