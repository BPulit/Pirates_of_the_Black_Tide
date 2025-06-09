using UnityEngine;

public class KrakenAttack : MonoBehaviour
{
    public int danoTentaculo = 1;
    public string targetTag = "Player"; // Quem o tentáculo pode atingir
    public float cooldownDano = 1.5f;    // Tempo de recarga entre danos
    private bool podeAtacar = true;

    void OnCollisionEnter(Collision collision)
    {
        if (!podeAtacar) return;

        if (collision.gameObject.CompareTag(targetTag))
        {
            StatusPlayer player = collision.gameObject.GetComponent<StatusPlayer>();
            if (player != null)
            {
                player.TomarDano(danoTentaculo);
                StartCoroutine(Cooldown());
            }
        }
    }

    System.Collections.IEnumerator Cooldown()
    {
        podeAtacar = false;
        yield return new WaitForSeconds(cooldownDano);
        podeAtacar = true;
    }
}