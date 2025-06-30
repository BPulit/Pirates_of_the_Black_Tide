using UnityEngine;
using System.Collections;

public class NotaMusical : MonoBehaviour
{
    public int dano = 3;

    [Header("VFX")]
    public GameObject vfxNotaPrefab;
    private GameObject vfxInstanciado;

    private void Start()
    {
        // Inicia a corrotina para instanciar o VFX depois de 1 segundo
        StartCoroutine(AtivarVFXComDelay(0.4f));

        Destroy(gameObject, 40f);
    }

    IEnumerator AtivarVFXComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (vfxNotaPrefab != null)
        {
            vfxInstanciado = Instantiate(vfxNotaPrefab, transform.position, Quaternion.identity, transform);
        }
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
            if (seaMonster != null) seaMonster.TomarDano(dano);

            Destroy(gameObject);
        }
    }
}
