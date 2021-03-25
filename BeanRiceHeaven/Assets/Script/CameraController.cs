using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float Distance;
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

    private void Update()
    {
        
    }

    public void ResetDistance(float _distance)
    {
        Distance = _distance;
        childCamera.transform.position = transform.position + transform.forward * Distance * -1;
    }
}
