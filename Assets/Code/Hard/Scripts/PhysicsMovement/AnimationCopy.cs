using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCopy : MonoBehaviour
{
    public Transform targetRoot;
    public bool reverse;
    ConfigurableJoint joint;
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reverse)
        {
            joint.targetRotation = Quaternion.Inverse(targetRoot.rotation);
        }
        else
        {
            joint.targetRotation = targetRoot.rotation;
        }
        
    }
}
