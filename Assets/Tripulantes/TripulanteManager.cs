using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
            entrada.Value.Atualizar(Time.deltaTime);

            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), entrada.Key.ToString());
            if (Input.GetKeyDown(key))
            {
                entrada.Value.TentarAtivar();
            }
        }
    }

    public void ReduzirCooldownTemporariamente(float fator, float duracao, Tripulante excecao)
    {
        StartCoroutine(ReduzirCooldownCoroutine(fator, duracao, excecao));
    }

    private IEnumerator ReduzirCooldownCoroutine(float fator, float duracao, Tripulante excecao)
    {
        foreach (var t in tripulantesAtivos)
        {
            if (t.Value.tripulante != excecao)
            {
                t.Value.AplicarModificadorCooldown(fator);
            }
        }

        yield return new WaitForSeconds(duracao);

        foreach (var t in tripulantesAtivos)
        {
            t.Value.ResetarModificadorCooldown();
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

    public bool PossuiTripulante(Tripulante tripulante)
    {
        foreach (var t in tripulantesAtivos.Values)
        {
            if (t.tripulante != null && t.tripulante.idUnico == tripulante.idUnico)
                return true;
        }
        return false;
    }
    
    public bool TeclaOcupada(TeclaAtivacao tecla)
    {
        return tripulantesAtivos.ContainsKey(tecla);
    }


}
