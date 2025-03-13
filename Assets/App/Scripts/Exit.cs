using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private RSO_Resource rsoResource;

    private bool _canExit;

    private void Awake()
    {
        rsoResource.Value = 0;
    }

    private void OnEnable() => rsoResource.OnChanged += CheckExitAvailable;
    private void OnDisable() => rsoResource.OnChanged -= CheckExitAvailable;

    private void CheckExitAvailable(int value)
    {
        if (value <= 0)
        {
            _canExit = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _canExit)
        {
            Debug.Log("Exit");
        }
    }
}