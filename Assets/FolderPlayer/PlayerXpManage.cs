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

    void Update()
    {
        // CHEAT: Aperte "L" para ganhar XP suficiente e subir de nível
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("CHEAT: Subindo de nível...");
            GanharXP(xpParaProximoNivel);
        }
    }

    public void GanharXP(int quantidade)
{
    xpAtual += quantidade;

    while (xpAtual >= xpParaProximoNivel)
    {
        xpAtual -= xpParaProximoNivel;
        nivel++;
        xpParaProximoNivel += 10;

        
        if (nivel <= 6)
        {
            TripulanteSelector.instance.MostrarEscolhaTripulantes(gameObject);
        }
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
