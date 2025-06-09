using System.Collections.Generic;
using UnityEngine;

public class TripulanteManager : MonoBehaviour
{
    public static TripulanteManager instance;

    private Dictionary<TeclaAtivacao, TripulanteStatus> tripulantesAtivos = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        foreach (var entrada in tripulantesAtivos)
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), entrada.Key.ToString());
            if (Input.GetKeyDown(key))
            {
                entrada.Value.TentarAtivar();
            }
        }
    }

    public void RegistrarTripulante(Tripulante tripulante, TeclaAtivacao tecla, GameObject jogador)
    {
        if (!tripulantesAtivos.ContainsKey(tecla))
        {
            tripulantesAtivos[tecla] = new TripulanteStatus(tripulante, jogador);
            UITripulanteHUD.instance.MostrarCarta(tripulante, tecla); // HUD
        }
    }
}
