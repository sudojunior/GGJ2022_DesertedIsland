using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ButtonManager : MonoBehaviour
{
    public GameObject desktopApp;
    public GameObject App1;
    public GameObject App2;
    public GameObject App3;

    public Animator droneAnim;

    public SCR_GameManager gameManager;

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
        App3.GetComponent<SurveyHandler>().OnSurveyStart();
    }

    void baseApp()
    {
        desktopApp.SetActive(true);

    }

    public void closeApp()
    {
        droneAnim.SetBool("ItemBought", false);

        gameManager.disableShopOverlay();

        desktopApp.SetActive(false);
        App1.SetActive(false);
        App2.SetActive(false);
        App3.SetActive(false);

        print("Close App");

    }

    public void dayEnd()
    {
        print("End day");
        App3.GetComponent<SurveyHandler>().OnReset();
    }
}
