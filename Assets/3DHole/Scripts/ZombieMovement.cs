using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;
    private bool isEnabled = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isEnabled || waypoints.Length == 0)
            return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 movementDirection = (targetPosition - transform.position).normalized;
        Vector3 movement = movementDirection * movementSpeed;

        rb.velocity = movement;

        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        isEnabled = false;
        rb.velocity = Vector3.zero;
    }

}
