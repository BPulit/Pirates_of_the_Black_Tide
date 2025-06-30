using UnityEngine;

public class ItemVelocidadeTemp : MonoBehaviour
{
    public float duracao = 5f;
    public float multiplicadorVelocidade = 1.5f;
    public float multiplicadorRotacao = 1.5f;
    public string tagJogador = "Player";
    public int indexSFX;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagJogador)) return;

        PlayerCapitaoControlador pc = other.GetComponent<PlayerCapitaoControlador>();
        if (pc != null)
        {
            pc.AplicarBoostVelocidade(duracao, multiplicadorVelocidade, multiplicadorRotacao);
            AudioManager.Instance.TocarSomEfeito(indexSFX); 
        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(gameObject);
    }
}
