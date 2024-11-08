using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class MenuController : MonoBehaviour
{
    [Header("Test")]
    private int _language;
    public GameObject dropdown;

    public GameObject menu;
    private SpriteRenderer _spriteRenderer;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume")) //beholder volumet fra andre scener
        {
            LoadVolume();
        }
        else
        {
            ChangeVolume();
        }

        _spriteRenderer = shadow.GetComponent<SpriteRenderer>();
    }
    

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
        audioManager.PlaySFX(audioManager.click);
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        audioManager.PlaySFX(audioManager.click);
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
    
    //Language drop-down menu
    
    [SerializeField] private TMP_Dropdown languageDropdown;
    private string _languageString;
    private int _pickedLanguage;
    
    public void ChangeLanguage()
    {
        _pickedLanguage = languageDropdown.value;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_pickedLanguage];
        LoadScene();
        
        
    }

    public void LoadScene()
    {
        if (_pickedLanguage == 0)
        {
            SceneManager.LoadScene("Tesco");
        }
        
        else if (_pickedLanguage == 1)
        {
            SceneManager.LoadScene("Main");
        }
        
        else if (_pickedLanguage == 2)
        { SceneManager.LoadScene("Mexico");}
        
        else if (_pickedLanguage == 3)
        { SceneManager.LoadScene("Ikea");}
    }
    
    //Sliders
    
    //Brightness
    public Slider brightnessSlider;
    public GameObject shadow;

    public void ChangeBrightness()
    {
        float brightness = 1 - brightnessSlider.value;
        _spriteRenderer.color = new Color(0, 0, 0, brightness);
    }
    
    //volume
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