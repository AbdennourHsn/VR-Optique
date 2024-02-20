using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{

    public Transform pos1;
    public Transform pos2;

    private void Start()
    {
        print(CalculateDistanceOnXAxis(pos1.transform.position, pos2.transform.position));
    }

    float CalculateDistanceOnXAxis(Vector3 startPosition, Vector3 endPosition)
    {
        // Calculate the distance between the positions along the X-axis
        float distanceOnX = Vector3.Distance(startPosition, endPosition) *100;
        return distanceOnX;
    }
}
