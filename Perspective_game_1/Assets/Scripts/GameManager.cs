using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOn;


    private void Awake()
    {
        Instance = this;

        isGameOn = true;
    }

    private void Start()
    {
        
    }

}
