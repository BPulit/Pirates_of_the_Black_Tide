using UnityEngine;

public class NotaMusical : MonoBehaviour
{
    public int dano = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            var status = other.GetComponent<StatusEnemie>();
            if (status != null) status.TakeDamage(dano);
            Destroy(gameObject);
        }

        if (other.CompareTag("Boss"))
        {
            var kraken = other.GetComponent<KrakenStatus>();
            if (kraken != null) kraken.TakeDamage(dano);
            Destroy(gameObject);
        }
    }
}
