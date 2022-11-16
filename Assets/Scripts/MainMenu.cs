using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LeaderBoardScene;
    public GameObject StartMenu;
    PlayfabManager playfabManager;

    void Start()
    {
        playfabManager = GetComponent<PlayfabManager>();
    }
    
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }

    public void LeaderBoard()
    {
        LeaderBoardScene.SetActive(true);
        playfabManager.GetLevel1LeaderBoard();
        playfabManager.GetLevel2LeaderBoard();
    }

    public void GoToStartMenu()
    {
        // Go to Scene "StartMenu"
        SceneManager.LoadScene("StartMenu");
    }

    public void BackToMenu()
    {
        // Go to Scene "Menu"
        SceneManager.LoadScene("Menu");
    }

    public void GoToLevel1()
    {
        // Go to Scene "Level1"
        SceneManager.LoadScene("Level1");
    }

    public void GoToLevel2()
    {
        // Go to Scene "Level2"
        SceneManager.LoadScene("Level2");
    }
}
