
using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    [SerializeField] private Transform playerTransform;
    private void Start()
    {
       StartCoroutine( SpawnPowerUpRandomly());
    }

    private IEnumerator SpawnPowerUpRandomly()
    {

        var randomTimmer = Random.Range(0, 15);
        yield return new WaitForSeconds(randomTimmer);

        var powerUpIndex = UnityEngine.Random.Range(0, powerUpData.PowerUps.Count);
        var powerUp = powerUpData.PowerUps[powerUpIndex];
        var powerUpPrefab = powerUp.powerUpPrefab;
        
        var powerUpPosition = new Vector3(playerTransform.position.x+Random.Range(-4,4), transform.position.y, playerTransform.position.z+Random.Range(-4,4));
        var powerUpGameObject = GameObject.Instantiate(powerUpPrefab,powerUpPosition,Quaternion.identity);
        var powerUpInstance = powerUpGameObject.GetComponent<PowerUpInstance>();
        powerUpInstance.InitPowerUp(powerUp.powerUpType);
        
        StartCoroutine(SpawnPowerUpRandomly());

    }
}
