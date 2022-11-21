using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject MiniMapCamera1;
    public GameObject MiniMapCamera2;

    public void ClickZoomButton()
    {
        MiniMapCamera1.SetActive(!MiniMapCamera1.activeSelf);
        MiniMapCamera2.SetActive(!MiniMapCamera2.activeSelf);
    }
}
