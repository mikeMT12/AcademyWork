using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    public Vector3 rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(rotationSpeed);
    }
}
