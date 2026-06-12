using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        healthSlider.minValue = 0f;
        healthSlider.maxValue = playerHealth.MaxHealth;

        healthSlider.value = playerHealth.CurrentHealth;
    }

    private void Update()
    {
        healthSlider.value = playerHealth.CurrentHealth;
    }
}
