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
    private Coroutine ciclo;

    void Start()
    {
        rend = GetComponent<Renderer>();
        materialInstanciado = rend.material;
        corOriginal = materialInstanciado.color;

        // Inicia ciclo
        ciclo = StartCoroutine(CicloBolha());
    }

    public void ReiniciarDuracao(float novaDuracao)
    {
        duracao = novaDuracao;

        if (ciclo != null)
            StopCoroutine(ciclo);

        ciclo = StartCoroutine(CicloBolha());
    }

    IEnumerator CicloBolha()
    {
        yield return new WaitForSeconds(duracao);

        float tempo = 0f;
        while (tempo < tempoPiscando)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * velocidadePiscar));
            Color novaCor = new Color(corOriginal.r, corOriginal.g, corOriginal.b, alpha);
            materialInstanciado.color = novaCor;
            yield return null;
        }

        if (StatusPlayer.Instance != null && StatusPlayer.Instance.bolhaAtual == this.gameObject)
            StatusPlayer.Instance.bolhaAtual = null;

        Destroy(gameObject);
    }
}
