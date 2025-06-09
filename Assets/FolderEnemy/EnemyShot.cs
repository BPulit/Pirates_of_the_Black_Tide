using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform leftCannonPoint;
    public Transform rightCannonPoint;
    public float shootForce = 500f;
    public float cooldownTime = 2f;
    public float attackRange = 20f;

    private float cooldown = 0f;
    public float spawnOffset = 1f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (cooldown > 0f) cooldown -= Time.deltaTime;

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && cooldown <= 0f)
        {
            int nivelJogador = PlayerXpManage.instance.nivel; 
            int danoCalculado = Random.Range(nivelJogador, nivelJogador + 1);

            Vector3 toPlayer = player.position - transform.position;
            float side = Vector3.Dot(toPlayer.normalized, transform.right);

            if (side > 0f)
            {
                // Player está à direita
                Vector3 spawnPosition = rightCannonPoint.position + (transform.right * spawnOffset);
                GameObject bala = Instantiate(balaPrefab, spawnPosition, Quaternion.identity);
                AudioManager.Instance.TocarSomDirecional(0, transform.position);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                rb.AddForce(transform.right * shootForce);

                BolaDeCanhao scriptBala = bala.GetComponent<BolaDeCanhao>();
                scriptBala.shooterTag = "Inimigo";
                scriptBala.dano = danoCalculado;
                Debug.Log("Ataque: " + danoCalculado);
            }
            else
            {
                // Player está à esquerda
                Vector3 spawnPosition = leftCannonPoint.position + (-transform.right * spawnOffset);
                GameObject bala = Instantiate(balaPrefab, spawnPosition, Quaternion.identity);
                AudioManager.Instance.TocarSomDirecional(0, transform.position);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                rb.AddForce(-transform.right * shootForce);

                BolaDeCanhao scriptBala = bala.GetComponent<BolaDeCanhao>();
                scriptBala.shooterTag = "Inimigo";
                scriptBala.dano = danoCalculado;
                Debug.Log("Ataque: " + danoCalculado);
            }

            cooldown = cooldownTime;
        }
    }
}
