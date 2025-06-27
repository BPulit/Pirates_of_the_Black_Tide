using UnityEngine;

public class MovimentoGaivota : MonoBehaviour
{
    public float raioVoo = 10f;
    public float velocidadeAngular = 30f;
    public float velocidadeVooReto = 5f;
    public float tempoVooReto = 2f;
    public float distanciaMaxima = 30f;

    private Vector3 centro;
    private float anguloAtual;
    private float tempoRetoRestante = 0f;
    private bool voandoReto = false;
    private Vector3 direcaoVooReto;
    private float alturaFixaY;

    void Start()
    {
        centro = transform.position;
        anguloAtual = Random.Range(0f, 360f);
        alturaFixaY = transform.position.y;
    }

    void Update()
    {
        // Corrige altura Y fixa
        Vector3 pos = transform.position;
        pos.y = alturaFixaY;
        transform.position = pos;

        float distancia = Vector3.Distance(transform.position, centro);

        // Se longe demais, força a voltar ao centro
        if (distancia > distanciaMaxima)
        {
            voandoReto = false;
        }

        if (voandoReto)
        {
            transform.position += direcaoVooReto * velocidadeVooReto * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcaoVooReto), Time.deltaTime * 2f);

            tempoRetoRestante -= Time.deltaTime;
            if (tempoRetoRestante <= 0f)
            {
                voandoReto = false;
            }
        }
        else
        {
            // Movimento circular ao redor do centro
            anguloAtual += velocidadeAngular * Time.deltaTime;
            float rad = anguloAtual * Mathf.Deg2Rad;
            Vector3 novaPos = centro + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * raioVoo;
            Vector3 direcao = (novaPos - transform.position).normalized;
            direcao.y = 0;

            transform.position += direcao * velocidadeVooReto * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcao), Time.deltaTime * 2f);

            // Chance aleatória de começar voo reto
            if (Random.Range(0f, 1f) < 0.01f)
            {
                voandoReto = true;
                tempoRetoRestante = tempoVooReto;
                direcaoVooReto = transform.forward;
            }
        }
    }
    public void DefinirCentro(Vector3 novoCentro)
    {
        centro = novoCentro;
    }
}
