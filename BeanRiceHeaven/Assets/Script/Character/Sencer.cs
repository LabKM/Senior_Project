using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sencer : MonoBehaviour
{
    public BeanController bean;
    public string SencerName;

    private void OnTriggerEnter(Collider other)
    {
        bean.OnSencer(SencerName);
    }
}
