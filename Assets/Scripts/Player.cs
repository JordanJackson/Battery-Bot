using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public GameObject body;
    public Transform groundCheck;

    public int maxPower;
    public float maxSpeed;
    public float maxJump;

    public Color fullColor;
    public Color emptyColor;

    Animator animator;
    Renderer[] playerRenderers;

    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerRenderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        UpdateAgent();
        SetColor();
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
            agent.speed = speed;

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
