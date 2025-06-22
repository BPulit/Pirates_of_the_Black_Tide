using UnityEngine;
using System.Collections;

public class AreaLoja : MonoBehaviour
{
    public GameObject painelLoja;
    public GameObject painelTriAtivo;
    public PlayerMove playerMove;

    private Collider areaCollider;

    void Start()
    {
        areaCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (painelLoja != null) painelLoja.SetActive(true);
            painelTriAtivo.SetActive(false);
            if (playerMove != null) playerMove.enabled = false; // trava o navio
            Time.timeScale = 0f; // pausa o tempo
        }
    }

    public void FecharLoja()
    {
        if (painelLoja != null) painelLoja.SetActive(false);
        painelTriAtivo.SetActive(true);
        if (playerMove != null) playerMove.enabled = true; // volta o controle do navio
        Time.timeScale = 1f; // volta o tempo

        // Desativa o collider por alguns segundos para não reabrir a loja imediatamente
        if (areaCollider != null)
        {
            StartCoroutine(ReativarColliderDepoisDeTempo());
        }
    }

    IEnumerator ReativarColliderDepoisDeTempo()
    {
        areaCollider.enabled = false;
        yield return new WaitForSeconds(5f); // Tempo que o player tem para sair da área
        areaCollider.enabled = true;
    }
}