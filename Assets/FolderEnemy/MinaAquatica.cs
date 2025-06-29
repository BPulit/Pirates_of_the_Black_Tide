using UnityEngine;

public class MinaAquatica : MonoBehaviour
{
    public int dano = 1;
    public GameObject vfxExplosao;
    public float tempoParaDestruirVFX = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplica dano no player
            StatusPlayer.Instance.TomarDano(dano);

            // Efeito visual
            if (vfxExplosao != null)
            {
                GameObject vfx = Instantiate(vfxExplosao, transform.position, Quaternion.identity);
                Destroy(vfx, tempoParaDestruirVFX);
            }

            // Som de explosão, se quiser
            AudioManager.Instance?.TocarSomEfeito(8);

            // Destrói a mina
            Destroy(gameObject);
        }
    }
}
