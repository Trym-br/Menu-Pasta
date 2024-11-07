using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private int _language;
    public GameObject dropdown;

    public GameObject menu;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = shadow.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeInHierarchy)
        {
            OpenMenu();
        }
        
        
    }

    void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
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
    private int pickedLanguage;
    
    public void ChangeLanguage()
    {
        pickedLanguage = languageDropdown.value;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[pickedLanguage];
        LoadScene();
        
        
    }

    public void LoadScene()
    {
        if (pickedLanguage == 0)
        { SceneManager.LoadScene("Tesco"); }
        
        else if (pickedLanguage == 1)
        { SceneManager.LoadScene("Main"); }
        
        else if (pickedLanguage == 2)
        { SceneManager.LoadScene("Mexico");}
        
        else if (pickedLanguage == 3)
        {
            SceneManager.LoadScene("Ikea");}
    }
    
    //Sliders
    
    //Brightness
    public Slider brightnessSlider;
    public GameObject shadow;

    public void ChangeBrightness()
    {
        print("changed Brightness to"+ brightnessSlider.value);
        float brightness = 1 - brightnessSlider.value;
        _spriteRenderer.color = new Color(0, 0, 0, brightness);
    }
    



}