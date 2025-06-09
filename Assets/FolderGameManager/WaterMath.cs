using UnityEngine;

public class WaterMath : MonoBehaviour
{
    public static float deslocamento = 0.5f; // ajuste conforme slider do material
    public static float forca = 0.8f; // idem ao 'Strength'
    public static float velocidade = 1.0f;

    public static float CalcularAlturaDaOnda(Vector3 pos)
    {
        float tempo = Time.time * velocidade;

        float deslocamentoX = Mathf.Sin((pos.x + tempo)) * forca;
        float deslocamentoZ = Mathf.Cos((pos.z + tempo)) * forca;

        return (deslocamentoX + deslocamentoZ) * deslocamento;
    }

    public static Vector3 CalcularNormalDaOnda(Vector3 pos)
    {
        float alturaX1 = CalcularAlturaDaOnda(pos + Vector3.right * 0.1f);
        float alturaX2 = CalcularAlturaDaOnda(pos - Vector3.right * 0.1f);
        float alturaZ1 = CalcularAlturaDaOnda(pos + Vector3.forward * 0.1f);
        float alturaZ2 = CalcularAlturaDaOnda(pos - Vector3.forward * 0.1f);

        float dx = (alturaX1 - alturaX2);
        float dz = (alturaZ1 - alturaZ2);

        return new Vector3(-dx, 1f, -dz).normalized;
    }
}