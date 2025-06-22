using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemieMove : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    [Header("Configuração de Distância")]
    public float distanciaFlanqueamento = 25f;
    public float distanciaMinima = 10f;

    [Header("Flanqueamento")]
    public float lateralOffset = 15f;
    public float suavidadeRotacao = 2f;

    [Header("Evitar outros inimigos")]
    public float raioSeparacao = 10f;
    public float forcaSeparacao = 5f;
    public LayerMask camadaInimigos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void Update()
    {
        if (agent.isOnNavMesh && player != null)
        {
            Vector3 toPlayer = player.position - transform.position;
            toPlayer.y = 0f;
            float distancia = toPlayer.magnitude;

            Vector3 destinoBase;

            if (distancia > distanciaFlanqueamento)
            {
                destinoBase = player.position;
            }
            else if (distancia > distanciaMinima)
            {
                Vector3 lado = Vector3.Cross(Vector3.up, toPlayer.normalized) * lateralOffset;
                destinoBase = player.position + lado;
            }
            else
            {
                destinoBase = transform.position;
            }

            // === Cálculo do vetor de separação ===
            Collider[] proximos = Physics.OverlapSphere(transform.position, raioSeparacao, camadaInimigos);
            Vector3 separacao = Vector3.zero;
            int count = 0;

            foreach (var c in proximos)
            {
                if (c.gameObject != this.gameObject)
                {
                    Vector3 direcao = transform.position - c.transform.position;
                    float distanciaSeparacao = direcao.magnitude;
                    if (distanciaSeparacao > 0f)
                    {
                        separacao += direcao.normalized / distanciaSeparacao;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                separacao /= count;
                separacao *= forcaSeparacao;
                separacao.y = 0f;
            }

            Vector3 destinoFinal = destinoBase + separacao;
            agent.SetDestination(destinoFinal);

            // Rotação suave para frente
            Vector3 dirMovimento = agent.desiredVelocity;
            if (dirMovimento.sqrMagnitude > 0.01f)
            {
                Quaternion rot = Quaternion.LookRotation(dirMovimento.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * suavidadeRotacao);
            }
        }
    }
}
