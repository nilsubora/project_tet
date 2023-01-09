using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour
{

    public static Transform[,] gridboundary = new Transform[20,20];
    // Start is called before the first frame update
    void Start()
    {
        

    }
    public void creategrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            gridboundary[roundedX, roundedY] = children;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
