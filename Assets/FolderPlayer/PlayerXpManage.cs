using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerXpManage : MonoBehaviour
{
    public static PlayerXpManage instance;

    public int nivel = 1;
    public int xpAtual = 0;
    public int xpParaProximoNivel = 10;

    public Slider barraXP;
    public TextMeshProUGUI nivelTexto;

    private void Awake()
    {
        instance = this;
    }

    public void GanharXP(int quantidade)
    {
        xpAtual += quantidade;

        if (xpAtual >= xpParaProximoNivel)
        {
            xpAtual -= xpParaProximoNivel;
            nivel++;
            TripulanteSelector.instance.MostrarEscolhaTripulantes(gameObject);
            xpParaProximoNivel += 10; // pode ajustar a progressão
        }

        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (barraXP != null)
        {
            barraXP.maxValue = xpParaProximoNivel;
            barraXP.value = xpAtual;
        }

        if (nivelTexto != null)
        {
            nivelTexto.text = "Lvl: " + nivel;
        }
    }
}