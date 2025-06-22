using UnityEngine;

public class MissilTeleguiado : MonoBehaviour
{
    public float velocidade = 40f;
    public float rotacao = 20f;
    public float tempoDeVida = 15f;
    public int dano = 10;
    public GameObject efeitoExplosao;

    private Transform alvo;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    void Update()
    {
        if (alvo == null)
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
            return;
        }

        Vector3 direcao = (alvo.position - transform.position).normalized;
        Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, rotacao * Time.deltaTime);

        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Inimigo") || other.CompareTag("Boss"))
    {
        bool aplicouDano = false;

        var inimigo = other.GetComponent<StatusEnemie>();
        if (inimigo != null)
        {
            inimigo.TakeDamage(dano);
            aplicouDano = true;
        }

        var kraken = other.GetComponent<KrakenStatus>();
        if (kraken != null)
        {
            kraken.TakeDamage(dano);
            aplicouDano = true;
        }

        if (aplicouDano && efeitoExplosao != null)
        {
            Instantiate(efeitoExplosao, transform.position, Quaternion.identity);
        }

        if (aplicouDano)
        {
            Destroy(gameObject);
        }
    }
}

}
