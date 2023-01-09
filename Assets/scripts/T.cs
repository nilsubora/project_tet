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

    // private string touch;
    //  private string grounding;
    /*
      public void OnTriggerEnter2D(Collider2D other)
      {
          if (other.tag == "leftwall")
          {
              touch = "left";
          }
          else if (other.tag == "rightwall")
          {
              touch = "right";
          }
          else if (other.tag == "bottom")
          {
              grounding = "bottom";
              Invoke("changebodytype", 2.5f); 
              //changebodytype();
              print(GetComponent<Rigidbody2D>().bodyType);
              FindObjectOfType<spawnpoint>().newblock();
          }
      }

      */
    /*
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "leftwall" || other.tag == "rigthwall")
        {
            touch = "";
        }
    }
    
    */

    void Start()
    {

    }
    private static Transform[,] grid = new Transform[width, heigth];
    //void addtogrid()
   // {
     //   foreach (Transform children)
    //}
    bool validmove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if ( roundedX < 0 || roundedX > width || roundedY < 0 )
            {
                return false;
            }
        }
        return true;
    }
    void öncekinidurdurma()
    {
        this.enabled = false;
    }

   
    void Update()
    {
        
        if (Time.time - previousTime > ((Input.GetKeyDown(KeyCode.DownArrow))?fallTime/10:fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!validmove())
            {
                transform.position -= new Vector3(0, -1, 0);
                // yield return new WaitForSeconds(1);
                Invoke("öncekinidurdurma", 3.0f);
                this.enabled = false;
                FindObjectOfType<spawnpoint>().newblock();
            }
            
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // if (touch != "right" || validmove() == true)
            transform.position += new Vector3(1, 0, 0);
            if (!validmove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // if (touch != "left" || validmove() == true)
            transform.position += new Vector3(-1, 0, 0);
            if (!validmove())
            {
                transform.position += new Vector3(1, 0, 0);
               // print(validmove());
               // print(touch);
            }      
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), 90);
            if (!validmove())
            {
                transform.RotateAround(transform.TransformPoint(rotationpoint), new Vector3(0, 0, 1), -90);
            }
        }
    }
}

