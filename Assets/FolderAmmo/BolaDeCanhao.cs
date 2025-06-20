using UnityEngine;

public class BolaDeCanhao : MonoBehaviour
{
    public int dano;
    public string shooterTag;
    public GameObject shooterObject;

    void Start()
    {
        // Se a bala foi disparada pelo Player e o dano ainda não foi atribuído
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
                if (collision.gameObject.CompareTag("Bolha"))
                {
                    Destroy(gameObject); // Bala destruída ao colidir com a bolha
                    return;
                }
                StatusPlayer player = collision.gameObject.GetComponent<StatusPlayer>();
                if (player != null)
                {
                    player.TomarDano(dano);
                }
        }

        Destroy(gameObject);
    }
}
