using UnityEngine;
using UnityEngine.AI;

public class EnemieMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position);
        }
        
    }
}