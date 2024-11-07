using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUG_t_button : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void OnMouseDown()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
