using UnityEngine;

[CreateAssetMenu(menuName = "Tripulantes/Passivo/Maga de Gelo")]
public class TripulanteMagaDeGelo : Tripulante
{
    public float raio = 10f;
    public float duracaoCongelamento = 3f;

   public override void AtivarHabilidade(GameObject jogador)
{
    Collider[] alvos = Physics.OverlapSphere(jogador.transform.position, raio);
    AudioManager.Instance.TocarSomEfeito(10);

    foreach (Collider alvo in alvos)
    {
        CongelarInimigo congelador = alvo.GetComponentInParent<CongelarInimigo>();
        if (congelador == null)
            congelador = alvo.GetComponentInChildren<CongelarInimigo>();

        if (congelador != null)
        {
            congelador.Congelar(duracaoCongelamento);
            Debug.Log($"[Maga de Gelo] Congelou {alvo.name} por {duracaoCongelamento}s");
        }
        else
        {
            Debug.LogWarning($"[Maga de Gelo] Nenhum script CongelarEntidade encontrado em {alvo.name}");
        }
    }
}

}
