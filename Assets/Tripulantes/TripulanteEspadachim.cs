using UnityEngine;

[CreateAssetMenu(fileName = "TripulanteEspadachim", menuName = "Tripulantes/Espadachim")]
public class TripulanteEspadachim : Tripulante
{
    public GameObject cortePrefab;
    public float velocidadeCorte = 15f;
    public float tempoDestruicao = 3f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);
        AudioManager.Instance.TocarSomEfeito(13);

        Vector3 spawnPos = jogador.transform.position + jogador.transform.forward * 3f + Vector3.up * 1f;
        GameObject corte = Instantiate(cortePrefab, spawnPos, jogador.transform.rotation);

        Rigidbody rb = corte.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = jogador.transform.forward * velocidadeCorte;
        }

        GameObject.Destroy(corte, tempoDestruicao);
    }
}
