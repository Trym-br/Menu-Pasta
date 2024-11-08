using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class VictoryScreenController : MonoBehaviour
{
    private AudioManager _audioManager;
    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        _audioManager.PlaySFX(_audioManager.victory);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Maya");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }
}
