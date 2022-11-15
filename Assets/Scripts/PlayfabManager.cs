using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account create!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating a new account.");
        Debug.Log(error.GenerateErrorReport());
    }



    public void uploadLevel1Score(double score)
    {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Level1_Time",
                    Value = (int) Mathf.Floor((float) score*-100)
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateScoreSuccess, OnUpdateScoreError);
    }

    public void uploadLevel2Score(double score)
    {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "Level2_Time",
                    Value = (int) Mathf.Floor((float) score*-100)
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateScoreSuccess, OnUpdateScoreError);
    }

    void OnUpdateScoreSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully updated player score");
    }

    void OnUpdateScoreError(PlayFabError error)
    {
        Debug.Log("Error while updating player score.");
        Debug.Log(error.GenerateErrorReport());
    }

    


    public void GetLevel1LeaderBoard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "Level1_Time",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderBoardSuccess, OnGetLeaderBoardError);
    }

    public void GetLevel2LeaderBoard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "Level2_Time",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderBoardSuccess, OnGetLeaderBoardError);
    }

    void OnGetLeaderBoardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Successfully retrieved leaderboard.");
        foreach (var item in result.Leaderboard)
        {
            string statValue = TimeSpan.FromMilliseconds(item.StatValue * -10).ToString("mm':'ss'.'ff");
            Debug.Log(item.Position + " " + item.PlayFabId + " " + statValue);
        }
    }

    void OnGetLeaderBoardError(PlayFabError error)
    {
        Debug.Log("Error while retrieving leaderboard.");
        Debug.Log(error.GenerateErrorReport());
    }
}