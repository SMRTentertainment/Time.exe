using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth =
            other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
            return;

        ApplyEffect(other.gameObject);

        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(GameObject player);
}
