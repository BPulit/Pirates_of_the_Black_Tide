using UnityEngine;

public class CorteEspadaX : MonoBehaviour
{
    public int dano = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            StatusEnemie inimigo = other.GetComponent<StatusEnemie>();
            if (inimigo != null)
            {
                inimigo.TakeDamage(dano);
            }
        }

        if (other.CompareTag("Boss"))
        {
            KrakenStatus boss = other.GetComponent<KrakenStatus>();
            if (boss != null)
            {
                boss.TakeDamage(dano);
            }
        }
    }
}
