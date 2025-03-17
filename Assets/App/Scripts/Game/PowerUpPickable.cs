using UnityEngine;
public class PowerUpPickable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float powerUpDelay;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    [Header("Output")]
    [SerializeField] RSE_PowerUpPickable rsePowerUpPicked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rsePowerUpPicked.Call(powerUpDelay);
            gameObject.SetActive(false);
        }
    }
}