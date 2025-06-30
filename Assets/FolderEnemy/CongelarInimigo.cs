using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class CongelarInimigo : MonoBehaviour
{
    private NavMeshAgent agente;
    private BossKrakenControler krakenControl;
    private Animator animator;
    private Renderer rend;
    private Color corOriginal;

    private bool estaCongelado = false;
    private EnemyShot atirador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        krakenControl = GetComponent<BossKrakenControler>();
        animator = GetComponentInChildren<Animator>();
        atirador = GetComponent<EnemyShot>();

        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            corOriginal = rend.material.color;
    }

    public void Congelar(float duracao)
    {
        if (gameObject.activeInHierarchy && !estaCongelado)
            StartCoroutine(Congelamento(duracao));
    }

    private IEnumerator Congelamento(float duracao)
    {
        estaCongelado = true;

        // Para inimigos com NavMeshAgent
        if (agente != null)
            agente.isStopped = true;

        if (atirador != null)
            atirador.congelado = true;

        // Para Kraken com script de movimento próprio
        if (krakenControl != null)
            krakenControl.Congelar(duracao);

        // Para bosses com Animator (SeaMonster)
        if (animator != null)
            animator.enabled = false;

        if (rend != null)
            rend.material.color = Color.cyan;

        yield return new WaitForSeconds(duracao);

        // Volta ao normal
        if (agente != null)
            agente.isStopped = false;

        if (atirador != null)
            atirador.congelado = false;

        if (krakenControl != null)
            krakenControl.enabled = true;

        if (animator != null)
            animator.enabled = true;

        if (rend != null)
            rend.material.color = corOriginal;

        estaCongelado = false;
    }

}
