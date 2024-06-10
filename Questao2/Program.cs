using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Questao2.Configurations;
using Questao2.External.Http;
using Questao2.Models;
using System.Text.Json;

public class Program
{
    static ServiceProvider? ServiceProvider { get; set; }

    public static void Main()
    {
        Initialize();

        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var config = ServiceProvider?.GetRequiredService<IConfiguration>();
        var request = new Request(config?.GetValue<string>("ApiEndpoint:FootballMatches")!, HttpMethod.Get);

        int totalScoredGoals = 0;

        void CalculateGoalsForTeamType(string teamType)
        {
            int requestPage = 1, totalPages = 1;

            while (requestPage <= totalPages)
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "page", requestPage.ToString() },
                    { "year", year.ToString() },
                    { teamType, team }
                };

                request.SetQueryParameters(queryParams);
                var httpResponse = new ApiCommunicationService().Get(request!).Result;
                var responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                var result = JsonSerializer.Deserialize<PagedResponseAPI<FootballMatch>>(responseBody);

                if (result != null)
                {
                    totalScoredGoals += result.Data.Sum(x => int.Parse(teamType == "team1" ? x.Team1Goals : x.Team2Goals));
                    totalPages = result.TotalPages;
                    requestPage++;
                }
                else
                {
                    if (!httpResponse.IsSuccessStatusCode)
                        throw new InvalidOperationException($"Failure while getting response from endpoint. Status Code: {httpResponse.StatusCode}");
                    break;
                }
            }
        }

        CalculateGoalsForTeamType(FootballMatch.HOME_TEAM_ATTRIBUTE_NAME);
        CalculateGoalsForTeamType(FootballMatch.VISITOR_TEAM_ATTRIBUTE_NAME);

        return totalScoredGoals;
    }

    static void Initialize()
    {
        IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                                                 .AddJsonFile("appsettings.json", optional: false)
                                                                 .Build();

        ServiceProvider = new ServiceCollection().AddSingleton(configuration)
                                                 .AddLogging()
                                                 .AddComunicacaoExterna()
                                                 .BuildServiceProvider();
    }

}