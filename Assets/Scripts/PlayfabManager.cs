using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public GameObject Row;
    public Transform level1RowParent;
    public Transform level2RowParent;

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

    public void GetLevel1LeaderBoard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "Level1_Time",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLevel1LeaderBoardSuccess, OnGetLeaderBoardError);
    }

    public void GetLevel2LeaderBoard()
    {
        var request = new GetLeaderboardRequest {
            StatisticName = "Level2_Time",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLevel2LeaderBoardSuccess, OnGetLeaderBoardError);
    }

    void OnGetLevel1LeaderBoardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Successfully retrieved level 1 leaderboard.");
        foreach (var item in result.Leaderboard)
        {
            GameObject row = Instantiate(Row, level1RowParent);

            string statValue = TimeSpan.FromMilliseconds(item.StatValue * -100).ToString("mm':'ss'.'ff");
            Debug.Log(item.Position + " " + item.PlayFabId + " " + statValue);

            Text[] texts = row.GetComponentsInChildren<Text>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = statValue;
        }
    }

    void OnGetLevel2LeaderBoardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Successfully retrieved level 2 leaderboard.");
        foreach (var item in result.Leaderboard)
        {
            GameObject row = Instantiate(Row, level2RowParent);

            string statValue = TimeSpan.FromMilliseconds(item.StatValue * -100).ToString("mm':'ss'.'ff");
            Debug.Log(item.Position + " " + item.PlayFabId + " " + statValue);

            TextMesh[] texts = row.GetComponentsInChildren<TextMesh>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = TimeSpan.FromSeconds(item.StatValue *-100).ToString("mm':'ss'.'ff");
        }
    }

    public void SavePlayerLevelStatus()
    {
        var request = new UpdateUserDataRequest {
            Data = new Dictionary<string, string> {
                {"level1Status", Singleton.Instance.getLevel1Status().ToString()},
                {"level2Status", Singleton.Instance.getLevel2Status().ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSavePlayerLevelStatusSuccess, OnSavePlayerLevelStatusError);
    }
    public void GetPlayerLevelStatus()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnLoadPlayerLevelStatusSuccess, OnLoadPlayerLevelStatusError);
    }

    void OnLoadPlayerLevelStatusSuccess(GetUserDataResult result)
    {
        Debug.Log("Successfully loaded player level stats.");
        if (result.Data != null  && result.Data.ContainsKey("level1Status") && result.Data.ContainsKey("level2Status"))
        {
            Singleton.Instance.setLevel1Status(bool.Parse(result.Data["level1Status"].Value));
            Singleton.Instance.setLevel2Status(bool.Parse(result.Data["level2Status"].Value));
        }else
        {
            Debug.Log("Player level stats not found.");
            Singleton.Instance.setLevel1Status(false);
            Singleton.Instance.setLevel2Status(false);
        }
        
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

    void OnUpdateScoreSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully updated player score");
    }

    void OnUpdateScoreError(PlayFabError error)
    {
        Debug.Log("Error while updating player score.");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnGetLeaderBoardError(PlayFabError error)
    {
        Debug.Log("Error while retrieving leaderboard.");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnSavePlayerLevelStatusSuccess(UpdateUserDataResult result)
    {
        Debug.Log("Successfully saved player level status.");
    }

    void OnSavePlayerLevelStatusError(PlayFabError error)
    {
        Debug.Log("Error while saving player level status.");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLoadPlayerLevelStatusError(PlayFabError error)
    {
        Debug.Log("Error while loading player level stats.");
        Debug.Log(error.GenerateErrorReport());
    }
}