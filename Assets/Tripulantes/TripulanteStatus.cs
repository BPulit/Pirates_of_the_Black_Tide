using UnityEngine;

public class TripulanteStatus
{
    public Tripulante tripulante;
    private float tempoUltimoUso;
    private GameObject jogador;

    public TripulanteStatus(Tripulante t, GameObject j)
    {
        tripulante = t;
        jogador = j;
        tempoUltimoUso = -999f;
    }

    public void TentarAtivar()
    {
        if (Time.time >= tempoUltimoUso + tripulante.cooldown)
        {
            tripulante.AtivarHabilidade(jogador);
            tempoUltimoUso = Time.time;
            UITripulanteHUD.instance.IniciarCooldown(tripulante); // Atualiza barra
        }
    }

    public float GetCooldownRestante()
    {
        return Mathf.Max(0f, (tempoUltimoUso + tripulante.cooldown) - Time.time);
    }
}
