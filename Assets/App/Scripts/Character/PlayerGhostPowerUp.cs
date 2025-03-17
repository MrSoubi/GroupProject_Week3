using System.Collections;
using UnityEngine;

public class PlayerGhostPowerUp : MonoBehaviour
{
    [Header("Settings")]
    public bool isActive = false;
    public bool IsActive() { return isActive; }

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OnGhostPowerPicked rseOnPowerUpicked;

    //[Header("Output")]

    private void OnEnable()
    {
        rseOnPowerUpicked.Action += OnPowerUpPicked;
    }
    private void OnDisable()
    {
        rseOnPowerUpicked.Action -= OnPowerUpPicked;
    }

    void OnPowerUpPicked(float delay)
    {
        if (activationDelay != null) StopCoroutine(activationDelay);

        activationDelay = StartCoroutine(ActivationDelay(delay));
    }

    Coroutine activationDelay;
    IEnumerator ActivationDelay(float delay)
    {
        isActive = true;
        yield return new WaitForSeconds(delay);
        isActive = false;
    }
}