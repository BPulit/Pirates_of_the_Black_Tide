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

    [Header("VFX")]
    public GameObject vfxHabilidadeCapitao;
    private GameObject vfxInstanciado;

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

            // Instancia o VFX como filho do navio
            if (vfxHabilidadeCapitao != null && vfxInstanciado == null)
            {
                vfxInstanciado = Instantiate(vfxHabilidadeCapitao, transform);
                
                // Define a posição atrás do barco
                vfxInstanciado.transform.localPosition = new Vector3(0, 0, -2f); // ajuste o -2f conforme o tamanho do barco
                vfxInstanciado.transform.localRotation = Quaternion.identity; // evita inclinar junto com rotação
                vfxInstanciado.transform.localScale = Vector3.one; // evita deformações
            }
        }
    }


    void Update()
    {
        if (invulneravel)
        {
            tempoRestante -= Time.deltaTime;

            // Aplica knockback
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

            // Mantém VFX acompanhando o player (mas com rotação fixa)
            if (vfxInstanciado != null)
            {
                vfxInstanciado.transform.position = transform.position;
                vfxInstanciado.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            }

            if (tempoRestante <= 0f)
            {
                invulneravel = false;
                statusPlayer.invulneravel = false;
                playerMove.multiplicadorVelocidade = 1f;
                playerMove.multiplicadorRotacao = 1f;

                if (vfxInstanciado != null)
                    Destroy(vfxInstanciado);
            }
        }
    }

    public void AplicarBoostVelocidade(float duracao, float multVel, float multRot)
    {
        if (!invulneravel)
        {
            playerMove.multiplicadorVelocidade = multVel;
            playerMove.multiplicadorRotacao = multRot;
        }

        tempoRestante += duracao;
    }
}
