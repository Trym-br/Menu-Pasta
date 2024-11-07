using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private AnimationCurve shrinkCurve;
    public string _incurrentSceneName;
    
    public string _state = "Unobtained";
    // Unobtained: in original location
    // Obtained: in hub
    // Used: in oven
    [SerializeField] private bool draggable;

    private void Awake() { DontDestroyOnLoad(this.gameObject); }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDrag()
    {
        // _state == Obtained, unobtained is just for debug
        if (draggable && _state == "Unobtained")
        {
            this.transform.position = GetMouseWorldPosition();
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
            // _state = "Obtained";
            _incurrentSceneName = "Main";
        }
    }
    IEnumerator PickupAnimCallback()
    {
        // Wait until the animation finishes
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 0.7f);
    }

    IEnumerator PickupAnimation()
    {
        // Debug.Log("RUNNING PICKUP ANIM");
        // _spriteRenderer.size = new Vector2(_spriteRenderer.size.x + 0.5f, _spriteRenderer.size.y + 0.5f);
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(1.0f, 1.0f, 1);
        // _spriteRenderer.size = new Vector2(_spriteRenderer.size.x - 0.5f, _spriteRenderer.size.y - 0.5f);
    }
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

        // Ensure the scale is set to zero at the end
        transform.localScale = Vector3.zero;
    }
    private IEnumerator ShrinkToZeroOLD()
    {
        // Store the original scale
        Vector3 originalScale = transform.localScale;

        // Track the elapsed time
        float elapsedTime = 0f;

        // Loop over the specified shrink duration
        while (elapsedTime < shrinkDuration)
        {
            // Calculate the proportion of the time passed
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            // Lerp between the original scale and zero based on the time proportion
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);

            // Wait until the next frame
            yield return null;
        }

        // Ensure the scale is set to zero at the end
        transform.localScale = Vector3.zero;
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
}
