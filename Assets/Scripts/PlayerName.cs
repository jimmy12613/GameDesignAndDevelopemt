using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public InputField input;
    private MainMenu mainMenu;

    void Start()
    {
        mainMenu = GetComponent<MainMenu>();
    }

    void Update()
    {
        if (string.IsNullOrEmpty(Singleton.Instance.getPlayerName()))
        {
            GameObject.Find("PlayerName").GetComponent<Text>().text = null;
        } else
        {
            GameObject.Find("PlayerName").GetComponent<Text>().text = "Hi, " + Singleton.Instance.getPlayerName();
        }
    }
   
    public void setNameLevel1()
    {
        Singleton.Instance.setPlayerName(input.text);
        Debug.Log(Singleton.Instance.getPlayerName());
        GetComponent<PlayfabManager>().UpdatePlayerName(input.text);
        mainMenu.GoToLevel1();
    }

    public void setNameLevel2()
    {
        Singleton.Instance.setPlayerName(input.text);
        Debug.Log(Singleton.Instance.getPlayerName());
        GetComponent<PlayfabManager>().UpdatePlayerName(input.text);
        mainMenu.GoToLevel2();
    }
}