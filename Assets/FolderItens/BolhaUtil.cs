using UnityEngine;

public static class BolhaUtil
{
    public static void AtivarBolha(GameObject jogador, GameObject prefabBolha, float tempo)
    {
        StatusPlayer status = jogador.GetComponent<StatusPlayer>();

        if (status == null) return;

        if (status.bolhaAtual != null)
        {
            // Já tem bolha: só renova
            BolhaProtetora antiga = status.bolhaAtual.GetComponent<BolhaProtetora>();
            if (antiga != null)
            {
                antiga.ReiniciarDuracao(tempo);
                return;
            }
        }

        // Instancia nova bolha
        GameObject novaBolha = GameObject.Instantiate(prefabBolha, jogador.transform.position, Quaternion.identity);
        novaBolha.transform.SetParent(jogador.transform);
        novaBolha.layer = LayerMask.NameToLayer("Protecao");

        BolhaProtetora script = novaBolha.GetComponent<BolhaProtetora>();
        script.duracao = tempo;

        status.bolhaAtual = novaBolha;
    }
}
