using System.Text.Json.Serialization;

namespace Questao2.Models
{
    public class FootballMatch
    {
        [JsonPropertyName("competition")]
        public string Competition { get; set; } = default!;

        [JsonPropertyName("year")]
        public int Year { get; set; } = default!;

        [JsonPropertyName("round")]
        public string Round { get; set; } = default!;

        [JsonPropertyName("team1")]
        public string Team1 { get; set; } = default!;

        [JsonPropertyName("team2")]
        public string Team2 { get; set; } = default!;

        [JsonPropertyName("team1goals")]
        public string Team1Goals { get; set; } = default!;

        [JsonPropertyName("team2goals")]
        public string Team2Goals { get; set; } = default!;

        [JsonIgnore]
        public const string HOME_TEAM_ATTRIBUTE_NAME = "team1";

        [JsonIgnore]
        public const string VISITOR_TEAM_ATTRIBUTE_NAME = "team2";
    }
}
