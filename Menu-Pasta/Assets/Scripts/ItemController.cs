using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ItemController : MonoBehaviour
{
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    [SerializeField] private AnimationCurve shrinkCurve;
    
    [SerializeField] private BoxCollider2D _boxCollider2D;
    
    public string _incurrentSceneName;
    
    public string _state = "Unobtained";
    // Unobtained: in original location
    // Obtained: in hub
    // Used: in oven
    [SerializeField] private bool draggable;

    private void Awake() { DontDestroyOnLoad(this.gameObject); HideOrShow(false); }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDrag()
    {
        // _state == Obtained, unobtained is just for debug
        if (draggable && _state == "Obtained")
        {
            // preserve z of editor
            Vector3 mousePosition = GetMouseWorldPosition();
            this.transform.position = new Vector3(mousePosition.x, mousePosition.y, this.transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        if (_state == "Unobtained")
        {
            Debug.Log("Clicked on " + this.name);
            // StartCoroutine(PickupAnimation());
            StartCoroutine(ShrinkToZero());
            // _animator.SetTrigger("Oven");
            // StartCoroutine(PickupAnimCallback());
            Debug.Log(this.name + "moved to scene Trym 2");
            _incurrentSceneName = "Main";
            ItemManager.Instance.UpdateBackground(this.tag);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + "entered " + other.name);
        Debug.Log("tags of collision: " + other.gameObject.tag + " / " + this.tag);
        if (other.gameObject.CompareTag("Oven") && this.tag != "Oven" && _state == "Obtained")
        {
            Debug.Log(this.name + ": put in oven");
            _animator.SetBool("PutInPot", true);
            _state = "Used";
            ItemManager.Instance.OvenStateUpdate();
        } 
    }

    //      Helper functions & animation        //
    
    public float shrinkDuration = 1.0f;
    private IEnumerator ShrinkToZero()
    {
        // Store the original scale
        Vector3 originalScale = transform.localScale;

        // Track the elapsed time
        float elapsedTime = 0f;

        // Loop until the specified duration is reached
        while (elapsedTime < shrinkDuration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            // Use the curve to get the scale factor (from 1 to 0 based on curve)
            float scaleFactor = shrinkCurve.Evaluate(t);

            // Apply the scale factor to the original scale
            transform.localScale = originalScale * scaleFactor;

            // Wait for the next frame
            if (transform.localScale.x < 0.1f)
            { 
                transform.localScale = Vector3.zero;
                break;
            }
            yield return null;
        }
        HideOrShow(false);
        _state = "Obtained";
        transform.localScale = new Vector3(1,1,1);
    }
    
    // Helper function to get the mouse position in world space
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        // Convert screen position to world position
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePoint);
        pos.z = 0f;
        return pos;
    }

    public void HideOrShow(bool show)
    {
        // this.gameObject.SetActive(show);
        _spriteRenderer.enabled = show;
        _boxCollider2D.enabled = show;
    }
}
