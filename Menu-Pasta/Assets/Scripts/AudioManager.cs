using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip mainBackground;
    public AudioClip tescoBackground;
    public AudioClip ikeaBackground;
    public AudioClip mexicoBackground;
    
    public AudioClip click;
    public AudioClip tomatoSplat;
    public AudioClip pastaCooking;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene ();
        string sceneName = currentScene.name;

        if (sceneName == "Main")
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
