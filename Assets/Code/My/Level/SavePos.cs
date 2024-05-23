using UnityEngine;

public class SavePos : MonoBehaviour
{
	public Transform checkPoint;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "PlayerObject")
		{
			col.gameObject.GetComponent<PhysicsMovement>().checkPoint = checkPoint.position;
		}
	}
}
