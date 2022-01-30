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

    public void BuyLogs() => BuyItem(100, LogsImage);

    public void BuyCannedBrad() => BuyItem(100, CanImage);

    public void BuyTrousers()
    {
        BuyItem(100, TrousersImage);

        trouserOverlay.SetActive(true);
        characterLegs.SetActive(true);
    }

    public void BuyItem(int cost, Sprite image)
    {
        transactionOverlay.SetActive(true);

        // cost
        animController.SetBool("ItemBought", true);
        DroneSpriteSlot.sprite = image;
        StartCoroutine(ClearOnExit());
    }

    IEnumerator ClearOnExit()
    {
        yield return new WaitForSeconds(4f);

        DroneSpriteSlot.sprite = null;
        animController.SetBool("ItemBought", false);

        transactionOverlay.SetActive(false);
    }
}
