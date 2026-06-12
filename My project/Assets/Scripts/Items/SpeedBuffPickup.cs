using UnityEngine;

public class SpeedBuffPickup : Pickup
{
    [SerializeField] private float speedBonus = 3f;
    [SerializeField] private float duration = 5f;

    protected override void ApplyEffect(GameObject player)
    {
        PlayerMovement movement =
            player.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.ApplySpeedBuff(
                speedBonus,
                duration);
        }
        Destroy(gameObject);
    }
}
