using UnityEngine;

public class SnowBallFactory
{
    private readonly SnowBall _snowBallPrefab;

    public SnowBallFactory()
    {
        _snowBallPrefab = Resources.Load<SnowBall>("SnowBall");
    }

    public SnowBall Create(Transform handTransform, bool isPlayerBall = true)
    {
        if (_snowBallPrefab == null) return null;
        
        var snowBall = Object.Instantiate(_snowBallPrefab);
        snowBall.SetSnowBallInHand(handTransform,isPlayerBall);

        return snowBall;
    }
}