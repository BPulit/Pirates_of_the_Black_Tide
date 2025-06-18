using UnityEngine;

public class OndaTritao : MonoBehaviour
{
    private Vector3 direcao;
    private float velocidade;
    private float duracao;
    private int dano;
    private float tempoVivo;

    public void Configurar(Vector3 direcao, float velocidade, float duracao, int dano)
    {
        this.direcao = direcao.normalized;
        this.velocidade = velocidade;
        this.duracao = duracao;
        this.dano = dano;
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
        tempoVivo += Time.deltaTime;

        if (tempoVivo >= duracao)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            StatusEnemie inimigo = other.GetComponent<StatusEnemie>();
            if (inimigo != null)
                inimigo.TakeDamage(dano);
        }

        if (other.CompareTag("Boss"))
        {
            KrakenStatus kraken = other.GetComponent<KrakenStatus>();
            if (kraken != null)
                kraken.TakeDamage(dano);
        }
    }
}
