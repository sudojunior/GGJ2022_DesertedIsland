using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LoadingScript : MonoBehaviour
{
    public GameObject loadingOverlay;

    void Start()
    {
        loadingOverlay.SetActive(true);
        StartCoroutine(LoadingTimer());
    }

    void Update()
    {
        
    }

    IEnumerator LoadingTimer()
    {
        yield return new WaitForSeconds(12f);
        loadingOverlay.SetActive(false);
    }
}
