using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BossKrakenControler : MonoBehaviour
{
    [Header("Tentáculos do Kraken")]
    public List<Transform> tentaculos;    
    public float velocidadeRotacao = 20f;
    public float intervaloEntreAtaques = 3f;

    private bool podeGirar = true;
    private bool estaCongelado = false;

    private Coroutine cicloAtaqueCoroutine;

    void Start()
    {
        cicloAtaqueCoroutine = StartCoroutine(CicloDeAtaque());
    }

    void Update()
    {
        if (podeGirar && !estaCongelado)
        {
            transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
        }
    }

    IEnumerator CicloDeAtaque()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloEntreAtaques);

            if (estaCongelado) continue;

            podeGirar = false;

            int index = Random.Range(0, tentaculos.Count);
            Transform tentaculo = tentaculos[index];

            yield return StartCoroutine(AbaixarETerminar(tentaculo));

            podeGirar = true;
        }
    }

    IEnumerator AbaixarETerminar(Transform tentaculo)
    {
        Vector3 posOriginal = tentaculo.localPosition;
        Vector3 posAlvo = posOriginal + Vector3.down * 2f;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            if (!estaCongelado)
                tentaculo.localPosition = Vector3.Lerp(posOriginal, posAlvo, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            if (!estaCongelado)
                tentaculo.localPosition = Vector3.Lerp(posAlvo, posOriginal, t);
            yield return null;
        }
    }

    public void Congelar(float duracao)
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(CongelarCoroutine(duracao));
    }

    IEnumerator CongelarCoroutine(float duracao)
    {
        estaCongelado = true;
        yield return new WaitForSeconds(duracao);
        estaCongelado = false;
    }
}

