using UnityEngine;

public class BolaDeCanhao : MonoBehaviour
{
    public int dano;
    public string shooterTag;
    public GameObject shooterObject;

    void Start()
    {
        if (shooterTag == "Player")
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

        if (shooterTag == "Player")
        {
            StatusEnemie enemy = collision.gameObject.GetComponent<StatusEnemie>();
            if (enemy != null)
            {
                enemy.TakeDamage(dano);
            }

            KrakenStatus kraken = collision.gameObject.GetComponent<KrakenStatus>();
            if (kraken != null)
            {
                kraken.TakeDamage(dano);
            }
        }

        if (shooterTag == "Inimigo")
        {
            StatusPlayer player = collision.gameObject.GetComponent<StatusPlayer>();
            if (player != null)
            {
                player.TomarDano(dano);
            }
        }

        Destroy(gameObject);
    }
}
