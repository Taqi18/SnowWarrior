using System;
using UnityEngine;

public class ApplyPowerUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<PowerUpInstance>(out var powerUpInstance))
        {
            Debug.Log(powerUpInstance.GetPowerUpType());
            Destroy(powerUpInstance.gameObject);
        }
    }
}
