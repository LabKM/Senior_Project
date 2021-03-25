using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField]
    Transform targetPoint;
    Vector3 Origin;
    bool Opening;
    bool moving;

    private void Start()
    {
        Opening = false;
        moving = false;
        Origin = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Opening && !moving)
        {
            transform.DOMove(targetPoint.position, 0.5f);
            Invoke("CloseDoor", 2.5f);
            Opening = true;
            moving = true;
        }
    }

    void CloseDoor()
    {
        transform.DOMove(Origin, 0.5f);
        Invoke("MoveEnd", 0.6f);
    }

    void MoveEnd()
    {
        Opening = false;
        moving = false;
    }
}
