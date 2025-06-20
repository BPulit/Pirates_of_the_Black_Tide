using UnityEngine;

public class TripulanteStatus
{
    public Tripulante tripulante;
    private float tempoUltimoUso;
    private GameObject jogador;

    private float modificadorCooldown = 1f;

    public TripulanteStatus(Tripulante t, GameObject j)
    {
        tripulante = t;
        jogador = j;
        tempoUltimoUso = -999f;
    }

    public void TentarAtivar()
    {
        float cooldownModificado = tripulante.cooldown * modificadorCooldown;

        if (Time.time >= tempoUltimoUso + cooldownModificado)
        {
            tripulante.AtivarHabilidade(jogador);
            tempoUltimoUso = Time.time;
        }
    }

    public void Atualizar(float deltaTime)
    {
        float cooldownModificado = tripulante.cooldown * modificadorCooldown;
        float tempoDecorrido = Time.time - tempoUltimoUso;
        float progresso = Mathf.Clamp01(tempoDecorrido / cooldownModificado);

        UITripulanteHUD.instance.AtualizarCooldown(tripulante, progresso);
    }

    public void AplicarModificadorCooldown(float fator)
    {
        modificadorCooldown = fator;
    }

    public void ResetarModificadorCooldown()
    {
        modificadorCooldown = 1f;
    }
}


