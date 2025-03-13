using System;
using UnityEngine;
public class Pickable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RSO_Resource rsoResource;

    private void Start()
    {
        rsoResource.Value += 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rsoResource.Value -= 1;
            Destroy(gameObject);
        }
    }
}