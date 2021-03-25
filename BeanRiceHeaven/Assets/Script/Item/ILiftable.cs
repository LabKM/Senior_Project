using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiftable
{
    void GetUp(Transform transform);
    void GetDown(Transform transform);
}
