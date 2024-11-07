using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OvenController : MonoBehaviour
{
    private Animator _animator;
    public string _state = "Unobtained";
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (_state == "Unobtained")
        {
            Debug.Log("Clicked on Oven");
            // _animator.SetTrigger("Oven");
            // StartCoroutine(OvenAnimCallBack());
            SceneManager.LoadScene("Trym");
            // _state = "Obtained";
        }
    }
    IEnumerator OvenAnimCallBack()
    {
        // Wait until the animation finishes
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 0.7f);
    }
}
