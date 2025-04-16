using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 10f; // Force to apply when the player hits the jump pad

    [SerializeField]
    private Vector3 launchDirection = Vector3.up; // Direction to launch the player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(launchDirection.normalized * jumpForce, ForceMode.Impulse);
                Debug.Log("Player launched in direction: " + launchDirection);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 start = transform.position;
        Vector3 end = start + launchDirection.normalized * jumpForce * 0.1f; // Scale for visualization
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f); // Draw a sphere at the end of the arrow
    }
}
