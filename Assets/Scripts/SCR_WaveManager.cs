using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_WaveManager : MonoBehaviour
{
    public GameObject waveHolder;
    private GameObject wave1;
    private float objectY;
    private bool cloneSpawned;

    void Start()
    {
        cloneSpawned = false;
        objectY = this.transform.position.y;
        wave1 = this.gameObject;
    }

    void Update()
    {
        if (this.transform.position.x >= 240 & !cloneSpawned)
        {
            spawnWave();
        }
    }

    void spawnWave()
    {
        Instantiate(wave1, new Vector2(-1920f, objectY), Quaternion.identity, waveHolder.transform);
        //wave1.transform.SetParent(waveHolder.transform);
        cloneSpawned = true;

    }
}
