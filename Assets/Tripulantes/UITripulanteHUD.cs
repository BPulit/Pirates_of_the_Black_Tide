using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UITripulanteHUD : MonoBehaviour
{
    public static UITripulanteHUD instance;

    [Header("UI")]
    public Transform painelCartasHUD;          // Painel que vai conter as cartas
    public GameObject prefabCartaTripulante;   // Prefab da carta HUD

    private Dictionary<Tripulante, Slider> cooldownsAtivos = new();

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
            cooldownsAtivos[tripulante] = carta.barraCooldown;
        }
    }

    public void IniciarCooldown(Tripulante tripulante)
    {
        if (cooldownsAtivos.ContainsKey(tripulante))
        {
            Slider barra = cooldownsAtivos[tripulante];
            float tempo = tripulante.cooldown;
            StartCoroutine(AtualizarCooldown(barra, tempo));
        }
    }

    private System.Collections.IEnumerator AtualizarCooldown(Slider barra, float tempo)
    {
        barra.gameObject.SetActive(true);
        float t = tempo;

        while (t > 0)
        {
            barra.value = t / tempo;
            t -= Time.deltaTime;
            yield return null;
        }

        barra.value = 0;
        barra.gameObject.SetActive(false);
    }
}
