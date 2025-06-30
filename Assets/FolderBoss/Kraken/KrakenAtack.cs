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

        // Impede dano se colidiu com a bolha
        if (collision.gameObject.CompareTag("Bolha"))
        {
            return;
        }

        if (collision.gameObject.CompareTag(targetTag))
        {
            // Verifica se o player está protegido pela bolha
            Transform playerTransform = collision.transform;

            // Busca por objeto com tag "Bolha" como filho do jogador
            Transform bolha = playerTransform.Find("BolhaProtetora(Clone)");
            if (bolha != null && bolha.gameObject.activeInHierarchy)
            {
                Debug.Log("Dano bloqueado pelo escudo do ciborgue.");
                return;
            }

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