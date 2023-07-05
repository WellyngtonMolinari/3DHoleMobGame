using UnityEngine;

public class EyeController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float sensitivity = 1f;
    private Vector3 originalPosition;
    private Vector3 initialMousePosition;
    private bool isDragging;
    private Transform faceTransform;

    private void Start()
    {
        originalPosition = transform.localPosition;
        faceTransform = transform.parent;
    }

    private void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            initialMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = (Input.mousePosition - initialMousePosition) / Screen.width;
            mouseDelta.z = mouseDelta.y;
            mouseDelta.y = 0f;

            Vector3 targetPosition = originalPosition + mouseDelta * moveSpeed * sensitivity;

            transform.localPosition = targetPosition;
        }
        else
        {
            Vector3 targetPosition = originalPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
