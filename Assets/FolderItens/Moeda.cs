using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valor = 1;            // Quantidade de moedas que essa moeda vale
    public float velocidadeRotacao = 90f; // Graus por segundo
 

    void Update()
    {
        // Faz a moeda girar no eixo Y (verticalmente)
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona moedas no CurrencyManager
            if (CurrencyManager.instance != null)
            {
                CurrencyManager.instance.AdicionarMoedas(valor);
                AudioManager.Instance.TocarSomDirecional(1, transform.position);
            }

            // Destrói a moeda
            Destroy(gameObject);
        }
    }
}