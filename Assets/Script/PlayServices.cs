using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayServices : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate(succes => { });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PostScore(long score, string leaderBoard)
    {
        Social.ReportScore(score, leaderBoard, (success => { }));
    }

    public static void ShowLeaderboard(string leaderboard)
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboard);
    }

    public static long GetPlayerScore(string learderboard)
    {
        long score = 0;
        PlayGamesPlatform.Instance.LoadScores(learderboard, LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime, (LeaderboardScoreData data) => { score = data.PlayerScore.value; });
        return score;
    }
}
