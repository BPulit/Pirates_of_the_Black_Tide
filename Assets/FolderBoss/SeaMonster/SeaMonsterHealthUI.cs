using UnityEngine;
using UnityEngine.UI;

public class SeaMonsterHealthUI : MonoBehaviour
{
    public static SeaMonsterHealthUI instance;
    public Slider barraDeVida;

    void Awake()
    {
        instance = this;
        if (barraDeVida != null)
        {
            barraDeVida.gameObject.SetActive(false);
        }
    }

    public void Inicializar(int vidaMax)
    {
        barraDeVida.maxValue = vidaMax;
        barraDeVida.value = vidaMax;
        barraDeVida.gameObject.SetActive(true);
    }

    public void AtualizarVida(int vidaAtual)
    {
        barraDeVida.value = vidaAtual;
    }

    public void Esconder()
    {
        barraDeVida.gameObject.SetActive(false);
    }
}
