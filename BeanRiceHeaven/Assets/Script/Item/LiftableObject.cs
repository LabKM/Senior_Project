using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftableObject : MonoBehaviour, ILiftable
{
    public Material Outline;
    public Transform liftedPoint;
    MeshRenderer mesh;
    GameObject lifter;

    public void GetUp(Transform _transform)
    {
        Vector3 offset = transform.position - liftedPoint.position;
        transform.position = _transform.position + offset;
    }
    public void GetDown(Transform _transform)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lifter = other.gameObject;
            List<Material> materials = new List<Material>();
            mesh.GetMaterials(materials);
            materials.Add(Outline);
            mesh.materials = materials.ToArray();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == lifter)
        {
            lifter = null;
            List<Material> materials = new List<Material>();
            mesh.GetMaterials(materials);
            materials.RemoveAt(materials.Count-1);
            mesh.materials = materials.ToArray();
        }
    }

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        lifter = null;
    }
}
