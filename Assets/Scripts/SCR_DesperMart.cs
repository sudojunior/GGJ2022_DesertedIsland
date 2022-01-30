using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_DesperMart : MonoBehaviour
{
    public Image DroneSpriteSlot;
    public Animator animController;

    public GameObject trouserOverlay;
    public GameObject characterLegs;
    public GameObject transactionOverlay;

    public Sprite LogsImage;
    public Sprite CanImage;
    public Sprite TrousersImage;

    public SCR_GameManager gameManager;

    public void BuyLogs()
    {
        float currentTemperature = gameManager.playerTemperature;
        BuyItem(300, LogsImage, (delta) =>
        {
            gameManager.playerTemperature = Mathf.SmoothStep(currentTemperature, 100, delta / 4);
        });
    }

    public void BuyCannedBrad() {
        float currentHunger = gameManager.playerHunger;
        BuyItem(80, CanImage, (delta) =>
        {
            gameManager.playerHunger = Mathf.SmoothStep(currentHunger, 100, delta / 4);
        });

        gameManager.playerHunger = 100;
    }

    public void BuyTrousers()
    {
        BuyItem(1, TrousersImage, (delta) => { });

        trouserOverlay.SetActive(true);
        characterLegs.SetActive(true);
    }

    public void BuyItem(int cost, Sprite image, System.Action<float> deltaCallback)
    {
        if (gameManager.playerBal - cost < 0)
        {
            return;
        }

        transactionOverlay.SetActive(true);

        animController.SetBool("ItemBought", true);
        DroneSpriteSlot.sprite = image;
        StartCoroutine(ClearOnExit(cost, deltaCallback));
    }

    IEnumerator ClearOnExit(int cost, System.Action<float> deltaCallback)
    {
        float delta = 0;
        int currentBal = gameManager.playerBal;
        while (delta <= 4f)
        {
            if (delta >= 1f)
            {
                animController.SetBool("ItemBought", false);
            }
            delta += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            gameManager.playerBal = Mathf.RoundToInt(Mathf.SmoothStep(currentBal, currentBal - cost, delta / 4));
            deltaCallback(delta);
        }

        DroneSpriteSlot.sprite = null;

        gameManager.disableShopOverlay();
    }
}
