using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public GameObject body;
    public Transform groundCheck;

    Animator animator;

    UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (animator)
        {
            int speed = 0;
            IntData[] intDataArr = this.GetComponents<IntData>();
            foreach (IntData intData in intDataArr)
            {
                if (intData.Name == "Power")
                {
                    speed = intData.data;
                    break;
                }
            }
            agent.speed = speed;

            animator.SetFloat("moveSpeed", Vector3.Magnitude(agent.velocity));
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
