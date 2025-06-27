using UnityEngine;

public class MovimentoPeixe : MonoBehaviour
{
    public float velocidade = 3f;
    public float variacaoDirecao = 20f;
    public float tempoTrocaDirecao = 2f;
    public bool peixeDeCardume = false;

    public Vector3 centroArea = Vector3.zero;
    public float raioMaximo = 30f;

    private Vector3 direcaoAtual;
    private float tempoRestante;
    private float tempoBase;
    private float fatorRotacao;
    private float alturaFixaY;

    void Start()
    {
        tempoBase = tempoTrocaDirecao + Random.Range(-0.5f, 0.5f);
        tempoRestante = tempoBase;

        fatorRotacao = Random.Range(1.5f, 3.5f);
        direcaoAtual = GerarNovaDirecao();

        alturaFixaY = transform.position.y;

        // Randomiza direção inicial
        transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    void Update()
    {
        // Mantém altura
        Vector3 pos = transform.position;
        pos.y = alturaFixaY;
        transform.position = pos;

        // Distância ao centro
        Vector3 centroXZ = new Vector3(centroArea.x, 0, centroArea.z);
        Vector3 posXZ = new Vector3(transform.position.x, 0, transform.position.z);
        float distancia = Vector3.Distance(posXZ, centroXZ);

        if (distancia > raioMaximo)
        {
            direcaoAtual = (centroArea - transform.position).normalized;
            direcaoAtual.y = 0;
        }
        else
        {
            tempoRestante -= Time.deltaTime;
            if (tempoRestante <= 0f)
            {
                direcaoAtual = GerarNovaDirecao();
                tempoRestante = tempoBase;
            }
        }

        // Rotaciona suavemente
        Quaternion rotacaoDesejada = Quaternion.LookRotation(direcaoAtual, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, Time.deltaTime * fatorRotacao);

        // Move em frente (sem alterar Y)
        Vector3 movimento = transform.forward * velocidade * Time.deltaTime;
        movimento.y = 0;
        transform.position += movimento;
    }

    Vector3 GerarNovaDirecao()
    {
        float variacao = peixeDeCardume ? variacaoDirecao * 0.3f : variacaoDirecao;
        Vector3 nova = Quaternion.Euler(0, Random.Range(-variacao, variacao), 0) * transform.forward;
        nova.y = 0;
        return nova.normalized;
    }
}
