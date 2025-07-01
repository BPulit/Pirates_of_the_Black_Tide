using UnityEngine;

public class BolaDeCanhao : MonoBehaviour
{
    public int dano;
    public string shooterTag;
    public GameObject shooterObject;

    [Header("VFX")]
    public GameObject vfxExplosaoPrefab;

    void Start()
    {
        if (shooterTag == "Player")
        {
            int nivel = PlayerXpManage.instance.nivel;
            int baseDano = (nivel - 1) * 2;
            dano = baseDano + StatusPlayer.Instance.ataque;
            Debug.Log(dano);
        }
        if (shooterObject != null)
        {
            Collider myCollider = GetComponent<Collider>();
            Collider shooterCollider = shooterObject.GetComponent<Collider>();
            if (myCollider != null && shooterCollider != null)
            {
                Physics.IgnoreCollision(myCollider, shooterCollider);
            }
        }

        Destroy(gameObject, 7f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(shooterTag)) return;

        SpawnExplosao(collision.contacts[0].point);

        if (shooterTag == "Player")
        {
            StatusEnemie enemy = collision.gameObject.GetComponent<StatusEnemie>();
            if (enemy != null) enemy.TakeDamage(dano);

            KrakenStatus kraken = collision.gameObject.GetComponent<KrakenStatus>();
            if (kraken != null) kraken.TakeDamage(dano);

            SeaMonsterStatus seaMonster = collision.gameObject.GetComponent<SeaMonsterStatus>();
            if (seaMonster != null) seaMonster.TomarDano(dano);
        }

        if (shooterTag == "Inimigo")
        {
            if (collision.gameObject.CompareTag("Bolha"))
            {
                Destroy(gameObject);
                return;
            }

            StatusPlayer player = collision.gameObject.GetComponent<StatusPlayer>();
            if (player != null) player.TomarDano(dano);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MinaAquatica"))
        {
            // Explode também quando tocar em minas trigger
            if (vfxExplosaoPrefab != null)
            {
                Instantiate(vfxExplosaoPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    private void SpawnExplosao(Vector3 posicao)
    {
        if (vfxExplosaoPrefab != null)
        {
            GameObject vfx = Instantiate(vfxExplosaoPrefab, posicao, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }
}
