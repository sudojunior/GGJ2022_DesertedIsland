using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject cantPlayOverlay;

    public GameObject allInButton;
    public GameObject headsButton;
    public GameObject tailsButton;

    public TMP_Text poolText;
    public TMP_Text outcomeText;

    public int pool; //money pool
    public bool playerChoice;
    public bool canFlip;

    public SCR_GameManager gameManager;

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
                    case CoinState.Heads:
                        outcomeText.SetText("Heads!");

                        if (playerChoice)
                        {
                            pool = pool * 2;
                            gameManager.playerBal = pool;
                            pool = 0;

                            poolText.SetText("Pool:", pool);
                        }
                        else
                        {
                            gameManager.playerBal = 0;
                            pool = 0;

                            poolText.SetText("Pool:", pool);
                        }
                        break;

                    case CoinState.Tails:
                        outcomeText.SetText("Tails!");
                        if (!playerChoice)
                        {
                            pool = pool * 2;
                            gameManager.playerBal = pool;
                            pool = 0;

                            poolText.SetText("Pool:", pool);
                        }
                        else
                        {
                            gameManager.playerBal = 0;
                            pool = 0;

                            poolText.SetText("POOL:", pool);
                        }
                        break;

                    case CoinState.Edge:
                        pool = pool * 3;
                        gameManager.playerBal = pool;
                        pool = 0;

                        poolText.SetText("POOL:", pool);
                        break;

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

    public void gameInitialize()
    {
        pool = gameManager.playerBal;
        gameManager.playerBal = gameManager.playerBal - gameManager.playerBal;

        poolText.SetText("POOL:", pool);

        allInButton.SetActive(false);

        headsButton.SetActive(true);
        tailsButton.SetActive(true);
    }

    public void heads()
    {
        playerChoice = true;

        headsButton.SetActive(false);
        tailsButton.SetActive(false);

        FlipCoin();
    }

    public void tails()
    {
        playerChoice = false;

        headsButton.SetActive(false);
        tailsButton.SetActive(false);

        FlipCoin();
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

    public void coinFlipReset()
    {
        allInButton.SetActive(true);
    }
}
