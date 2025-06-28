using UnityEngine;

public class DanoBossPart : MonoBehaviour
{
    public int dano = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatusPlayer player = other.GetComponent<StatusPlayer>();
            if (player != null)
            {
                player.TomarDano(dano);
                Debug.Log($"Player atingido por {gameObject.name}, dano: {dano}");
            }
        }
    }
}
