
using UnityEngine;

[System.Serializable]
public class PowerUp 
{


    
    public PowerUpType powerUpType; 
    public float powerUpTimer;
    public GameObject powerUpPrefab;
    
    
}
public enum PowerUpType
{
    PlayerShield,
    PlayerSnowSpeedBoost,
    FreezeEnemy,
    SlowdownEnemy
}