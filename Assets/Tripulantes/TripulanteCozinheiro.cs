using UnityEngine;

[CreateAssetMenu(menuName = "Tripulantes/Passivo/Cozinheiro")]
public class TripulanteCozinheiro : Tripulante
{
    public float duracao = 30f;
    public float fatorReducao = 0.5f; // cooldown pela metade

    public override void AtivarHabilidade(GameObject jogador)
    {
        Debug.Log("Cozinheiro ativado! Cooldowns reduzidos temporariamente.");

        TripulanteManager.instance.ReduzirCooldownTemporariamente(fatorReducao, duracao, this);
        AudioManager.Instance.TocarSomEfeito(9);
    }
}
