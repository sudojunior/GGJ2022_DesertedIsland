using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GameManager : MonoBehaviour
{
    public bool isDaytime; //Tracks if its day or night
    public int playerBal; //Stores player's money count
    public int daysSurvived; //Counts the amount of fdays the player has survived

    public SCR_ButtonManager buttonManager;

    public Animator dayNightAnim;
    public Animator arm1Anim;
    public Animator arm2Anim;
    public Animator laptopAnim;
    public Animator monitorOverlayAnim;

    void Start()
    {
        dayNightAnim.SetBool("IsDay", true);

        isDaytime = true;
        playerBal = 2;
        daysSurvived = 0;

    }

    void Update()
    {
        
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

        isDaytime = true;
        daysSurvived = daysSurvived + 1;
    }

    IEnumerator NightTimer()
    {
        yield return new WaitForSeconds(5f);

        startDay();
    }
}
