using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public GameObject body;
    public Transform groundCheck;

    public float powerDecayRate;
    float decayTimer;

    public int maxPower;
    public float maxSpeed;
    public float maxJump;

    public Color fullColor;
    public Color emptyColor;

    public float jumpHeight;

    public int layer = 8;

    Animator animator;
    Renderer[] playerRenderers;

    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerRenderers = GetComponentsInChildren<Renderer>();
        gameObject.layer = layer;
        decayTimer = 0;
    }

    void Update()
    {
        DecayPower();
        UpdateAgent();
        SetColor();
    }

    void DecayPower()
    {
        decayTimer += Time.deltaTime;
        if (decayTimer >= powerDecayRate)
        {
            decayTimer = 0;
            IntData[] intDataArr = this.GetComponents<IntData>();
            foreach (IntData intData in intDataArr)
            {
                if (intData.Name == "Power")
                {
                    intData.data -= 1;
                    break;
                }
            }
        }
    }

    void UpdateAgent()
    {
        if (animator)
        {
            int power = 0;
            IntData[] intDataArr = this.GetComponents<IntData>();
            foreach (IntData intData in intDataArr)
            {
                if (intData.Name == "Power")
                {
                    power = intData.data;
                    break;
                }
            }
            if (power >= maxPower)
            {
                power = maxPower;
            }
            float speed = ((float)power / (float)maxPower) * maxSpeed;
            float jump = ((float)power / (float)maxPower);
            agent.speed = speed;
            float curveHeight = animator.GetFloat("jumpHeight");
            Vector3 jumpOffset = new Vector3(0.0f, jump * curveHeight * jumpHeight, 0.0f);
            this.transform.position += jumpOffset;

            animator.SetFloat("moveSpeed", Vector3.Magnitude(agent.velocity));
        }
    }

    void SetColor()
    {
        int power = 0;
        IntData[] intDataArr = this.GetComponents<IntData>();
        foreach (IntData intData in intDataArr)
        {
            if (intData.Name == "Power")
            {
                power = intData.data;
                break;
            }
        }
        if (power >= maxPower)
        {
            power = maxPower;
        }
        float lerpage = ((float)power / (float)maxPower);

        foreach (Renderer r in playerRenderers)
        {
            r.material.color = Color.Lerp(emptyColor, fullColor, lerpage);
        }
        
    }

    public void Jump()
    {
        if (Physics.Raycast(body.transform.position, Vector3.down, Vector3.Distance(body.transform.position, groundCheck.position)))
        {
            if (animator)
            {
                animator.SetTrigger("jump");
            }
        }
    }
}
