 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class SubScript : MonoBehaviour
{

    public bool inSub = false;
    [Range(0, 20)]public float speed;
    [Range(0, 20)]public float heightSpeed;
    [Range(10, 100)]public float rotSpeed;
    private GameObject sub;
    private Rigidbody subRB;
    private GameObject subModel;
    private GameObject CameraPos;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        rotSpeed = 10f;
        heightSpeed = 3.5f;
        // get the manager instance so that for example the sub enters a station it can tell the game manager
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Debug.Log(manager);
        sub = this.gameObject;
        subModel = sub.transform.Find("model").gameObject;
        subRB = sub.GetComponent<Rigidbody>(); 
        CameraPos = sub.transform.Find("CameraPos").gameObject;
    }


    private bool isInSub(){
        return manager.inSub;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "SubmarineStation")
        {
            manager.enterStation();
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "SubmarineStation")
        {
            manager.exitStation();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (isInSub())
        {
            if (Input.GetKey(KeyCode.W))
            {
                sub.GetComponent<Rigidbody>().velocity = sub.transform.forward * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                sub.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            if (Input.GetKey(KeyCode.D))
            {
                //rotates from back, used -rotspeed so it pushes to left therefor making the front go to the right
                subRB.AddForceAtPosition(new Vector3(-rotSpeed,0f,0f),sub.transform.position - new Vector3(0f,0f, 2.19f), ForceMode.Force);
            }
            if (Input.GetKey(KeyCode.A))
            {
                subRB.AddForceAtPosition(new Vector3(rotSpeed, 0f, 0f), sub.transform.position - new Vector3(0f, 0f, 2.19f), ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.E))
            {
                sub.transform.position += new Vector3(0f, heightSpeed, 0f) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                sub.transform.position += new Vector3(0f, -heightSpeed, 0f) * Time.deltaTime;
            }
            // dont change
            if(inSub){
                if (Input.GetKeyDown(KeyCode.F))
                {
                    manager.actionSub();
                }
            }
            inSub = true;
        }
        else
        {
            inSub = false;
        }
    }
}
