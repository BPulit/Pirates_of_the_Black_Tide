using UnityEngine;

public class MissilTeleguiado : MonoBehaviour
{
    public float velocidade = 40f;
    public float rotacao = 20f;
    public float tempoDeVida = 30f;
    public int dano = 10;

    [Header("VFX")]
    public GameObject efeitoExplosao;
    public GameObject efeitoFlame;

    private Transform alvo;
    private GameObject flameInstanciado;
    private bool explodiu = false;

    void Start()
    {
        // Instancia o efeito de chama e o prende na traseira do míssil
        if (efeitoFlame != null)
        {
            flameInstanciado = Instantiate(efeitoFlame, transform);
            flameInstanciado.transform.localPosition = new Vector3(0, 0, -1f); // ajuste para a traseira
            flameInstanciado.transform.localRotation = Quaternion.identity;
        }

        // Explodir após tempo limite
        Invoke(nameof(ExplodirPorTempo), tempoDeVida);
    }

    public void DefinirAlvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    void Update()
    {
        if (explodiu) return;

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
        if (explodiu) return;

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

            var seaMonster = other.GetComponent<SeaMonsterStatus>();
            if (seaMonster != null)
            {
                seaMonster.TomarDano(dano);
                aplicouDano = true;
            }

            if (aplicouDano)
            {
                Explodir(transform.position);
            }
        }
    }

    private void ExplodirPorTempo()
    {
        if (!explodiu)
        {
            Explodir(transform.position);
        }
    }

    private void Explodir(Vector3 posicao)
    {
        explodiu = true;

        if (efeitoExplosao != null)
        {
            GameObject vfx = Instantiate(efeitoExplosao, posicao, Quaternion.identity);
            Destroy(vfx, 2f);
        }

        if (flameInstanciado != null)
        {
            Destroy(flameInstanciado);
        }

        Destroy(gameObject);
    }
}
