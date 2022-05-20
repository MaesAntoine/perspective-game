using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentChecker : MonoBehaviour
{
    public static AlignmentChecker Instance;

    private GameObject _player;

    private float _precision = 0.15f;

    private List<Cylinder> _cylinders;

    

    private void Awake()
    {
        Instance = this;

        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        _cylinders = CylinderManager.Instance.cylinderList;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_cylinders.Count > 1)
        {
            if (IsAligned(_cylinders[0], _cylinders[1]))
            {
                ColorChildrens.Instance.ColorObject(_player.transform, ColorChildrens.Instance.newColor);
            }
            else
            {
                ColorChildrens.Instance.ColorObject(_player.transform, ColorChildrens.Instance.initialColor);
            }
        }
    }


    public bool IsAligned(Cylinder cyl_0,Cylinder cyl_1)
    {
        // get the beginning of the first cylinder and the end of the second
        Vector3 point_0 = new Vector3(cyl_0.transform.position.x, cyl_0.transform.position.y, cyl_0.transform.position.z - CylinderManager.Instance.lengthList[0]);
        Vector3 point_1 = new Vector3(cyl_1.transform.position.x, cyl_1.transform.position.y, cyl_1.transform.position.z + CylinderManager.Instance.lengthList[1]);

        // get the two vectors that will evaluate the alignment
        Vector3 _camDirection = CameraMovement.Instance.cam.transform.forward;
        Vector3 _betweenPoint = point_0 - point_1;

        return Vector3.Magnitude(Vector3.Cross(_camDirection, _betweenPoint)) < _precision;

    }
}
