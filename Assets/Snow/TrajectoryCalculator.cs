
using UnityEngine;


public static class TrajectoryUtility
{
    public static Vector3 CalculateVelocity(Vector3 start, Vector3 end, float speed)
    {
        var displacement = end - start;
        var displacementXZ = new Vector3(displacement.x, 0, displacement.z);
        var horizontalDistance = displacementXZ.magnitude;
        var verticalDistance = displacement.y;
        var gravity = Mathf.Abs(Physics.gravity.y);

        var time = horizontalDistance / speed;
        var velocityY = (verticalDistance + 0.5f * gravity * time * time) / time;
        var velocityXZ = displacementXZ.normalized * speed;

        return new Vector3(velocityXZ.x, velocityY, velocityXZ.z);
    }
}
