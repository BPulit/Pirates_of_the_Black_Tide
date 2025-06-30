using UnityEngine;

public class ZoneCheckKraken : MonoBehaviour
{
    [Header("Kraken")]
    public GameObject krakenPrefab;
    public Transform spawnPoint;
    

    [Header("Referências")]
    public PlayerMove playerMove;
    public CameraFollow cameraFollow;
    public GameObject avisoBoss;
    public EnemySpawner spawner;
    public Transform centroArenaKraken;

    private bool bossAtivado = false;
    private int nivelMinimo = 3;

    void Start()
    {
        if (PlayerXpManage.instance.nivel >= nivelMinimo)
        {
            AtivarAvisoBoss();
        }
    }

    void Update()
    {
        // Se o jogador subir de nível enquanto estiver perto da zona
        if (!bossAtivado && PlayerXpManage.instance.nivel >= nivelMinimo && avisoBoss != null && !avisoBoss.activeSelf)
        {
            AtivarAvisoBoss();
        }
    }

    void AtivarAvisoBoss()
    {
        avisoBoss?.SetActive(true);
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || bossAtivado || !BossManager.instance.PodeAtivarBoss()) return;

        if (PlayerXpManage.instance.nivel < nivelMinimo) return;
        spawner?.DestruirInimigosAtivos();
        spawner.spawnAtivo = false;
        
        bossAtivado = true;
        BossManager.instance.AtivarBoss();

        avisoBoss?.SetActive(false);

        GameObject kraken = Instantiate(krakenPrefab, spawnPoint.position, Quaternion.identity);

        if (playerMove != null && playerMove.centrosDaArena != null)
            playerMove.AtivarModoOrbital(centroArenaKraken);

        cameraFollow?.AtivarCameraBoss();

        AudioManager.Instance.TocarSomEfeito(3);
        AudioManager.Instance.TocarSomEfeito(4);
        AudioManager.Instance.TocarSomEfeito(5);
    }
}
