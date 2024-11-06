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
}