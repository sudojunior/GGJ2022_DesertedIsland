using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    private float distanceFromOrigin;

    public Vector3 origin = new Vector3(0, 1, 0);
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        distanceFromOrigin = Vector3.Distance(transform.position, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(origin, Vector3.up, Time.deltaTime * speed);
        transform.LookAt(Vector3.zero);
    }
}
