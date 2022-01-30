using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ButtonManager : MonoBehaviour
{
    public GameObject desktopApp;
    public GameObject App1;
    public GameObject App2;
    public GameObject App3;

    void Start()
    {
        desktopApp.SetActive(false);
        App1.SetActive(false);
        App2.SetActive(false);
        App3.SetActive(false);
    }

    public void openApp1()
    {
        baseApp();
        App1.SetActive(true);

    }

    public void openApp2()
    {
        baseApp();
        App2.SetActive(true);

    }

    public void openApp3()
    {
        baseApp();
        App3.SetActive(true);

    }

    void baseApp()
    {
        desktopApp.SetActive(true);

    }

    public void closeApp()
    {
        desktopApp.SetActive(false);
        App1.SetActive(false);
        App2.SetActive(false);
        App3.SetActive(false);

        print("Close App");

    }
}
