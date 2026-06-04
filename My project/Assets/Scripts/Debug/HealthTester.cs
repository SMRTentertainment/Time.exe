using UnityEngine;
using UnityEngine.InputSystem;

public class HealthTester : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float healAmount = 10f;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            playerHealth.TakeDamage(damageAmount);

            Debug.Log(
                $"Daño: {damageAmount} | Vida actual: {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}"
            );
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame ||
            Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            playerHealth.Heal(healAmount);

            Debug.Log(
                $"Curación: {healAmount} | Vida actual: {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}"
            );
        }
    }
}
