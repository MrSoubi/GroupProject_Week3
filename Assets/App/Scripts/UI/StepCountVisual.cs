using TMPro;
using UnityEngine;
public class StepCountVisual : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] TMP_Text stepCountTxt;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_UpdateStepCountVisual rseUpdateStepCountVisual;
    
    //[Header("Output")]

    private void OnEnable()
    {
        rseUpdateStepCountVisual.Action += UpdateStapCountVisual;
    }
    private void OnDisable()
    {
        rseUpdateStepCountVisual.Action -= UpdateStapCountVisual;
    }

    void UpdateStapCountVisual(int current, int max)
    {
        stepCountTxt.text = $"Step : {current}/{max}";
    }
}