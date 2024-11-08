using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
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
        /*

        if (PlayerPrefs.GetInt("Language") == 1 || PlayerPrefs.GetInt("Language") == 2)
        {
            languageDropdown.value = PlayerPrefs.GetInt("Language");
            
        }
        */

        _spriteRenderer = shadow.GetComponent<SpriteRenderer>();
    }
    
    [Header("Menu Objects")]
    public GameObject dropdown;
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
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        _audioManager.PlaySFX(_audioManager.click);
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
    
    [Header("Language Settings")]
    
    [SerializeField] private TMP_Dropdown languageDropdown;
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
        { SceneManager.LoadScene("Mexico");}
        
        else if (_pickedLanguage == 3)
        { SceneManager.LoadScene("Ikea");}
    }
    
    [Header("Brightness Settings")]
    public Slider brightnessSlider;
    public GameObject shadow;

    public void ChangeBrightness()
    {
        float brightness = 1 - brightnessSlider.value;
        _spriteRenderer.color = new Color(0, 0, 0, brightness); //endrer opacity på skyggeboksen
    }
    
    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
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
}