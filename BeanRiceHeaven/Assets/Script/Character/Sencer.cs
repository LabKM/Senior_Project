using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencer : MonoBehaviour
{
    public BeanController bean;
    public string SencorName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name != bean.transform.name )
            bean.OnSencor(SencorName);
    }
}
