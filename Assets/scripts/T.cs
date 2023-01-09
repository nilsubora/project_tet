using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class T : MonoBehaviour
{

    private float previousTime;
    public float fallTime = 0.8f;
    public static int heigth = 20;
    public static int width = 10;

    public Vector3 rotationpoint;

    void Start()
    {

    }
    private static Transform[,] grid = new Transform[width, heigth];
    void Addtogrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool Validmove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if ( roundedX < 0 || roundedX >= width || roundedY < 0 )
            {
                return false;
            }
            if (grid[roundedX,roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
    void Stoptheprevious()
    {
        this.enabled = false;
    }

   
    void Update()
    {
        
        if (Time.time - previousTime > ((Input.GetKeyDown(KeyCode.DownArrow))?fallTime/10:fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!Validmove())
            {
                transform.position -= new Vector3(0, -1, 0);
                Addtogrid();
            //    Debug.Log(grid);
                Invoke("Stoptheprevious", 3.0f);
                this.enabled = false;
                FindObjectOfType<spawnpoint>().newblock();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            transform.position += new Vector3(1, 0, 0);
            if (!Validmove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Validmove())
            {
                transform.position += new Vector3(1, 0, 0);

            }      
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), 90);
            if (!Validmove())
            {
                transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), -90);
            }
        }
    }
}

