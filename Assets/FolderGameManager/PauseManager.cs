using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public AudioMixer mixer;

    public Slider sliderGeral;
    public Slider sliderMusica;
    public Slider sliderSFX;
    public Slider sliderNatureza;

    public Toggle fullScreenToggle;
    public GameObject painelTripAtivo;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        painelTripAtivo.SetActive(true);
        fullScreenToggle.isOn = Screen.fullScreen;
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;

        sliderGeral.value = PlayerPrefs.GetFloat("masterVol", 0.7f);
        sliderMusica.value = PlayerPrefs.GetFloat("musicVol", 0.7f);
        sliderSFX.value = PlayerPrefs.GetFloat("sfxVol", 0.7f);
        sliderNatureza.value = PlayerPrefs.GetFloat("natureVol", 0.7f);

        // Aplica os volumes carregados
        SetVolume("masterVol", sliderGeral.value);
        SetVolume("musicVol", sliderMusica.value);
        SetVolume("sfxVol", sliderSFX.value);
        SetVolume("natureVol", sliderNatureza.value);

        // Inicializa sliders com valores de 0.0001 a 1, convertendo dB pra valor linear
        sliderGeral.value = GetLinearVolume("masterVol");
        sliderMusica.value = GetLinearVolume("musicVol");
        sliderSFX.value = GetLinearVolume("sfxVol");
        sliderNatureza.value = GetLinearVolume("natureVol");
    }

    float GetLinearVolume(string parametro)
    {
        float dB;
        if (mixer.GetFloat(parametro, out dB))
        {
            return Mathf.Pow(10f, dB / 20f);
        }
        return 0.7f; // valor padrão caso não ache
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        painelTripAtivo.SetActive(!isPaused);
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
       // AudioListener.pause = isPaused;
    }

    public void SetVolume(string parametro, float valor)
    {
        // Evita log de 0 e suaviza curva
        valor = Mathf.Clamp(valor, 0.0001f, 1f);

        float dB = Mathf.Log10(valor) * 20f;
        mixer.SetFloat(parametro, dB);
        PlayerPrefs.SetFloat(parametro, valor);
    }

    public void SetVolumeGeral(float value) => SetVolume("masterVol", sliderGeral.value);
    public void SetVolumeMusica(float value) => SetVolume("musicVol", sliderMusica.value);
    public void SetVolumeSFX(float value) => SetVolume("sfxVol", sliderSFX.value);
    public void SetVolumeNatureza(float value) => SetVolume("natureVol", sliderNatureza.value);

    public void SetFullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        PlayerPrefs.SetInt("fullscreen", isFull ? 1 : 0);
    }
    public void VoltarJogo()
    {
        TogglePause();

    }

    public void VoltarMenu()
    {
        TogglePause();
        SceneManager.LoadScene("MenuInicial");
    }
}
