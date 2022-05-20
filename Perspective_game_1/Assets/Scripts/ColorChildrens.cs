using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChildrens : MonoBehaviour
{
    public static ColorChildrens Instance;
    public Transform myParent;

    public Color initialColor;
    public Color newColor;

    private GameObject _player;

    private void Awake()
    {
        Instance = this;
        
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        initialColor = _player.GetComponent<Renderer>().material.color;
    }

    public List<Transform> GetAllChildren()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in myParent)
        {
            childs.Add(child);
        }
        return childs;
    }

    

    public void ColorObject(Transform element, Color newColor)
    {
        element.GetComponent<Renderer>().material.color = newColor;
    }

}
