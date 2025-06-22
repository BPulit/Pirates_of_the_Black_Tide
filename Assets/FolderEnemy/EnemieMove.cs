using UnityEngine;
using UnityEngine.AI;

public class EnemieMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Parâmetros de Movimento")]
    public float lateralOffsetDist = 15f; // Distância lateral ideal em relação ao player
    public float rotacaoSuavidade = 2f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // A rotação será controlada manualmente
    }

    void Update()
    {
        if (agent.isOnNavMesh && player != null)
        {
            // Direção do player (ignora o Y)
            Vector3 dirToPlayer = player.position - transform.position;
            dirToPlayer.y = 0f;

            // Calcula um ponto ao lado do jogador (flanco direito neste caso)
            Vector3 offset = Vector3.Cross(Vector3.up, dirToPlayer.normalized) * lateralOffsetDist;
            Vector3 destinoFlanco = player.position + offset;

            // Move o inimigo para esse ponto lateral
            agent.SetDestination(destinoFlanco);

            // Faz o inimigo olhar na direção do movimento (sempre com casco à frente)
            Vector3 dirMovimento = agent.desiredVelocity;
            if (dirMovimento.sqrMagnitude > 0.01f)
            {
                Quaternion rot = Quaternion.LookRotation(dirMovimento.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * rotacaoSuavidade);
            }
        }
    }
}
