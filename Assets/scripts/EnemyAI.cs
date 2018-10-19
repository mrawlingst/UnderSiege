using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject goal;

    private NavMeshAgent agent;
    private bool arrived = false;

	void Start ()
    {
		agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.transform.position;
	}

    void Update()
    {
        if (!agent.pathPending && !arrived)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("Arrived");
                    arrived = true;
                    changeAnimation();
                }
            }
        }

        if (GetComponent<Enemy>().dead && !agent.isStopped)
        {
            agent.isStopped = true;
        }
    }

    void changeAnimation()
    {
        Animator anim = GetComponent<Animator>();
        anim.Play("Attack");
    }
}
