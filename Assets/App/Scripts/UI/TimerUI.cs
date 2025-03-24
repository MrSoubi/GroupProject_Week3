using TMPro;
using UnityEngine;
public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI _text;
    public RSO_Timer timer;

    private void OnEnable()
    {
        timer.OnChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        timer.OnChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(float value)
    {
        _text.text = Mathf.FloorToInt(value).ToString();
    }
}