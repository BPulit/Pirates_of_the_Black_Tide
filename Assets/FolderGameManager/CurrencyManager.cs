using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    private int moedas = 0;
    public TextMeshProUGUI textoMoedas; // Referência ao texto da UI

    private int naviosDestruidos = 0;
    private int bossDerrotados = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AdicionarMoedas(int quantidade)
    {
        moedas += quantidade;
        AtualizarHUD();
    }

    public void GastarMoedas(int quantidade)
    {
        moedas -= quantidade;
        moedas = Mathf.Max(0, moedas); // Impede ficar negativo
        AtualizarHUD();
    }

    public int GetMoedas()
    {
        return moedas;
    }

    void AtualizarHUD()
    {
        if (textoMoedas != null)
        {
            textoMoedas.text = "Moedas: " + moedas.ToString();
        }
    }

    public void IncrementarNaviosDestruidos()
    {
        naviosDestruidos++;
    }

    public int GetNaviosDestruidos()
    {
        return naviosDestruidos;
    }
     public void IncrementarBossDerrotados()
    {
        bossDerrotados++;
    }

    public int GetBossDerrotados()
    {
        return bossDerrotados;
    }
}
