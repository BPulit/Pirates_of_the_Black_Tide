using UnityEngine;
using System.Collections;

public class SeaMonsterAttack : MonoBehaviour
{
    public SeaMonsterAnimatorControl animControl;
    public Transform player;
    public float intervaloEntreAtaques = 4f;
    public float anguloFrontal = 60f;

    void Start()
{
    // Atribui o player automaticamente se estiver nulo
    if (player == null)
        player = GameObject.FindWithTag("Player")?.transform;

    // Atribui o animControl se estiver nulo
    if (animControl == null)
        animControl = GetComponent<SeaMonsterAnimatorControl>();

    StartCoroutine(CicloDeAtaque());
}

    IEnumerator CicloDeAtaque()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloEntreAtaques);
            RealizarAtaque();
        }
    }

   void RealizarAtaque()
{
    if (player == null || animControl == null) return;

    Vector3 direcaoPlayer = (player.position - transform.position).normalized;
    float dot = Vector3.Dot(transform.forward, direcaoPlayer);

    // Player está na frente (ângulo ~60°) → dot > 0.5
    if (dot > 0.5f)
        animControl.AtacarCabeca();
    else
        animControl.AtacarRabo();
}

}
