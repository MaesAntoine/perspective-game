using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderManager : MonoBehaviour
{
    public static CylinderManager Instance;

    [SerializeField] Cylinder _cylinderPrefab;
    [SerializeField] int cylinderOnScreen = 2;

    protected Transform _parentCylinder;

    public List<Cylinder> cylinderList;
    public List<float> lengthList;
    public List<Color> colorList;

    public List<Cylinder> cylindersToDrop;
    private float _minRange = 1.0f;
    private float _maxRange = 3.0f;
    private float _timeDuration = .5f;
    private float _elapsedTime;


    private void Awake()
    {
        // everything that needs to be ready at launch
        Instance = this;
        _parentCylinder = GameObject.Find("Managers/Cylinder Manager").transform;
        
        // declare empty lists at start
        cylinderList = new List<Cylinder>();
        lengthList = new List<float>();
        colorList = new List<Color>();

        // pop the cylinder
        for (int i=0; i < cylinderOnScreen; i++)
        {
            SpawnManyCylinders(true);
        }
    }

    private void Update()
    {
        if(GameManager.Instance.isGameOn)
        {
            for (int i = 0; i < cylinderList.Count; i++)
            {
                cylinderList[i].Move();
            }

            if((cylinderList[0].transform.position.z > lengthList[0]) && (AlignmentChecker.Instance.IsAligned(cylinderList[0], cylinderList[1])))
            {
                ClearStuff();
                SpawnManyCylinders(true);
                UpdatePosition();
                
            }

/*            else if ((cylinderList[0].transform.position.z > lengthList[0]) && (!AlignmentChecker.Instance.IsAligned(cylinderList[0], cylinderList[1])))
            {
                // if cyl_0 passed the origin and the view wasn't alligned, game over.
                GameManager.Instance.isGameOn = false;
            }*/
        }
    }


    public void SpawnManyCylinders(bool colorOn=false) // spawn cylinders when called
    {
        if(cylinderList.Count < 1) // happens when launching the game
        {
            float scaleZ = 4.0f;

            Cylinder temporaryCylinder = Instantiate(_cylinderPrefab, new Vector3(0, 0, 0), new Quaternion(0, 1, 1, 0), _parentCylinder.transform);
            temporaryCylinder.transform.localScale = new Vector3(1, scaleZ, 1);

            cylinderList.Add(temporaryCylinder);
            lengthList.Add(scaleZ);
        }
        else // happens every other time
        {
            float scaleZ = UnityEngine.Random.Range(_minRange, _maxRange);
            float positionX = UnityEngine.Random.Range(-_maxRange, _maxRange);
            float positionY = UnityEngine.Random.Range(-_maxRange, _maxRange);
            float positionZ = UnityEngine.Random.Range(-_maxRange, _maxRange);

            Cylinder temporaryCylinder = Instantiate(_cylinderPrefab, new Vector3(positionX, positionY, -10), new Quaternion(0, 1, 1, 0), _parentCylinder.transform);
            temporaryCylinder.transform.localScale = new Vector3(1, scaleZ, 1);

            cylinderList.Add(temporaryCylinder);
            lengthList.Add(scaleZ);
        }


        if (colorOn) // Temporary option, more likely to be deleted at some point
        {
            CreateColors();
            ChangeColors();
        }
    }


    public void CreateColors()
    {
        float rand1 = UnityEngine.Random.Range(0.0f, 1.0f);
        float rand2 = UnityEngine.Random.Range(0.0f, 1.0f);
        float rand3 = UnityEngine.Random.Range(0.0f, 1.0f);

        colorList.Add(new Color(rand1, rand2, rand3));
    }

    public void ChangeColors()
    {
        for(int i = 0; i <cylinderList.Count; i++)
        {
            cylinderList[i].GetComponent<Renderer>().material.color = colorList[i];
        }
    }

    private void ClearStuff()
    {
        Destroy(cylinderList[0].gameObject);
        cylinderList.RemoveAt(0);
        lengthList.RemoveAt(0);

        

        // if colorOn in SpawnManyCylinders
        colorList.RemoveAt(0);
    }

    private void UpdatePosition()
    {
        Cylinder cylinderToMove = cylinderList[1];
        float distance = lengthList[0];
        Vector3 startPosition = cylinderToMove.transform.position;
        Vector3 endPosition = new Vector3(0, 0, - distance);

        _elapsedTime += Time.deltaTime;
        float percentageComplete = _elapsedTime / _timeDuration;


        cylinderList[0].transform.position = endPosition;
        // cylinderList[0].transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

    }

    


}
