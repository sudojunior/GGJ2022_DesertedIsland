using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_DesperMart : MonoBehaviour
{
    public Image DroneSpriteSlot;
    public Animator animController;

    public Sprite LogsImage;
    public Sprite CanImage;
    public Sprite TrousersImage;

    public void BuyLogs() => BuyItem(100, LogsImage);

    public void BuyCannedBrad() => BuyItem(100, CanImage);

    public void BuyTrousers() => BuyItem(100, TrousersImage);

    public void BuyItem(int cost, Sprite image)
    {
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
    }
}
