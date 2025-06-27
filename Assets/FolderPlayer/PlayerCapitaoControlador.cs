using UnityEngine;

public class PlayerCapitaoControlador : MonoBehaviour
{
    private PlayerMove playerMove;
    private StatusPlayer statusPlayer;
    private Collider playerCollider;

    private float tempoRestante;
    private bool invulneravel = false;

    public LayerMask layerInimigo;
    public float knockbackForce = 10f;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        statusPlayer = GetComponent<StatusPlayer>();
        playerCollider = GetComponent<Collider>();
    }

    public void AtivarManobra(float duracao, float velocidadeExtra, float rotacaoExtra)
    {
        if (playerMove != null && statusPlayer != null)
        {
            playerMove.multiplicadorVelocidade *= velocidadeExtra;
            playerMove.multiplicadorRotacao *= rotacaoExtra;
            statusPlayer.invulneravel = true;
            invulneravel = true;
            tempoRestante = duracao;
        }
    }

    void Update()
    {
        if (invulneravel)
        {
            tempoRestante -= Time.deltaTime;

            // Colisão com inimigos - aplica knockback
            Collider[] inimigos = Physics.OverlapSphere(transform.position, 2f, layerInimigo);
            foreach (var inimigo in inimigos)
            {
                Rigidbody rb = inimigo.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 dir = (inimigo.transform.position - transform.position).normalized;
                    rb.AddForce(dir * knockbackForce, ForceMode.Impulse);
                }
            }

            if (tempoRestante <= 0f)
            {
                invulneravel = false;
                statusPlayer.invulneravel = false;
                playerMove.multiplicadorVelocidade = 1f;
                playerMove.multiplicadorRotacao = 1f;
            }
        }
    }
    public void AplicarBoostVelocidade(float duracao, float multVel, float multRot)
    {
        if (!invulneravel) // se não for o Capitão ativo
        {
            playerMove.multiplicadorVelocidade = multVel;
            playerMove.multiplicadorRotacao = multRot;
        }

        tempoRestante += duracao; // acumula tempo se já estiver ativo
    }
}
