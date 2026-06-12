using UnityEngine;

public class HealPickup : Pickup
{
    [SerializeField] private float healAmount = 25f;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerHealth health =
            player.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}
