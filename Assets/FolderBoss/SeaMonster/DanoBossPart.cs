using UnityEngine;

public class DanoBossPart : MonoBehaviour
{
    public int dano = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bolha"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Transform bolha = other.transform.Find("BolhaProtetora(Clone)");
            if (bolha != null && bolha.gameObject.activeInHierarchy)
            {
                return;
            }

            StatusPlayer player = other.GetComponent<StatusPlayer>();
            if (player != null)
            {
                player.TomarDano(dano);
            }
        }
    }
}
