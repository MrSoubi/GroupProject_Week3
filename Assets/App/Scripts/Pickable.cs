using System;
using UnityEngine;
public class Pickable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RSO_Resource rsoResource;

    private void Awake()
    {
        rsoResource.Value += 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rsoResource.Value += 1;
            Destroy(gameObject);
        }
    }
}