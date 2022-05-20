using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public static LerpMovement Instance;

    private Vector3 endPosition;
    private Vector3 startPosition;

    private float desiredDuration = .5f;
    private float elapsedTime;

    private Cylinder cylinderToMove;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cylinderToMove = CylinderManager.Instance.cylinderList[0];
        startPosition = cylinderToMove.transform.position;
        endPosition = new Vector3(0, 0, - CylinderManager.Instance.lengthList[0]);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        CylinderManager.Instance.cylinderList[0].transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
    }
}
