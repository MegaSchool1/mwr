using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public class ArkNationFastStartChecklist
{
    [JsonPropertyName("groupme_subscription")]
    public bool SubscribedToGroupMe { get; set; }
}
