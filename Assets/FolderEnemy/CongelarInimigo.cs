using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CongelarInimigo : MonoBehaviour
{
    private NavMeshAgent agente;
    private Renderer rend;
    private Color corOriginal;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            corOriginal = rend.material.color;
    }

    public void Congelar(float duracao)
    {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Congelamento(duracao));
    }

    private IEnumerator Congelamento(float duracao)
    {
        if (agente != null)
            agente.isStopped = true;

        if (rend != null)
            rend.material.color = Color.blue;

        yield return new WaitForSeconds(duracao);

        if (agente != null)
            agente.isStopped = false;

        if (rend != null)
            rend.material.color = corOriginal;
    }
}
