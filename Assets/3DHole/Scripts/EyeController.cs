using UnityEngine;

public class EyeController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    private Vector3 clickedScreenPosition;

    // Update is called once per frame
    void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // First calculate the difference in screen position
            Vector3 difference = Input.mousePosition - clickedScreenPosition;

            Vector3 direction = difference.normalized;

            float maxScreenDistance = Screen.height;

            if (difference.magnitude > maxScreenDistance)
            {
                clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }

            difference /= Screen.width;

            difference.z = difference.y;
            difference.y = 0;

            Vector3 TargetPosition = transform.position + difference * moveSpeed * Time.deltaTime;

            transform.position = TargetPosition;
        }
    }
}