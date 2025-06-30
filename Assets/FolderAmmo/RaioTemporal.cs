using UnityEngine;

public class RaioTemporal : MonoBehaviour
{
    private float danoPorSegundo;
    private float duracao;
    private float tempoVivo;

    public float velocidadeDescida = 10f;
    private bool tocouChao = false;
    private float velocidadeRotacao;

    [Header("VFX")]
    public GameObject tornadoVFXPrefab;
    private GameObject tornadoVFXInstanciado;

    void Update()
    {
        if (!tocouChao)
        {
            transform.position += Vector3.down * velocidadeDescida * Time.deltaTime;

            if (transform.position.y <= 20f) // ponto de colisão com o solo
            {
                tocouChao = true;
                tempoVivo = 0f;

               if (tornadoVFXPrefab != null)
                {
                    // Instancia o VFX como objeto independente
                    Quaternion rotacaoVFX = Quaternion.Euler(-90f, 0f, 0f);
                    tornadoVFXInstanciado = Instantiate(tornadoVFXPrefab, transform.position, rotacaoVFX);

                    // Aumenta o tamanho do VFX (ajuste conforme necessário)
                    tornadoVFXInstanciado.transform.localScale = Vector3.one * 3f;
                }

            }
        }
        else
        {
            tempoVivo += Time.deltaTime;
            transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);

            if (tempoVivo >= duracao)
            {
                if (tornadoVFXInstanciado != null)
                {
                    Destroy(tornadoVFXInstanciado);
                }

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
                enemy.TakeDamage(Mathf.CeilToInt(danoPorSegundo * Time.deltaTime));

            var kraken = other.GetComponent<KrakenStatus>();
            if (kraken != null)
                kraken.TakeDamage(Mathf.CeilToInt(danoPorSegundo * Time.deltaTime));

            var seaMonster = other.GetComponent<SeaMonsterStatus>();
            if (seaMonster != null)
                seaMonster.TomarDano(Mathf.CeilToInt(danoPorSegundo * Time.deltaTime));
        }
    }

    public void Configurar(float dano, float duracaoRaio)
    {
        this.danoPorSegundo = dano;
        this.duracao = duracaoRaio;
        this.velocidadeRotacao = Random.Range(500f, 600f);
    }
}
