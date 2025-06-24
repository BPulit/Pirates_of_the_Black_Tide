using UnityEngine;
using UnityEngine.SceneManagement;

public class KrakenStatus : MonoBehaviour
{
    public int vidaMaxima = 10;
    private int vidaAtual;

    [Header("Referências externas")]
    public PlayerMove playerMove;
    public GameObject barreiraArena; 
    public GameObject zonaBoss;      

    [Header("Spawner de Inimigos")]
    public EnemySpawner spawner;
    
   

    void Start()
    {
         vidaAtual = vidaMaxima;
         
    if (KrakenHealthUI.instance != null)
        {
            KrakenHealthUI.instance.Inicializar(vidaMaxima);
        }

    if (barreiraArena == null)
        {
            barreiraArena = GameObject.FindWithTag("BarreiraArena");
        }
    if (zonaBoss == null)
    {
        zonaBoss = GameObject.FindWithTag("BossZone");
    }
    if (spawner == null)
    {
        GameObject spawnerObj = GameObject.FindGameObjectWithTag("EnemySpawner");
        if (spawnerObj != null)
        {
            spawner = spawnerObj.GetComponent<EnemySpawner>();
        }
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
        MensagemUI.instance.MostrarMensagem("Parabens voce matou o kraken!");
         SceneManager.LoadScene("Vitoria");


        if (playerMove != null)
        {
            playerMove.modoOrbital = false;


            if (playerMove.centroDaArena != null)
            {
                Destroy(playerMove.centroDaArena.gameObject);
                playerMove.centroDaArena = null;
            }

            Rigidbody rb = playerMove.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
        GameObject centro = GameObject.FindGameObjectWithTag("ArenaCenter");
        if (centro != null)
        {
            Destroy(centro);
        }


        // 2. Sumir barreiras da arena
        if (barreiraArena != null)
        {
            Debug.Log("Destruindo: " + barreiraArena.name);

            Destroy(barreiraArena);
        }

        // 3. Sumir zona de boss
        if (zonaBoss != null)
        {
            Destroy(zonaBoss);
            Debug.Log("Destruindo: " + zonaBoss.name);
        }
        // 4. sumir Vida Boss
        if (KrakenHealthUI.instance != null)
        {
            KrakenHealthUI.instance.Esconder();
        }

        CurrencyManager.instance.IncrementarBossDerrotados();
        Destroy(gameObject);
        

        if (spawner != null)
        {
            spawner.spawnAtivo = true;
            spawner.estaNoBoss = false;
        }
        
    }
}

