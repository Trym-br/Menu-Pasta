using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        Debug.Log("Item Manager loaded");
    }

    private void OnEnable()
    {
        // Register the OnSceneLoaded callback method
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unregister the callback to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene loaded: " + scene.name);
    }
}