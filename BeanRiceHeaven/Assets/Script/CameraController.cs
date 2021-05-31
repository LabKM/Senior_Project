using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float CameraDistanceFromTarget = 1.0f;
    [SerializeField]
    Vector2 CameraDistanceMinMax = new Vector2(0.2f, 3.0f);
    public float Distance{ 
        get{ return CameraDistanceFromTarget; } 
        set{ CameraDistanceFromTarget = Mathf.Clamp(value, CameraDistanceMinMax.x, CameraDistanceMinMax.y );
            childCamera.transform.position = transform.position + transform.forward * CameraDistanceFromTarget * -1;}
        } 
    [SerializeField]
    Vector2 Sensitivity;
    public Vector3 Right { get { return Vector3.Cross(Vector3.up, transform.forward); } }
    public Vector3 Forward { get { return Vector3.Cross(transform.right, Vector3.up); } }

    Camera childCamera;

    private void Start()
    {
        childCamera = transform.GetComponentInChildren<Camera>();
        if(childCamera == null)
        {
            Debug.LogError("Need Child Camera Object");
        }
        childCamera.transform.position = transform.position + transform.forward * Distance * -1;
    }
}
