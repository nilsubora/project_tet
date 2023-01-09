using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class T : MonoBehaviour
{

    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;

    public Vector3 rotationpoint;

    void Start()
    {

    }
    private static Transform[,] grid = new Transform[width+1, height+1];
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

    void Checklines()
    {
        for(int i = 0; i <= height - 1; i++)
        //for(int i = height-1; i >= 0; i--)
        {
            if (LineExist(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
        
        
    }

    bool LineExist(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y=i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }



    void Update()
    {
        Checklines();
        if (Time.time - previousTime > ((Input.GetKeyDown(KeyCode.DownArrow))?fallTime/10:fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!Validmove())
            {
                transform.position -= new Vector3(0, -1, 0);
                Addtogrid();
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

