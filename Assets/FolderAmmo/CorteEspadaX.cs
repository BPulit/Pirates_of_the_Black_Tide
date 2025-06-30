using UnityEngine;

public class CorteEspadaX : MonoBehaviour
{
    public int dano = 5;

    [Header("VFX")]
    public GameObject vfxCortePrefab;

    private GameObject vfxInstanciado;

    private void Start()
    {
        // Instancia o VFX de corte como filho do corte
        if (vfxCortePrefab != null)
        {
            vfxInstanciado = Instantiate(vfxCortePrefab, transform.position, transform.rotation, transform);
        }

        Destroy(gameObject, 20f); // tempo de vida do corte (e do VFX, se for filho)
    }

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
