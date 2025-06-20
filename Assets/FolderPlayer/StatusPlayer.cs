using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatusPlayer : MonoBehaviour
{
    public static StatusPlayer Instance;

    [Header("Atributos")]
    public int vidaMaxima = 100;
    public int vidaAtual = 100;
    public float velocidade = 5f;
    public int ataque = 10;
    public bool invulneravel = false;

    [Header("Referências UI")]
    public TextMeshProUGUI vidasTexto;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (vidasTexto == null)
        {
            GameObject textoVida = GameObject.Find("NumeroVidas");
            if (textoVida != null)
                vidasTexto = textoVida.GetComponent<TextMeshProUGUI>();
        }

        AtualizarUI();
    }

    public void TomarDano(int dano)
    {
        if (invulneravel)
        {
            Debug.Log("Dano ignorado: jogador está invulnerável");
            return;
        }
            vidaAtual -= dano;
            vidaAtual = Mathf.Max(vidaAtual, 0);

            AtualizarUI();

            if (vidaAtual <= 0)
                Morreu();
    }

    public void Curar(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
        AtualizarUI();
    }

    public void AtualizarUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = "x" + vidaAtual.ToString();
    }

    private void Morreu()
    {
        
         PlayerPrefs.SetInt("nivel", PlayerXpManage.instance.nivel); 
        PlayerPrefs.SetInt("naviosDestruidos", CurrencyManager.instance.GetNaviosDestruidos());
        PlayerPrefs.SetInt("bossDerrotados", CurrencyManager.instance.GetBossDerrotados());
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Derrota");
    }
}
