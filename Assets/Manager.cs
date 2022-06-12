using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool one;
    public bool two;
    public bool three;

    public GameObject oneObj;
    public GameObject twoObj;
    public GameObject threeObj;

    // Start is called before the first frame update
    void Start()
    {
        if (one) 
        {
            oneObj.SetActive(true);
        }
        if (two)
        {
            twoObj.SetActive(true);
        }
        if (three)
        {
            threeObj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
