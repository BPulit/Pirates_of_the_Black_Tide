using System.Collections;
using UnityEngine;

public class BolhaProtetora : MonoBehaviour
{
    public float duracao = 5f;
    public float tempoPiscando = 2f;
    public float velocidadePiscar = 5f;

    private Renderer rend;
    private Color corOriginal;
    private Material materialInstanciado;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Instancia material para não alterar o original
        materialInstanciado = rend.material;
        corOriginal = materialInstanciado.color;

        // Inicia ciclo de vida da bolha
        StartCoroutine(CicloBolha());
    }

    IEnumerator CicloBolha()
    {
        yield return new WaitForSeconds(duracao);

        float tempo = 0f;
        while (tempo < tempoPiscando)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * velocidadePiscar)); // pisca entre 0 e 1
            Color novaCor = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);
            materialInstanciado.color = novaCor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
