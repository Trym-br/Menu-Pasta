using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    public GameObject[] items; 
    private List<ItemController> itemControllers = new List<ItemController>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        GameObject clone;
        ItemController tmp;
        foreach (GameObject obj in items)
        {
            Debug.Log("Instantiating: " + obj.name);
            clone = Instantiate(obj);
            tmp = clone.gameObject.GetComponent<ItemController>();
            // Debug.Log("Instantiated: " + tmp._incurrentSceneName);
            itemControllers.Add(tmp);
        }
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
        // Debug.Log("Items.length:" + items.Length);
        // Debug.Log("ItemControllers.length:" + itemControllers.Count);
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log(items[i].name + ": scene: " + scene.name + "/" + itemControllers[i]._incurrentSceneName);
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == itemControllers[i]._incurrentSceneName)
            {
                items[i].SetActive(true);
                Debug.Log(items[i].name + ": Activated");
            }
            else
            {
                items[i].SetActive(false);
                Debug.Log(items[i].name + ": Deactivated");
            }
        }
    }
}