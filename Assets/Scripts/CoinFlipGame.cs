using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoinFlipGame : MonoBehaviour
{
    // Start is called before the first frame update

    public float power = 10;

    public Rigidbody rb;
    // public GameObject coinObject;

    public CoinState state;

    bool flipped = false;
    float timeAcc = 0;

    public enum CoinState
    {
        None,
        Heads,
        Tails,
        Edge
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (flipped)
        {
            timeAcc += Time.fixedDeltaTime;
            if (Vector3.Distance(rb.velocity, Vector3.zero) > 0.1f)
                return; // still moving
            if (timeAcc >= 5)
            {
                state = DetermineOutcome();

                Debug.Log($"CoinState is {state}, {timeAcc}", gameObject);
                switch (state)
                {
                    case CoinState.None:
                        // still moving
                        timeAcc = 0;
                        break;

                    default:
                        flipped = false;
                        timeAcc = 0;
                        break;
                }
            }
        }
    }

    public void FlipCoin()
    {
        flipped = true;
        // transform.position = new Vector3(0, 0.2f, 0);
        // transform.rotation = Quaternion.identity;

        rb.AddForce((Random.value + 0.5f) * power * transform.up);
        rb.AddTorque(Random.onUnitSphere * power, ForceMode.Force);
    }


    private CoinState DetermineOutcome()
    {
        if (Vector3.Distance(rb.velocity, Vector3.zero) > 0.1f)
            return CoinState.None;
        if (Physics.Raycast(transform.position, transform.up.normalized, 0.1f))
            return CoinState.Heads;
        else if (Physics.Raycast(transform.position, -transform.up.normalized, 0.1f))
            return CoinState.Tails;
        else
            return CoinState.Edge;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.up.normalized * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, -transform.up.normalized * 2);
    }
}
