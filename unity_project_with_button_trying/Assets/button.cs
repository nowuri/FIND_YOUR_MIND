using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject character1;   // Charactor1
    public GameObject character2;   // Charactor2

    public GameObject button1;      // button1
    public GameObject button2;      // button2
    public GameObject wallpaper;    // wallpaper

    public bool charLoad = false;


    void Start()
    {
        //if (character1 != null) character1.SetActive(true);
        //if (character2 != null) character2.SetActive(true);
        //if (wallpaper != null) wallpaper.SetActive(true);
        //if (button1 != null) button1.SetActive(true);
        //if (button2 != null) button2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendLogToAndroid(string logMessage)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("onUnityLogReceived", logMessage);

    }

    public void OnClick1()
    {
        UnityEngine.Debug.Log("***********Charactor1 is selected***********");
        SendLogToAndroid("Character1 selected");
        if (character2 != null) character2.SetActive(false);
        if (wallpaper != null) wallpaper.SetActive(false);
        if (button1 != null) button1.SetActive(false);
        if (button2 != null) button2.SetActive(false);
        charLoad = true;
    }
    public void OnClick2()
    {
        UnityEngine.Debug.Log("***********Charactor2 is selected***********");
        SendLogToAndroid("Character2 selected");
        if (character1 != null) character1.SetActive(false);
        if (wallpaper != null) wallpaper.SetActive(false);
        if (button1 != null) button1.SetActive(false);
        if (button2 != null) button2.SetActive(false);
        charLoad = true;
    }
}
