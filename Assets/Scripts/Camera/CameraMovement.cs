using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player; 
    [SerializeField] private Vector3 distanceFromObject = new Vector3(0, 11, -7); 
    private void LateUpdate() 
    {
        Vector3 positionToGo = player.transform.position + distanceFromObject; 
        Vector3 smoothPosition = Vector3.Lerp( transform.position,  positionToGo, t: Time.deltaTime * 4f);
        transform.position = smoothPosition;
        transform.LookAt(player.transform.position); 
    }
}
