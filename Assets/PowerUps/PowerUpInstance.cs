
using UnityEngine;

public class PowerUpInstance : MonoBehaviour
{
    private  PowerUpType _powerUpType;

   public void InitPowerUp(PowerUpType powerUpType)
   {
        _powerUpType=powerUpType;
   }

   public PowerUpType GetPowerUpType()
   {
       return _powerUpType;
   }
}
