using UnityEngine;

public class MinaAquatica : MonoBehaviour
{
    public int dano = 1;
    public GameObject vfxExplosao;
    public float tempoParaDestruirVFX = 2f;
    private Vector3 posInicial;
    private float offsetOscilacao;
    public float amplitude = 0.5f;   // Altura da oscilação
    public float velocidadeOscilacao = 1f; // Velocidade da oscilação

    void Start()
    {
        posInicial = transform.position;
        offsetOscilacao = Random.Range(0f, 10f); // Garante que cada mina flutue diferente
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
            // Aplica dano no player
            StatusPlayer.Instance.TomarDano(dano);

            // Efeito visual
            if (vfxExplosao != null)
            {
                GameObject vfx = Instantiate(vfxExplosao, transform.position, Quaternion.identity);
                Destroy(vfx, tempoParaDestruirVFX);
            }

            // Som de explosão, se quiser
            AudioManager.Instance?.TocarSomEfeito(8);

            // Destrói a mina
            Destroy(gameObject);
        }
        if (other.CompareTag("Projectile"))
        {
            GameObject vfx = Instantiate(vfxExplosao, transform.position, Quaternion.identity);
            Destroy(vfx, tempoParaDestruirVFX);
            AudioManager.Instance.TocarSomDirecional(2, transform.position);
            Destroy(gameObject);
        }
        if (other.CompareTag("Raio"))
        {
            GameObject vfx = Instantiate(vfxExplosao, transform.position, Quaternion.identity);
            Destroy(vfx, tempoParaDestruirVFX);
            AudioManager.Instance.TocarSomDirecional(2, transform.position);
            Destroy(gameObject);
        }
    }
}
