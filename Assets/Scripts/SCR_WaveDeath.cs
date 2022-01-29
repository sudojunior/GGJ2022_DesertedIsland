using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WaveDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("oh");
        Destroy(collision.gameObject);

    }
}
