using UnityEngine;
using TMPro;
using System.Collections;

public class MensagemUI : MonoBehaviour
{
    public static MensagemUI instance;

    public TextMeshProUGUI textoMensagem;

    private void Awake()
    {
        instance = this;
    }

    public void MostrarMensagem(string mensagem, float duracao = 2f)
    {
        StopAllCoroutines();
        textoMensagem.text = mensagem;
        textoMensagem.gameObject.SetActive(true);
        StartCoroutine(EsconderDepois(duracao));
    }

    IEnumerator EsconderDepois(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos);
        textoMensagem.gameObject.SetActive(false);
    }
}
