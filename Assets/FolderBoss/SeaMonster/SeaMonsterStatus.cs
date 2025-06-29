using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SeaMonsterStatus : MonoBehaviour
{
    public int vidaMaxima = 12;
    private int vidaAtual;
    public int valueXp;
    private bool estaMorrendo = false;

    [Header("Referências")]
    public PlayerMove playerMove;
    public GameObject barreiraArena;
    public GameObject zonaBoss;
    public Animator animator;
    public EnemySpawner spawner;
    public CameraFollow cameraFollow;

    void Start()
    {
        vidaAtual = vidaMaxima;

        if (SeaMonsterHealthUI.instance != null)
            SeaMonsterHealthUI.instance.Inicializar(vidaMaxima);
            
         if (playerMove == null)
            playerMove = Object.FindFirstObjectByType<PlayerMove>();

        if (cameraFollow == null)
            cameraFollow = Object.FindFirstObjectByType<CameraFollow>();

        if (barreiraArena == null)
            barreiraArena = GameObject.FindWithTag("BarreiraSeaMonster");

        if (zonaBoss == null)
        zonaBoss = GameObject.FindWithTag("ZonaSeaMonster");

        GameObject centro = GameObject.FindGameObjectWithTag("ArenaCenterSeaMonster");
        if (spawner == null)
        {
            GameObject spawnerObj = GameObject.FindGameObjectWithTag("EnemySpawner");
            if (spawnerObj != null)
                spawner = spawnerObj.GetComponent<EnemySpawner>();
        }
    }

    public void TomarDano(int dano)
    {
        if (estaMorrendo) return;

        vidaAtual -= dano;
        Debug.Log("SeaMonster tomou dano: " + vidaAtual);
         if (SeaMonsterHealthUI.instance != null)
        {
            SeaMonsterHealthUI.instance.AtualizarVida(vidaAtual);
        }
        if (vidaAtual <= 0)
            StartCoroutine(Morrer());
    }

    IEnumerator Morrer()
{
    estaMorrendo = true;
    PlayerXpManage.instance.GanharXP(valueXp);

    if (playerMove != null)
        {
            playerMove.DesativarModoOrbital();
            playerMove.DestruirCentroAtual();

            Rigidbody rb = playerMove.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }
    

    if (cameraFollow != null)
            cameraFollow.VoltarCameraNormal();

    // Toca animação com tempo de espera mais confiável
    yield return StartCoroutine(MorrerEBloquear());
        PlayerPrefs.SetInt("nivel", PlayerXpManage.instance.nivel);
        PlayerPrefs.SetInt("naviosDestruidos", CurrencyManager.instance.GetNaviosDestruidos());
        PlayerPrefs.SetInt("bossDerrotados", CurrencyManager.instance.GetBossDerrotados());
        PlayerPrefs.SetString("ultimaCenaJogada", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    Destroy(barreiraArena);
    Destroy(zonaBoss);


    BossManager.instance.FinalizarBoss();
    CurrencyManager.instance.IncrementarBossDerrotados();

    SceneManager.LoadScene("Vitoria");
}

IEnumerator MorrerEBloquear()
{
    if (animator != null)
    {
        animator.Play("SeaMonsterDie"); // ou "Death" se esse for o nome certo
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }
    else
    {
        yield return new WaitForSeconds(2f);
    }
}

}
