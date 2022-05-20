using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    public static Cylinder Instance;

    private Transform _parentCylinder;
    private Vector3 _cylinderDirection = Vector3.forward;

    private float _speed = .5f;
    

    public void Move()
    {
        transform.Translate(_cylinderDirection * Time.deltaTime * _speed, Space.World);
    }


    public void DropCylinder()
    {
        // when cylinder not in use anymore, activate it's gravity

    }
}
