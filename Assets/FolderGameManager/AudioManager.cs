using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mixer;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource natureSource;
    public GameObject audioSource3DPrefab;

    public AudioClip[] musicas;
    public AudioClip[] efeitos;

    public AudioClip[] natureza;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TocarSomEfeito(int index)
    {
        if (index >= 0 && index < efeitos.Length)
        {
            sfxSource.PlayOneShot(efeitos[index]);
        }
    }
    public void TocarSomNaturza(int index)
        {
            if (index < 0 || index >= natureza.Length || natureSource == null)
                return;

            natureSource.clip = natureza[index];
            natureSource.loop = true;
            natureSource.Play();
        }

     public void PararSomNatureza()
        {
           if (natureSource != null)
                    natureSource.Stop();
         }

    public void TocarMusica(int index)
    {
        if (index < 0 || index >= musicas.Length || musicSource == null)
            return;

        musicSource.clip = musicas[index];
        musicSource.loop = true;
        musicSource.Play();
    }

     public void PararMusica()
        {
           if (musicSource != null)
                    musicSource.Stop();
         }

    public void TocarSomDirecional(int index, Vector3 origem)
    {
        if (index < 0 || index >= efeitos.Length || audioSource3DPrefab == null)
            return;

        GameObject tempGO = Instantiate(audioSource3DPrefab, origem, Quaternion.identity);
        AudioSource tempSource = tempGO.GetComponent<AudioSource>();

        tempSource.clip = efeitos[index];
        tempSource.Play();

        Destroy(tempGO, efeitos[index].length);
    }
}


