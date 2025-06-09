using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaManager : MonoBehaviour
{
    void Start()
    {
        string nomeCena = SceneManager.GetActiveScene().name;

        switch (nomeCena)
        {
            case "MenuInicial":
                AudioManager.Instance.TocarMusica(1);
                AudioManager.Instance.TocarSomNaturza(0);
                AudioManager.Instance.TocarSomNaturza(0);
                break;

            case "Fase1":
                AudioManager.Instance.TocarMusica(0);
                AudioManager.Instance.TocarSomNaturza(0);
                AudioManager.Instance.TocarSomNaturza(1);
                break;

            default:
                Debug.LogWarning("Cena sem som configurado.");
                break;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("MenuInicial");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Fase1");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("Fase2");
        }
    }
}
