using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    public Slider sliderVidaInstantanea; // barra vermelha (vida atual)
    public Slider sliderVidaDelay;       // barra preta transparente (atraso visual)

    [Header("Velocidade do Delay")]
    public float velocidadeDelay = 1f; // velocidade que a barra preta chega até a vermelha

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Busca o texto se não tiver arrastado
        if (vidasTexto == null)
        {
            GameObject textoVida = GameObject.Find("NumeroVidas");
            if (textoVida != null)
                vidasTexto = textoVida.GetComponent<TextMeshProUGUI>();
        }

        AtualizarUI(true); // força atualizar de início
    }

    private void Update()
    {
        // Atualiza gradualmente a barra delay para "alcançar" a barra principal
        if (sliderVidaDelay != null && sliderVidaInstantanea != null)
        {
            if (sliderVidaDelay.value > sliderVidaInstantanea.value)
            {
                sliderVidaDelay.value = Mathf.MoveTowards(
                    sliderVidaDelay.value,
                    sliderVidaInstantanea.value,
                    velocidadeDelay * Time.deltaTime * vidaMaxima
                );
            }
        }
    }

    public void TomarDano(int dano)
    {
        if (invulneravel)
        {
            Debug.Log("Dano ignorado: jogador está invulnerável");
            return;
        }

        vidaAtual -= dano;
        Object.FindFirstObjectByType<OverlayEffect>()?.MostrarDano();


        vidaAtual = Mathf.Max(vidaAtual, 0);
        AtualizarUI(false);

        if (vidaAtual <= 0)
            Morreu();
    }

    public void Curar(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
        AtualizarUI(true); // em cura, atualiza as duas barras juntas
    }

    public void AtualizarUI(bool forcarAmbas)
    {
        if (vidasTexto != null)
            vidasTexto.text = "x" + vidaAtual.ToString();

        if (sliderVidaInstantanea != null)
        {
            sliderVidaInstantanea.maxValue = vidaMaxima;
            sliderVidaInstantanea.value = vidaAtual;
        }

        if (sliderVidaDelay != null)
        {
            sliderVidaDelay.maxValue = vidaMaxima;
            if (forcarAmbas)
            {
                sliderVidaDelay.value = vidaAtual; // cura instantânea
            }
            // caso contrário (em dano), deixa o delay rolar no Update
        }
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
