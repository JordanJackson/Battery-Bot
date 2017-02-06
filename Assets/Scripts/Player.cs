using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Color fullEmissionColor;
    public Color emptyEmissinoColor;

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

    private void Start()
    {
        IntData[] intDataArr = this.GetComponents<IntData>();
        foreach (IntData intData in intDataArr)
        {
            if (intData.Name == "Power")
            {
                intData.data = maxPower / 2;
                break;
            }
        }
    }

    void Update()
    {
        SetColor();
        UpdateAgent();
        DecayPower();
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
                    if (intData.data <= 0)
                    {
                        LevelManager.Instance.LoadNextLevel();
                    }
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
            if (r.gameObject.name == "Alpha_Surface")
            {
                r.material.color = Color.Lerp(emptyColor, fullColor, lerpage);
                r.material.SetColor("_EmissionColor", Color.Lerp(emptyEmissinoColor, fullEmissionColor, lerpage));
            }
            
        }
        
    }

    public void Jump()
    {
        if (Physics.Raycast(body.transform.position, Vector3.down, Vector3.Distance(body.transform.position, groundCheck.position)))
        {
            if (animator)
            {
                animator.SetTrigger("jump");
                animator.ResetTrigger("jump");
            }
        }
    }
}
