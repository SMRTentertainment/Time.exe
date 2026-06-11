using UnityEngine;
using UnityEngine.UI;

public class TimeStopBarUI : MonoBehaviour
{
    [SerializeField] private PlayerTimeStop playerTimeStop;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = playerTimeStop.MaxCharge;

        slider.value = playerTimeStop.CurrentCharge;
    }

    private void Update()
    {
        slider.value = playerTimeStop.CurrentCharge;
    }
}
