using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valor = 1;
    public float velocidadeRotacao = 90f;

    [Header("VFX")]
    public GameObject vfxDropPrefab;

    private GameObject vfxInstanciado;

    private void Start()
    {
        // Instancia o VFX de item dropado na mesma posição da moeda
        if (vfxDropPrefab != null)
        {
            vfxInstanciado = Instantiate(vfxDropPrefab, transform.position, Quaternion.identity);
            vfxInstanciado.transform.SetParent(transform); // Faz o VFX seguir a moeda
        }

        Destroy(gameObject, 40f);
        Destroy(vfxInstanciado, 40f);
    }

    void Update()
    {
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrencyManager.instance != null)
            {
                CurrencyManager.instance.AdicionarMoedas(valor);
                AudioManager.Instance.TocarSomDirecional(1, transform.position);
            }

            // Destroi o VFX junto com a moeda
            if (vfxInstanciado != null)
            {
                Destroy(vfxInstanciado);
            }

            Destroy(gameObject);
        }
    }
}
