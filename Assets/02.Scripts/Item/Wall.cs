using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the player
            SpawnManager.Instance.PlayerRespawn();

        }
    }
}
