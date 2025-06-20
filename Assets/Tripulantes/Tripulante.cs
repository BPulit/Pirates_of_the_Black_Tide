using UnityEngine;

public enum TeclaAtivacao { Q, E, R, F, C }

[CreateAssetMenu(fileName = "NovoTripulante", menuName = "Tripulantes/Tripulante")]
public class Tripulante : ScriptableObject
{
    public string nome;
    public string idUnico;
    public Sprite icone;
    public float cooldown;
    public string descricao;

    public virtual void AtivarHabilidade(GameObject jogador)
    {
        Debug.Log($"Habilidade de {nome} ativada!");
    }
}
