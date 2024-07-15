using BaseTemplate.Behaviours;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GoogleManager : MonoSingleton<GoogleManager>
{

    public void SubmitScore(int score)
    {
        // Post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore(score, "Cfji293fjsie_QA", (bool success) =>
        {
            // Handle success or failure
        });
    }

    public void HandleShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }


}


