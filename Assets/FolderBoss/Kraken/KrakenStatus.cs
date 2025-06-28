using UnityEngine;
using UnityEngine.SceneManagement;

public class KrakenStatus : MonoBehaviour
{
    public int vidaMaxima = 10;
    private int vidaAtual;

    [Header("Referências externas")]
    public PlayerMove playerMove;
    public CameraFollow cameraFollow;
    public GameObject barreiraArena; 
    public GameObject zonaBoss;      

    [Header("Spawner de Inimigos")]
    public EnemySpawner spawner;
    

   void Start()
{
    vidaAtual = vidaMaxima;

    if (KrakenHealthUI.instance != null)
        KrakenHealthUI.instance.Inicializar(vidaMaxima);

    // Correções de referência com métodos atualizados
    if (playerMove == null)
        playerMove = Object.FindFirstObjectByType<PlayerMove>();

    if (cameraFollow == null)
        cameraFollow = Object.FindFirstObjectByType<CameraFollow>();

    if (barreiraArena == null)
        barreiraArena = GameObject.FindWithTag("BarreiraKraken");

    if (zonaBoss == null)
        zonaBoss = GameObject.FindWithTag("ZonaKraken");

    if (spawner == null)
    {
        GameObject spawnerObj = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (spawnerObj != null)
            spawner = spawnerObj.GetComponent<EnemySpawner>();
    }
}





    public void TakeDamage(int quantidade)
    {
        vidaAtual -= quantidade;
        Debug.Log("Kraken tomou dano! Vida atual: " + vidaAtual);
        
    if (KrakenHealthUI.instance != null)
        {
            KrakenHealthUI.instance.AtualizarVida(vidaAtual);
        }

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

   void Morrer()
{
    Debug.Log("Morrendo...");

    MensagemUI.instance?.MostrarMensagem("Parabens voce matou o kraken!");

    if (playerMove != null)
    {
        playerMove.DesativarModoOrbital();

        Transform centro = playerMove.GetCentroAtual();
        if (centro != null)
        {
            Destroy(centro.gameObject);
            Debug.Log("Centro da arena destruído.");
        }
        else
        {
            Debug.LogWarning("Centro da arena estava nulo.");
        }

        Rigidbody rb = playerMove.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        else
        {
            Debug.LogWarning("Rigidbody do playerMove está nulo.");
        }
    }
    else
    {
        Debug.LogWarning("playerMove está nulo.");
    }

    if (cameraFollow != null)
    {
        cameraFollow.VoltarCameraNormal();
    }
    else
    {
        Debug.LogWarning("cameraFollow está nulo.");
    }

    if (barreiraArena != null)
    {
        Destroy(barreiraArena);
    }
    else
    {
        Debug.LogWarning("barreiraArena está nulo.");
    }

    if (zonaBoss != null)
    {
        Destroy(zonaBoss);
    }
    else
    {
        Debug.LogWarning("zonaBoss está nulo.");
    }

    if (KrakenHealthUI.instance != null)
    {
        KrakenHealthUI.instance.Esconder();
    }
    else
    {
        Debug.LogWarning("KrakenHealthUI.instance está nulo.");
    }

    if (BossManager.instance != null)
    {
        BossManager.instance.FinalizarBoss();
    }
    else
    {
        Debug.LogWarning("BossManager.instance está nulo.");
    }

    if (CurrencyManager.instance != null)
    {
        CurrencyManager.instance.IncrementarBossDerrotados();
    }
    else
    {
        Debug.LogWarning("CurrencyManager.instance está nulo.");
    }

    if (spawner != null)
    {
        spawner.spawnAtivo = true;
        spawner.estaNoBoss = false;
    }
    else
    {
        Debug.LogWarning("spawner está nulo.");
    }

    Destroy(gameObject);
}

}

