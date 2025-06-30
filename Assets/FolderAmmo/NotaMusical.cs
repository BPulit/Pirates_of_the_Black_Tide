using UnityEngine;

public class NotaMusical : MonoBehaviour
{
    public int dano = 3;
    private void Start()
    {
        Destroy(gameObject, 40f);
    }

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
            
            var seaMonster = other.GetComponent<SeaMonsterStatus>();
            seaMonster.TomarDano(dano);

            
            Destroy(gameObject);
        }
    }
}
