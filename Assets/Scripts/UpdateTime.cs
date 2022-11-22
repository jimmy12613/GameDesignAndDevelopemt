using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateTime : MonoBehaviour
{
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Wait for 1 seconds
        yield return new WaitForSeconds(1);

        GetComponent<Text>().text = "";
        GetComponent<Text>().text = "Your Time: " + timeText.text;
        GameObject.Find("Menu").GetComponent<PlayfabManager>().specificUserOnLeaderBoard();
    }
}