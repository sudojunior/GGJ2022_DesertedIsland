using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SCR_GameManager : MonoBehaviour
{
    public bool isDaytime; //Tracks if its day or night
    public int playerBal; //Stores player's money count
    public int daysSurvived; //Counts the amount of fdays the player has survived
    public TMP_Text balTextSlot;

    public SCR_ButtonManager buttonManager;
    public CoinFlipGame coinFlipGame;

    public Animator dayNightAnim;
    public Animator arm1Anim;
    public Animator arm2Anim;
    public Animator laptopAnim;
    public Animator monitorOverlayAnim;

    public Animator animController;
    public GameObject transactionOverlay;

    public SurveyHandler surveyHandler;

    public Slider hungerSlider;
    public Slider temperatureSlider;

    public float playerHunger;

    public TMP_Text surviveText;
    private float playerTemperature;

    void Start()
    {
        dayNightAnim.SetBool("IsDay", true);

        surviveText.SetText("Days Survived: {0}", daysSurvived);

        isDaytime = true;
        playerBal = 2;
        daysSurvived = 0;
        playerHunger = 100;
        playerTemperature = 100;
    }

    void Update()
    {
        if (balTextSlot.text != playerBal.ToString())
        {
            balTextSlot.text = playerBal.ToString();
        }

        if (hungerSlider.value != playerHunger / 100)
        {
            hungerSlider.value = playerHunger / 100;
        }

        if (temperatureSlider.value != playerTemperature / 100)
        {
            temperatureSlider.value = playerTemperature / 100;
        }
    }

    public void RewardPlayer()
    {
        int reward = Random.Range(10, 40);
        playerBal += Mathf.RoundToInt(reward);
    }

    public void endDay() //call when end of day is pressed
    {
        StartCoroutine(NightTimer());

        buttonManager.closeApp();

        dayNightAnim.SetBool("IsDay", false);
        laptopAnim.SetBool("IsDay", false);
        monitorOverlayAnim.SetBool("CompOn", false);

        isDaytime = false;

    }

    public void startDay() //call when night is over
    {
        dayNightAnim.SetBool("IsDay", true);
        laptopAnim.SetBool("IsDay", true);
        //arm2Anim.SetBool("PCInteract", true);
        monitorOverlayAnim.SetBool("CompOn", true);
        surveyHandler.OnReset();

        isDaytime = true;
        daysSurvived = daysSurvived + 1;

        surviveText.SetText("Days Survived: {0}", daysSurvived);

        coinFlipGame.coinFlipReset();
    }

    public void disableShopOverlay()
    {
        animController.SetBool("ItemBought", false);

        transactionOverlay.SetActive(false);
    }

    IEnumerator NightTimer()
    {
        float delta = 0;
        float currentHunger = playerHunger;
        float takeHunger = Random.Range(30f, 60f);

        float currentTemperature = playerTemperature;

        while (delta <= 5f)
        {
            float targetHunger = Mathf.Max(currentHunger - takeHunger, 0);
            playerHunger = Mathf.SmoothStep(currentHunger, targetHunger, delta / 5);

            playerTemperature = Mathf.SmoothStep(currentTemperature, currentTemperature - 20, delta / 5);

            delta += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        if (playerHunger == 0/* || temperature == 0*/)
        {
            // game over
            Debug.Log("Game Over");
        }

        startDay();
    }
}
