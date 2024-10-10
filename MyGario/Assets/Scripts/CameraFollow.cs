using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float scaleSpeed;


    public Camera cam;

    
    void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        pos.z = transform.position.z;
        
        transform.position = pos;
        
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5 * target.localScale.x, scaleSpeed * Time.deltaTime);
    }
}
