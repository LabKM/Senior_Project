using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    [SerializeField]
    float secForOpenning = 1.0f;
    Vector3 Origin;
    bool Opening;
    bool moving;

    float original_scale;

    private void Start()
    {
        Opening = false;
        moving = false;
        Origin = transform.position;
        original_scale = transform.localScale.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Opening && !moving)
        {
            transform.DOScaleY(0.0f, secForOpenning);
            Invoke("CloseDoor", 2.5f);
            Opening = true;
            moving = true;
        }
    }

    void CloseDoor()
    {
        transform.DOScaleY(original_scale, secForOpenning);
        Invoke("MoveEnd", secForOpenning);
    }

    void MoveEnd()
    {
        Opening = false;
        moving = false;
    }
}
