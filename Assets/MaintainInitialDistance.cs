using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MaintainInitialDistance : MonoBehaviour
{
    public Transform target;
    private Vector3 initialOffset;

    void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset between the two objects
            initialOffset = transform.position - target.position;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Keep the initial distance between the objects
            transform.LookAt(target.position);
        }
    }
}