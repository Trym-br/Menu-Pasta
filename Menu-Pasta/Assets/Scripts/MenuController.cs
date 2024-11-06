using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
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
}