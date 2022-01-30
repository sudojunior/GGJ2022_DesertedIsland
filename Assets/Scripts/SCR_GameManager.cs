using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_GameManager : MonoBehaviour
{
    public bool isDaytime; //Tracks if its day or night
    public int playerBal; //Stores player's money count
    public int daysSurvived; //Counts the amount of fdays the player has survived
    public TMP_Text balTextSlot;

    public Animator dayNightAnim;

    void Start()
    {
        dayNightAnim.SetBool("IsDay", true);

        isDaytime = true;
        playerBal = 2;
        daysSurvived = 0;

    }

    void Update()
    {
        if (balTextSlot.text != playerBal.ToString())
        {
            balTextSlot.text = playerBal.ToString();
        }
    }

    public void RewardPlayer()
    {
        int reward = Random.Range(10, 40);
        playerBal += Mathf.RoundToInt(reward);
    }

    public void endDay() //call when end of day is pressed
    {
        dayNightAnim.SetBool("IsDay", false);

        isDaytime = false;

    }

    public void startDay() //call when night is over
    {
        dayNightAnim.SetBool("IsDay", true);

        isDaytime = true;
        daysSurvived = daysSurvived + 1;
    }
}
