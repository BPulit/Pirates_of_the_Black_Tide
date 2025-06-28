using UnityEngine;

public class ZoneCheckSeaMonster : MonoBehaviour
{
    [Header("Sea Monster")]
    public GameObject seaMonsterPrefab;
    public Transform spawnPoint;

    [Header("Referências")]
    public PlayerMove playerMove;
    public CameraFollow cameraFollow;
    public GameObject avisoBoss;
    public EnemySpawner spawner;

    private bool bossAtivado = false;
    private int nivelMinimo = 6;

    void Start()
    {
        if (PlayerXpManage.instance.nivel >= nivelMinimo)
        {
            AtivarAvisoBoss();
        }
    }

    void Update()
    {
        // Se o jogador subir de nível depois de entrar na cena
        if (!bossAtivado && PlayerXpManage.instance.nivel >= nivelMinimo && avisoBoss != null && !avisoBoss.activeSelf)
        {
            AtivarAvisoBoss();
        }
    }

    void AtivarAvisoBoss()
    {
        avisoBoss?.SetActive(true);
        spawner?.DestruirInimigosAtivos();
        if (spawner != null)
        {
            spawner.spawnAtivo = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || bossAtivado || !BossManager.instance.PodeAtivarBoss()) return;

        if (PlayerXpManage.instance.nivel < nivelMinimo) return;

        bossAtivado = true;
        BossManager.instance.AtivarBoss();

        avisoBoss?.SetActive(false);

        GameObject seaMonster = Instantiate(seaMonsterPrefab, spawnPoint.position, Quaternion.identity);

        if (playerMove != null && playerMove.centrosDaArena != null)
            playerMove.AtivarModoOrbital(spawnPoint);

        cameraFollow?.AtivarCameraBoss();

        AudioManager.Instance.TocarSomEfeito(3);
        AudioManager.Instance.TocarSomEfeito(4);
        AudioManager.Instance.TocarSomEfeito(5);
    }
}
