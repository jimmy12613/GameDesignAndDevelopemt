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
    public Text endText;
    string loggedInPlayfabId;

    // Start is called before the first frame update
    void Awake()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        print("Login successful!");
        print(result.InfoResultPayload.PlayerProfile.DisplayName);
        loggedInPlayfabId = result.PlayFabId;
        if (GameObject.Find("PlayerName") != null)
        {
            Text playerName = GameObject.Find("PlayerName").GetComponent<Text>();

            if (!string.IsNullOrEmpty(result.InfoResultPayload.PlayerProfile.DisplayName))
            {
                Singleton.Instance.setPlayerName(result.InfoResultPayload.PlayerProfile.DisplayName);
                if (!string.IsNullOrEmpty(Singleton.Instance.getPlayerName()))
                {
                    playerName.text = "Hi, " + Singleton.Instance.getPlayerName();
                } else
                {
                    playerName.text = null;
                }
            } else
            {
                Singleton.Instance.setPlayerName(null);
                playerName.text = null;
            }
        }
        GetPlayerLevelStatus();
    }

    public void UpdatePlayerName(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest {
            DisplayName = name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdatePlayerNameSuccess, OnError);
    }
    void OnUpdatePlayerNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Update player name successful!");
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
        foreach (Transform child in level1RowParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject row = Instantiate(Row, level1RowParent);
            string statValue = TimeSpan.FromSeconds(item.StatValue / -100f).ToString("mm':'ss'.'ff");
            Debug.Log(item.Position + " " + item.PlayFabId + " " + statValue);

            string playerName = item.DisplayName;
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Anonymous";
            }

            Text[] texts = row.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = playerName;
            texts[2].text = statValue;
        }
    }

    void OnGetLevel2LeaderBoardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Successfully retrieved level 2 leaderboard.");
        foreach (Transform child in level2RowParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject row = Instantiate(Row, level2RowParent);

            string statValue = TimeSpan.FromSeconds(item.StatValue / -100f).ToString("mm':'ss'.'ff");
            Debug.Log(item.Position + " " + item.PlayFabId + " " + statValue);

            string playerName = item.DisplayName;
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "Anonymous";
            }

            Text[] texts = row.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position+1).ToString();
            texts[1].text = playerName;
            texts[2].text = statValue;
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

    public void specificUserOnLeaderBoard()
    {
        var request = new GetLeaderboardAroundPlayerRequest {
            StatisticName = "Level1_Time",
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetSpecificUserOnLeaderBoardSuccess, OnError);
    }

    void OnGetSpecificUserOnLeaderBoardSuccess(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            if (item.PlayFabId == loggedInPlayfabId)
            {
                endText.text = endText.text + ", Your Place: " + (item.Position+1).ToString();
            }
            print(endText.text);
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