using UnityEngine;

[CreateAssetMenu(menuName = "Tripulantes/Passivo/Capitao")]
public class TripulanteCapitao : Tripulante
{
    public float duracao = 5f;
    public float velocidadeExtra = 1.5f;
    public float rotacaoExtra = 1.5f;
    public float cooldownProtecao = 20f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        PlayerCapitaoControlador capitao = jogador.GetComponent<PlayerCapitaoControlador>();
        if (capitao != null)
        {
            AudioManager.Instance.TocarSomEfeito(12);
            capitao.AtivarManobra(duracao, velocidadeExtra, rotacaoExtra);
        }
    }
}
