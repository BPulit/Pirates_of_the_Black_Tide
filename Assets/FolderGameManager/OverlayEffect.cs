using UnityEngine;
using UnityEngine.UI;

public class OverlayEffect : MonoBehaviour
{
    public Image overlayImage; // Use um único Image
    public float fadeSpeed = 3f;
    public float maxAlpha = 0.2f;

    private Color targetColor;
    private bool mostrando = false;
    public static OverlayEffect Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        if (overlayImage != null)
        {
            targetColor = overlayImage.color;
            overlayImage.color = new Color(0, 0, 0, 0); // transparente no início
        }
    }

    void Update()
    {
        if (overlayImage == null || !mostrando) return;

        Color atual = overlayImage.color;
        atual.a = Mathf.MoveTowards(atual.a, 0f, fadeSpeed * Time.unscaledDeltaTime);
        overlayImage.color = atual;

        if (overlayImage.color.a <= 0f)
            mostrando = false;
    }

    public void MostrarDano()
    {
        MostrarCor(new Color(1, 0, 0, maxAlpha)); // Vermelho
    }

    public void MostrarCura()
    {
        MostrarCor(new Color(0, 1, 0, maxAlpha)); // Verde
    }

    public void MostrarLevelUp()
    {
        MostrarCor(new Color(0.2f, 0.6f, 1f, maxAlpha)); // Azul claro
    }

    private void MostrarCor(Color cor)
    {
        if (overlayImage == null) return;

        overlayImage.color = cor;
        mostrando = true;
    }
}
