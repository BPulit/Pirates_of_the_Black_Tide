using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TripulanteMusico", menuName = "Tripulantes/Musico")]
public class TripulanteMusico : Tripulante
{
    public GameObject notaPrefab;
    public float alturaInicial = 3f;
    public float tempoPausa = 1f;
    public float velocidadeNota = 15f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);
        jogador.GetComponent<MonoBehaviour>().StartCoroutine(LancarNotas(jogador));
    }

    IEnumerator LancarNotas(GameObject jogador)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 posSpawn = jogador.transform.position + Vector3.up * 1f;
            GameObject nota = GameObject.Instantiate(notaPrefab, posSpawn, Quaternion.identity);

            Rigidbody rb = nota.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.up * alturaInicial;

            // Espera subir
            yield return new WaitForSeconds(tempoPausa);

            // Busca inimigo aleatório
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            List<GameObject> alvos = new List<GameObject>(inimigos);

            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if (boss != null)
                alvos.Add(boss);

            if (alvos.Count > 0)
            {
                GameObject alvo = alvos[Random.Range(0, alvos.Count)];
                Vector3 direcao = (alvo.transform.position - nota.transform.position).normalized;
                rb.linearVelocity = direcao * velocidadeNota;
            }

            // Pequeno delay entre cada nota
           //yield return new WaitForSeconds(0.f);
        }
    }
}
