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
        if (shooterTag == "Player" && dano == 0)
        {
            dano = StatusPlayer.Instance.ataque;
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

        // Spawn do VFX de explosão na posição de impacto
        if (vfxExplosaoPrefab != null)
        {
            ContactPoint pontoImpacto = collision.contacts[0];
            GameObject vfx = Instantiate(vfxExplosaoPrefab, pontoImpacto.point, Quaternion.identity);
            Destroy(vfx, 1f); // destrói o VFX depois de 2 segundos
        }

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
}
