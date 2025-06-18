using UnityEngine;

[CreateAssetMenu(fileName = "TripulanteTritao", menuName = "Tripulantes/Tritao")]
public class TripulanteTritao : Tripulante
{
    public GameObject ondaPrefab;
    public float velocidadeOnda = 10f;
    public float duracaoOnda = 3f;
    public int dano = 4;

public override void AtivarHabilidade(GameObject jogador)
{
    base.AtivarHabilidade(jogador);

    Vector3 pos = jogador.transform.position;

    // Direções laterais em relação ao navio, mas ignorando a inclinação vertical
    Vector3 direita = jogador.transform.right;
    Vector3 esquerda = -jogador.transform.right;

    direita.y = 0;
    esquerda.y = 0;

    direita.Normalize();
    esquerda.Normalize();

    Quaternion rotacaoDireita = Quaternion.LookRotation(direita);
    Quaternion rotacaoEsquerda = Quaternion.LookRotation(esquerda);

    // Lado direito
    GameObject ondaDireita = GameObject.Instantiate(ondaPrefab, pos + direita * 2f, rotacaoDireita);
    OndaTritao compDir = ondaDireita.GetComponent<OndaTritao>();
    compDir.Configurar(direita, velocidadeOnda, duracaoOnda, dano);

    // Lado esquerdo
    GameObject ondaEsquerda = GameObject.Instantiate(ondaPrefab, pos + esquerda * 2f, rotacaoEsquerda);
    OndaTritao compEsq = ondaEsquerda.GetComponent<OndaTritao>();
    compEsq.Configurar(esquerda, velocidadeOnda, duracaoOnda, dano);
}
}
