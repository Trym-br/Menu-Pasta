using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;


public class MenuController : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) //beholder volumvalg fra tidligere scener
        {
            LoadVolume();
        }
        else
        {
            ChangeVolume();
        }

        if (PlayerPrefs.HasKey("Brightness"))
        {
            LoadBrightness();
        }
        else
        {
            ChangeBrightness();
        }
        /*

        if (PlayerPrefs.GetInt("Language") == 1 || PlayerPrefs.GetInt("Language") == 2)
        {
            languageDropdown.value = PlayerPrefs.GetInt("Language");

        }
        */
    }

    [Header("Menu Objects")] public GameObject dropdown;
    public GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeInHierarchy)
        {
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && dropdown.activeInHierarchy)
        {
            CloseMenu();
        }
    }

    public void OpenMenu()
    {
        _audioManager.PlaySFX(_audioManager.click);
        ItemManager.Instance._menuOpen = true;
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        _audioManager.PlaySFX(_audioManager.click);
        ItemManager.Instance._menuOpen = false;
        menu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }

    [Header("Language Settings")] [SerializeField]
    private TMP_Dropdown languageDropdown;

    private string _languageString;
    private int _pickedLanguage;

    public void ChangeLanguage()
    {
        _pickedLanguage = languageDropdown.value;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_pickedLanguage];
        //PlayerPrefs.SetInt("Language", _pickedLanguage);
        LoadScene();
    }

    private void LoadScene()
    {
        if (_pickedLanguage == 0)
        {
            SceneManager.LoadScene("Tesco");
        }
        else if (_pickedLanguage == 1)
        {
            SceneManager.LoadScene("Main");
        }
        /*
        else if (_pickedLanguage == 2)
        {
            SceneManager.LoadScene("Main");
        }*/

        else if (_pickedLanguage == 2)
        {
            SceneManager.LoadScene("Mexico");
        }

        else if (_pickedLanguage == 3)
        {
            SceneManager.LoadScene("Ikea");
        }
    }

    [Header("Brightness Settings")] public Slider brightnessSlider;
    public GameObject shadow;
    public Light2D ovenLight;
    public Light2D globalLight;
    private bool _ovenInMain = true;
    private bool _cookingPasta;

    public void ChangeBrightness()
    {
        float brightness = brightnessSlider.value;
        if (brightness < 1f) { globalLight.intensity = brightness; }
        else { globalLight.intensity = 1f; }
        PlayerPrefs.SetFloat("Brightness", brightness);
        
        if (_ovenInMain)
        {
            if (brightness > 1f && !_cookingPasta)
            {
                ovenLight.intensity = brightness * 9;
                //kjør "kok pasta" her.
                print("cooking pasta!");
                _cookingPasta = true;
            }
            else
            {
                ovenLight.intensity = 0;
            }
        }
    }

    [Header("Audio Settings")] [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField] private Slider volumeSlider;

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        ChangeVolume();
    }

    private void LoadBrightness()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness");
    }
}