using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UITripulanteHUD : MonoBehaviour
{
    public static UITripulanteHUD instance;

    [Header("UI")]
    public Transform painelCartasHUD;
    public GameObject prefabCartaTripulante;

    private List<UITripulanteCarta> cartas = new(); // Cartas instanciadas na HUD

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void MostrarCarta(Tripulante tripulante, TeclaAtivacao tecla)
    {
        GameObject cartaGO = Instantiate(prefabCartaTripulante, painelCartasHUD);
        UITripulanteCarta carta = cartaGO.GetComponent<UITripulanteCarta>();

        if (carta != null)
        {
            carta.Configurar(tripulante, tecla);
            cartas.Add(carta); // guarda para futuras atualizações
        }
    }

    public void AtualizarCooldown(Tripulante tripulante, float progresso)
    {
        foreach (var carta in cartas)
        {
            if (carta.tripulante == tripulante)
            {
                carta.barraCooldown.gameObject.SetActive(progresso < 1f);
                carta.barraCooldown.value = 1f - progresso; // 0 = cheio, 1 = vazio
            }
        }
    }
}
