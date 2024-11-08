using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Ambience")]
    public AudioClip mainBackground;
    public AudioClip tescoBackground;
    public AudioClip ikeaBackground;
    public AudioClip mexicoBackground;
    
    [Header("SFX")]
    public AudioClip click;
    public AudioClip tomatoDrop;
    public AudioClip pastaDrop;
    public AudioClip waterBoiling;
    public AudioClip victory;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Main" || sceneName == "Maya")
        {
            ambienceSource.clip = mainBackground;
        }
        else if (sceneName == "Tesco")
        {
            ambienceSource.clip = tescoBackground;
        }
        else if (sceneName == "Ikea")
        {
            ambienceSource.clip = ikeaBackground;
        }
        else if (sceneName == "Mexico")
        {
            ambienceSource.clip = mexicoBackground;
        }

        ambienceSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}