using UnityEngine;

public class Bau : MonoBehaviour
{
    public int quantidadeMoedas = 30;

    private void Start()
    {

        Destroy(gameObject, 40f);
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyManager.instance.AdicionarMoedas(quantidadeMoedas);
            AudioManager.Instance.TocarSomEfeito(6); // som opcional
            Destroy(gameObject);
        }
    }
    
}