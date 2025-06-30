using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject balaPrefab;
    public Transform cannonPoint; // Apenas um ponto frontal
    public float shootForce = 500f;
    public float cooldownTime = 2f;
    public float attackRange = 20f;

    private float cooldown = 0f;
    public float spawnOffset = 1f;

    private Transform player;
    [HideInInspector] public bool congelado = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (congelado || player == null) return;

        if (cooldown > 0f) cooldown -= Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && cooldown <= 0f)
        {
            Atirar();
            cooldown = cooldownTime;
        }
    }

    void Atirar()
    {
        int nivelJogador = PlayerXpManage.instance.nivel;
        int danoCalculado = Random.Range(nivelJogador, nivelJogador + 1);

        Vector3 spawnPosition = cannonPoint.position + transform.forward * spawnOffset;
        GameObject bala = Instantiate(balaPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = bala.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootForce);

        BolaDeCanhao scriptBala = bala.GetComponent<BolaDeCanhao>();
        scriptBala.shooterTag = "Inimigo";
        scriptBala.dano = danoCalculado;

        AudioManager.Instance.TocarSomDirecional(0, transform.position);

        Debug.Log("Inimigo atirou: " + danoCalculado);
    }
}
