using UnityEngine;

public class MinaAquatica : MonoBehaviour
{
    public int dano = 1;
    public GameObject vfxExplosao;
    public float tempoParaDestruirVFX = 2f;

    private Vector3 posInicial;
    private float offsetOscilacao;
    public float amplitude = 0.5f;
    public float velocidadeOscilacao = 1f;

    [HideInInspector] public MinaSpawner spawnerOrigem; // <- Spawner de origem (setado pelo spawner)

    void Start()
    {
        posInicial = transform.position;
        offsetOscilacao = Random.Range(0f, 10f);
    }

    void Update()
    {
        float novaAltura = posInicial.y + Mathf.Sin(Time.time * velocidadeOscilacao + offsetOscilacao) * amplitude;
        transform.position = new Vector3(transform.position.x, novaAltura, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatusPlayer.Instance.TomarDano(dano);
            Explodir();
        }
        else if (other.CompareTag("Projectile") || other.CompareTag("Raio"))
        {
            AudioManager.Instance.TocarSomDirecional(2, transform.position);
            Explodir();
        }
    }

    private void Explodir()
    {
        if (vfxExplosao != null)
        {
            GameObject vfx = Instantiate(vfxExplosao, transform.position, Quaternion.identity);
            Destroy(vfx, tempoParaDestruirVFX);
        }

        if (spawnerOrigem != null)
        {
            spawnerOrigem.NotificarDestruicao(gameObject);
        }

        Destroy(gameObject);
    }
}
