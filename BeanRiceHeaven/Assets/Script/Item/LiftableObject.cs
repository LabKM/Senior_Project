using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : MonoBehaviour, ILiftable
{
    public Material Outline;
    public Transform liftedPoint;
    Rigidbody rigidbody;
    MeshRenderer mesh;
    BeanController lifter;
    bool onHand;

    public void LeftShift(Transform _transform)
    {
        Vector3 offset = transform.position - liftedPoint.position;
        transform.position = _transform.position + offset;
        onHand = !onHand;
        if (onHand)
        {   
            rigidbody.Sleep();
            rigidbody.isKinematic = true;
            transform.parent = _transform;
        }
        else
        {   
            rigidbody.WakeUp();
            rigidbody.isKinematic = false;
            transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!onHand && other.tag == "Player")
        {
            lifter = other.GetComponent<BeanController>();
            lifter.liftObject = this;
            List<Material> materials = new List<Material>();
            mesh.GetMaterials(materials);
            materials.Add(Outline);
            mesh.materials = materials.ToArray();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!onHand && other.tag == "Player")
        {
            lifter.liftObject = null;
            lifter = null;
            List<Material> materials = new List<Material>();
            mesh.GetMaterials(materials);
            materials.RemoveAt(materials.Count-1);
            mesh.materials = materials.ToArray();
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();
        lifter = null;
        onHand = false;
    }
}
