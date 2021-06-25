using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door2 : MonoBehaviour
{   
    Transform RightDoor;
    Transform LeftDoor;

    Vector3 RightOrigin;
    Vector3 LeftOrigin;
    float secForOpenning = 1.0f;
    bool Opening;
    bool moving;

    public bool locked { get; set;}

    void Awake(){
        RightDoor = transform.Find("Box060");
        RightOrigin = RightDoor.position;
        LeftDoor = transform.Find("Box061");
        LeftOrigin = LeftDoor.position;

        Opening = false;
        moving = false;
    }
    
    void Start(){
        RightDoor.name = "right door";
        LeftDoor.name = "left door";
    }

    void OnTriggerEnter(Collider collider){
        if (!Opening && !moving && !locked)
        {
            RightDoor.DOScaleX(0.0f, secForOpenning);
            LeftDoor.DOScaleX(0.0f, secForOpenning);
            Invoke("CloseDoor", 2.5f);
            Opening = true;
            moving = true;
        }
    }

    void CloseDoor()
    {
        RightDoor.DOScaleX(1.0f, secForOpenning);
        LeftDoor.DOScaleX(1.0f, secForOpenning);
        Invoke("MoveEnd", secForOpenning + 0.1f);
    }
    void MoveEnd()
    {
        Opening = false;
        moving = false;
    }
}
