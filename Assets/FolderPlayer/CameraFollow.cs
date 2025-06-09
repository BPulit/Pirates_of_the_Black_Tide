using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;
    public Vector3 offsetLocal = new Vector3(0, 5, -10);
    public float suavizacao = 5f;
    public float anguloVertical = 15f;

    // Configurações da câmera para boss fight
    public Vector3 offsetBoss = new Vector3(0, 15, -20);
    public float anguloBoss = 45f;
    public float tempoTransicao = 2f;

    private bool usandoCameraBoss = false;
    private float tempoInicioTransicao;

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 offsetAtual = usandoCameraBoss ? offsetBoss : offsetLocal;
        float anguloAtual = usandoCameraBoss ? anguloBoss : anguloVertical;

        // Corrigido: usar posição + rotação * offset
        Vector3 posDesejada = alvo.position + alvo.rotation * offsetAtual;
        transform.position = Vector3.Lerp(transform.position, posDesejada, Time.deltaTime * suavizacao);

        Quaternion rotacaoBase = Quaternion.LookRotation(alvo.forward);
        Quaternion inclinacao = Quaternion.Euler(anguloAtual, 0f, 0f);
        Quaternion rotacaoFinal = rotacaoBase * inclinacao;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoFinal, Time.deltaTime * suavizacao);
    }

    public void AtivarCameraBoss()
    {
        usandoCameraBoss = true;
        tempoInicioTransicao = Time.time;
    }

    public void VoltarCameraNormal()
    {
        usandoCameraBoss = false;
        tempoInicioTransicao = Time.time;
    }
}
