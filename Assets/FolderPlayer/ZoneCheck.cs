using UnityEngine;
using System.Collections;

public class ZoneCheck : MonoBehaviour
{
    [Header("Spawner de Inimigos")]
    public EnemySpawner spawner;

    [Header("Configuração do Boss")]
    public GameObject krakenPrefab;
    public Transform centroArena;
    private GameObject krakenInstanciado;

    [Header("Player")]
    public PlayerMove playerMove;
    [SerializeField] private CameraFollow cameraFollow;

    private bool nivelAtingido = false;
    private bool bossAtivo = false;

    void Start()
    {
        StartCoroutine(EsperarNivelDoJogador());
    }

    IEnumerator EsperarNivelDoJogador()
    {
        while (!nivelAtingido)
        {
            if (PlayerXpManage.instance.nivel >= 4)
            {
                nivelAtingido = true;
                Debug.Log("Atingiu o nível 2! Inimigos comuns sumiram. Vá para o centro da arena.");

                if (spawner != null)
                {
                    spawner.spawnAtivo = false;
                    spawner.DestruirInimigosAtivos();
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!nivelAtingido || bossAtivo) return;

        bossAtivo = true;

        if (krakenInstanciado == null && krakenPrefab != null && centroArena != null)
        {
            krakenInstanciado = Instantiate(krakenPrefab, centroArena.position, Quaternion.identity);
            Debug.Log("Kraken instanciado!");

            AudioManager.Instance.TocarSomEfeito(3);
            AudioManager.Instance.TocarSomEfeito(4);
            AudioManager.Instance.TocarSomEfeito(5);

            if (cameraFollow != null)
                cameraFollow.AtivarCameraBoss();
        }

        if (playerMove != null && centroArena != null)
        {
            playerMove.AtivarModoOrbital(centroArena);
        }
    }
}
