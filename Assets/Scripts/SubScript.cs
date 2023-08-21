 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    public bool inSub = false;
    [Range(0, 20)]public float speed = 10f;
    [Range(0, 20)]public float heightSpeed = 10f;
    [Range(0, 2)]public float rotSpeed = 1f;
    private GameObject sub;
    private GameObject subModel;
    private GameObject CameraPos;
    private GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        // get the manager instance so that for example the sub enters a station it can tell the game manager
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Debug.Log(manager);
        sub = this.gameObject;
        subModel = sub.transform.Find("model").gameObject;
        CameraPos = sub.transform.Find("CameraPos").gameObject;
    }
    private bool isInSub(){
        if(Camera.main.transform.parent == sub.gameObject.transform){
            return true;
        }else{
            return false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        subCollision(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        subCollisionExit(collision);
    }

    public void subCollision(Collision other)
    {
        if (other.gameObject.tag == "SubmarineStation"){
            // get the player spawn pos
            manager.enterStation(other.transform.Find("PlayerSpawn").gameObject.transform.position);
        }
    }
    public void subCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "SubmarineStation")
        {
            manager.exitStation();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isInSub())
        {
            inSub = true;
            if (Input.GetKey(KeyCode.W))
            {
                sub.GetComponent<Rigidbody>().velocity = sub.transform.forward * speed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (sub.GetComponent<Rigidbody>().velocity.z >= 1f)
                {
                    sub.GetComponent<Rigidbody>().velocity -= sub.transform.forward * 1f;
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                sub.transform.Rotate(0f,rotSpeed,0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                sub.transform.Rotate(0f, -rotSpeed, 0f);

            }

            if (Input.GetKey(KeyCode.E))
            {
                sub.transform.position += new Vector3(0f, heightSpeed, 0f) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                sub.transform.position += new Vector3(0f, -heightSpeed, 0f) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.F)){
                manager.actionSub();
            }
        }
        else
        {
            inSub = false;
        }
    }
}
