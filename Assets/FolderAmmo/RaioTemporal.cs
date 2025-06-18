using UnityEngine;

public class RaioTemporal : MonoBehaviour
{
    private float danoPorSegundo;
    private float duracao;
    private float tempoVivo;

    public float velocidadeDescida = 10f;
    private bool tocouChao = false;

    void Update()
    {
        if (!tocouChao)
        {
            transform.position += Vector3.down * velocidadeDescida * Time.deltaTime;

            if (transform.position.y <= 20f) // ponto de colisão com o solo
            {
                tocouChao = true;
                tempoVivo = 0f;
            }
        }
        else
        {
            tempoVivo += Time.deltaTime;

            if (tempoVivo >= duracao)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!tocouChao) return;

        if (other.CompareTag("Inimigo") || other.CompareTag("Boss"))
        {
            var enemy = other.GetComponent<StatusEnemie>();
            if (enemy != null)
            {
                enemy.TakeDamage(Mathf.CeilToInt(danoPorSegundo * Time.deltaTime));
            }

            var boss = other.GetComponent<KrakenStatus>();
            if (boss != null)
            {
                boss.TakeDamage(Mathf.CeilToInt(danoPorSegundo * Time.deltaTime));
            }
        }
    }

    public void Configurar(float dano, float duracaoRaio)
    {
        this.danoPorSegundo = dano;
        this.duracao = duracaoRaio;
    }
}
