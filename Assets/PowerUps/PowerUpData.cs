using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUps/PowerUpData")]
public class PowerUpData : ScriptableObject
{
   public  List<PowerUp> PowerUps = new List<PowerUp>();
   
   
}