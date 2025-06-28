using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo; // Navio (player)
    public float suavizacao = 5f;

    [Header("Câmera Normal")]
    public Vector3 offsetLocal = new Vector3(0, 5, -10);
    public float anguloVertical = 15f;

    [Header("Câmera Boss (Orbital)")]
    public float distanciaOrbital = 40f; // Distância entre câmera e o boss
    public float alturaOrbital = 20f;
    public float anguloHorizontal = 90f; // Ângulo ao redor do boss

    private bool usandoCameraBoss = false;
    private Transform alvoBoss;

    void LateUpdate()
    {
        if (alvo == null) return;

        if (usandoCameraBoss && alvoBoss != null)
        {
            AtualizarCameraBoss();
        }
        else
        {
            AtualizarCameraNormal();
        }
    }

    void AtualizarCameraNormal()
    {
        Vector3 posDesejada = alvo.position + alvo.rotation * offsetLocal;
        transform.position = Vector3.Lerp(transform.position, posDesejada, Time.deltaTime * suavizacao);

        Quaternion rotacaoBase = Quaternion.LookRotation(alvo.forward);
        Quaternion inclinacao = Quaternion.Euler(anguloVertical, 0f, 0f);
        Quaternion rotacaoFinal = rotacaoBase * inclinacao;

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoFinal, Time.deltaTime * suavizacao);
    }

    void AtualizarCameraBoss()
    {
        // Calcula posição ao redor do boss com base no ângulo horizontal
        Quaternion rot = Quaternion.Euler(0, anguloHorizontal, 0);
        Vector3 posOffset = rot * (Vector3.back * distanciaOrbital);
        posOffset.y = alturaOrbital;

        Vector3 posDesejada = alvoBoss.position + posOffset;
        transform.position = Vector3.Lerp(transform.position, posDesejada, Time.deltaTime * suavizacao);

        // Faz a câmera olhar para o navio
        Vector3 direcao = (alvo.position - transform.position).normalized;
        Quaternion rotDesejada = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotDesejada, Time.deltaTime * suavizacao);
    }

    public void AtivarCameraBoss()
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
        {
            alvoBoss = bossObj.transform;
            usandoCameraBoss = true;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com tag 'Boss' encontrado.");
        }
    }

    public void VoltarCameraNormal()
    {
        usandoCameraBoss = false;
        alvoBoss = null;
    }
}
