using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class BossKrakenControler : MonoBehaviour
{
    public List<Transform> tentaculos;    
    public float velocidadeRotacao = 20f;
    private bool podeGirar = true;
    public float intervaloEntreAtaques = 3f;

    void Start()
    {
        StartCoroutine(CicloDeAtaque());
    }

    void Update()
    {
            if (podeGirar)
        {
            transform.Rotate(Vector3.up * velocidadeRotacao * Time.deltaTime);
        }
        
    }
    IEnumerator CicloDeAtaque()
{
    while (true)
    {
        yield return new WaitForSeconds(intervaloEntreAtaques);

        podeGirar = false;

        // Escolhe um tentáculo aleatório
        int index = Random.Range(0, tentaculos.Count);
        Transform tentaculo = tentaculos[index];

        // Anima tentáculo (ex: mover Y, rotacionar, etc)
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
            t += Time.deltaTime * 2; // velocidade do ataque
            tentaculo.localPosition = Vector3.Lerp(posOriginal, posAlvo, t);
            yield return null;
        }

        // Espera um segundo abaixado
        yield return new WaitForSeconds(0.5f);

        // Sobe de volta
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 2;
            tentaculo.localPosition = Vector3.Lerp(posAlvo, posOriginal, t);
            yield return null;
        }
}
}
