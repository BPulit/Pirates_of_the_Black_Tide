using UnityEngine;

[CreateAssetMenu(fileName = "TripulanteCurandeiro", menuName = "Tripulantes/Curandeiro")]
public class TripulanteCurandeiro : Tripulante
{
    public override void AtivarHabilidade(GameObject jogador)
    {
        base.AtivarHabilidade(jogador);

        StatusPlayer status = jogador.GetComponent<StatusPlayer>();
        if (status != null)
        {
            status.vidaMaxima += 3; // Aumenta 1 coração
            status.vidaAtual = status.vidaMaxima;
            status.Curar(0); // Atualiza a HUD
            AudioManager.Instance.TocarSomEfeito(11);
            MensagemUI.instance.MostrarMensagem("Curandeiro ativado! Vida máxima aumentada.");
        }
    }
}
