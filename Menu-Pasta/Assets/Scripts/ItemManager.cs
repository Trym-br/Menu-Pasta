using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    public bool _menuOpen = true;
    public GameObject[] items;
    public Sprite[] backgrounds;
    public AnimationClip[] KjeleAnimations;
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
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene loaded: " + scene.name);
        // Debug.Log("Items.length:" + items.Length);
        // Debug.Log("ItemControllers.length:" + itemControllers.Count);
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log(items[i].name + ": scene: " + scene.name + "/" + itemControllers[i]._incurrentSceneName);
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == itemControllers[i]._incurrentSceneName)
            {
                // items[i].SetActive(true);
                itemControllers[i].HideOrShow(true);
                Debug.Log(items[i].name + ": Activated");
            }
            else
            {
                // items[i].SetActive(false);
                itemControllers[i].HideOrShow(false);
                Debug.Log(items[i].name + ": Deactivated");
            }
        }
    }

    public void OvenStateUpdate(string tag)
    {
        ItemController Kjele = itemControllers[3];
        // Ovn Pasta Tomat Kjele
        // if (itemControllers[1]._state == "Used" && itemControllers[2]._state != "Used")
        if (tag == "Tomat")
        {
            // Pasta ikkje Tomat
            itemControllers[2].HideOrShow(false);
            Kjele._animator.SetTrigger("Tomatfall");
            Debug.Log("Ovenstate: Tomat");
        }
        // else if (itemControllers[1]._state != "Used" && itemControllers[2]._state == "Used")
        else if (tag == "Pasta")
        {
            // Tomat ikkje Pasta
            itemControllers[1].HideOrShow(false);
            Kjele._animator.SetTrigger("Pastafall");
            Debug.Log("Ovenstate: Pasta");
        }
        // if (itemControllers[1]._state == "Used" && itemControllers[2]._state == "Used")
        // {
        //     // Pasta Og Tomat
        //     // Oven._spriteRenderer.sprite = 
        //     Debug.Log("Ovenstate: Both/Stir");
        // }
    }
    public void UpdateBackground(string itemtag)
    {
        GameObject background = GameObject.Find("Background");
        switch (itemtag)
        {
            case "Oven": 
                background.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
                itemControllers[3]._incurrentSceneName = "Main";
                break;
            case "Tomat": 
                background.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
                break;
            case "Pasta": 
                // background.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
                break;
        }
    }
}