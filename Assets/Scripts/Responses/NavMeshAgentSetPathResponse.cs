using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NavMeshAgentSetPathResponse : Response
{
    UnityEngine.AI.NavMeshAgent agent;
    UnityEngine.AI.NavMeshPath path;

    void Awake()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override void Execute()
    {
        // create a Raycast and set it to the mouses cursor position in game
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            path = new UnityEngine.AI.NavMeshPath();
            if (agent.CalculatePath(hit.point, path))
            {
                agent.SetPath(path);
            }
            else
            {
                path = null;
            }
        }
    }
}
