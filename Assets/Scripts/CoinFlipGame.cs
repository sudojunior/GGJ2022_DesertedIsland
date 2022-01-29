using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool IsMoving
    {
        get => (
            Vector3.Distance(rb.velocity, Vector3.zero) > 0.1f ||
            Vector3.Angle(rb.angularVelocity, Vector3.zero) > 0.1f
        );
    }

    public enum CoinState
    {
        Moving,
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
            if (IsMoving)
                return; // still moving
            if (timeAcc >= 5)
            {
                state = DetermineOutcome();

                Debug.Log($"CoinState is {state}, {timeAcc}", gameObject);
                switch (state)
                {
                    case CoinState.Moving:
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
        Vector3 direction = state == CoinState.Tails ? -transform.up : transform.up;
        rb.AddForce((Random.value + 0.5f) * power * direction);
        rb.AddTorque(Random.onUnitSphere * power, ForceMode.Force);
    }


    private CoinState DetermineOutcome()
    {
        Debug.Log($"{rb.velocity}, {rb.angularVelocity}");
        if (IsMoving)
            return CoinState.Moving;
        else if (Physics.Raycast(transform.position, transform.up.normalized, 1f))
            return CoinState.Heads;
        else if (Physics.Raycast(transform.position, -transform.up.normalized, 1f))
            return CoinState.Tails;
        else
            return CoinState.Edge;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.up * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, -transform.up * 2);
    }
}
