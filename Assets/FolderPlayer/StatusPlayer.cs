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
    public GameObject prefabBolha;
     public GameObject bolhaAtual;

    [Header("Referências UI")]
    public TextMeshProUGUI vidasTexto;
    public Slider sliderVidaInstantanea; 
    public Slider sliderVidaDelay;       

    [Header("Velocidade do Delay")]
    public float velocidadeDelay = 1f; 
    [Header("VFX")]
    public GameObject vfxCuraPrefab;

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

        AtualizarUI(true); 
    }

    private void Update()
    {
        
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
            MensagemUI.instance?.MostrarMensagem("Dano ignorado: jogador está invulnerável");
            return;
        }

        vidaAtual -= dano;
        OverlayEffect.Instance.MostrarDano();


        vidaAtual = Mathf.Max(vidaAtual, 0);
        AtualizarUI(false);

        if (vidaAtual <= 0)
            Morreu();
    }

    public void Curar(int quantidade)
{
    vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
    AtualizarUI(true); 
    OverlayEffect.Instance.MostrarCura();

    // VFX de cura no barco
    if (vfxCuraPrefab != null)
    {
        GameObject vfx = Instantiate(vfxCuraPrefab, transform.position, Quaternion.identity);
        vfx.transform.SetParent(transform); // Para seguir o barco se ele estiver se movendo
        Destroy(vfx, 2f); // Destroi o VFX depois de 2 segundos
    }
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
                sliderVidaDelay.value = vidaAtual; 
            }
            
        }
    }

    private void Morreu()
    {
        PlayerPrefs.SetInt("nivel", PlayerXpManage.instance.nivel);
        PlayerPrefs.SetInt("naviosDestruidos", CurrencyManager.instance.GetNaviosDestruidos());
        PlayerPrefs.SetInt("bossDerrotados", CurrencyManager.instance.GetBossDerrotados());
        PlayerPrefs.SetString("ultimaCenaJogada", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Derrota");
    }
}
