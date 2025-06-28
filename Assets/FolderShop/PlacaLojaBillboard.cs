using UnityEngine;

public class PlacaLojaBillboard : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");
        if (jogador != null)
        {
            player = jogador.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 direcao = player.position - transform.position;
        direcao.y = 0f;

        if (direcao.sqrMagnitude > 0.001f)
        {
            Quaternion rotacao = Quaternion.LookRotation(direcao);
            // Corrige rotação invertida
            rotacao *= Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = rotacao;
        }
    }
}
