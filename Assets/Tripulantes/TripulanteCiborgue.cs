using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Tripulantes/Passivo/Ciborgue")]
public class TripulanteCiborgue : Tripulante
{
    public GameObject prefabBolha;
    public float tempoProtecao = 5f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        AudioManager.Instance.TocarSomEfeito(8);
        BolhaUtil.AtivarBolha(jogador, prefabBolha, tempoProtecao);
    }

}

