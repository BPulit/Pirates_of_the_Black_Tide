using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Tripulantes/Passivo/Ciborgue")]
public class TripulanteCiborgue : Tripulante
{
    public GameObject prefabBolha;
    public float tempoProtecao = 5f;

    public override void AtivarHabilidade(GameObject jogador)
    {
        Transform pontoSpawn = jogador.transform;
        GameObject bolha = GameObject.Instantiate(prefabBolha, pontoSpawn.position, Quaternion.identity);
        bolha.transform.SetParent(jogador.transform);
        bolha.layer = LayerMask.NameToLayer("Protecao");

        BolhaProtetora script = bolha.GetComponent<BolhaProtetora>();
        script.duracao = tempoProtecao;
    }
}

