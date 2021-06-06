using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingIcon : MonoBehaviour
{
    Vector3 center;
    Vector2 rate;
    public GameObject Target;
    RectTransform rectTransform;
    
    void Start(){
        rectTransform = GetComponent<RectTransform>();
        center.Set(3.73f, 0, -10.943f);
        rate.Set(24.0f, 14.0f);
        if(Target == null){
            gameObject.SetActive(false);
        }
    }



    void Update(){
        if(Target != null){
            Vector3 targetPos = Target.transform.position - center;
            targetPos.x = -targetPos.x * 960 / rate.x;
            targetPos.y = -targetPos.z * 540.0f / rate.y;
            targetPos.z = 0;
            rectTransform.localPosition = targetPos;            
        }
    }
}
