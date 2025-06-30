using UnityEngine;

public class CameraMenuMovimento : MonoBehaviour
{
    private Vector3 posInicial;
    public float amplitude = 0.2f;     // Quão longe ela se move
    public float velocidade = 0.5f;    // Quão rápido ela se move

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        float deslocamentoY = Mathf.Sin(Time.time * velocidade) * amplitude;
        float deslocamentoX = Mathf.Sin(Time.time * velocidade * 0.6f) * amplitude * 0.5f;

        transform.position = posInicial + new Vector3(deslocamentoX, deslocamentoY, 0f);
    }
}
