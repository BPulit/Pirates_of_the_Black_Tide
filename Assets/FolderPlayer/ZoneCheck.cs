using UnityEngine;

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

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Desativa spawn dos inimigos
        if (spawner != null)
        {
            spawner.estaNoBoss = true;
            spawner.spawnAtivo = false;
            spawner.DestruirInimigosAtivos();
        }

        // Instancia o Kraken
        if (krakenInstanciado == null && krakenPrefab != null && centroArena != null)
        {
            krakenInstanciado = Instantiate(krakenPrefab, centroArena.position, Quaternion.identity);
            cameraFollow.AtivarCameraBoss();

            AudioManager.Instance.TocarSomEfeito(3);
            AudioManager.Instance.TocarSomEfeito(4);
            AudioManager.Instance.TocarSomEfeito(5);
        }

        // Chama o método de ativar modo orbital no Player
        if (playerMove != null && centroArena != null)
        {
            playerMove.AtivarModoOrbital(centroArena);
            

        }
    }


}