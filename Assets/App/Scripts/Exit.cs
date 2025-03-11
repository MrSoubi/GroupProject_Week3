using UnityEngine;

public class Exit : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private RSO_Resource rsoResource;

    private bool _canExit;

    private void OnEnable() => rsoResource.OnChanged += CheckExitAvailable;
    private void OnDisable() => rsoResource.OnChanged -= CheckExitAvailable;

    private void CheckExitAvailable(int value)
    {
        if (value <= 0)
        {
            _canExit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _canExit)
        {
            Debug.Log("Exit");
        }
    }
}