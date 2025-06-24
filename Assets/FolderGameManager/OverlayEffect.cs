using UnityEngine;
using UnityEngine.UI;

public class OverlayEffect : MonoBehaviour
{
    public Image overlayDamageImage;
    public float fadeSpeed = 3;
    public float maxAlpha = 0.1f;

    private Color currentColor;

    void Start()
    {
        if (overlayDamageImage != null)
            currentColor = overlayDamageImage.color;
    }

    void Update()
    {
        if (overlayDamageImage == null) return;

        if (currentColor.a > 0f)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, 0f, fadeSpeed * Time.deltaTime);
            overlayDamageImage.color = currentColor;
        }
    }

    public void MostrarDano()
    {
        currentColor = overlayDamageImage.color;
        currentColor.a = maxAlpha;
        overlayDamageImage.color = currentColor;
    }
}