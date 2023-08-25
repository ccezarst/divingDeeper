using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SubScriptNew : MonoBehaviour
{

    private String goKys = "If you decompiled this game then go fucking kys you fucking incel";

    public bool inSub = false;
    [Range(0, 20)] public float speed;
    [Range(0, 20)] public float heightSpeed;
    [Range(10, 100)] public float rotSpeed;
    [Range(0, 1000)] public float mouseSensitivity = 0f;
    public Int64 shootFramesDelay = 50;
    public float maxHeight = 25f;
    public GameObject healthUI;
    private GameObject sub;
    private Rigidbody subRB;
    private GameObject subModel;
    private GameObject CameraPos;
    private GameManager manager;
    private GameObject prop;
    private float xRotation;

    //public float health { get; private set; }
    //public float maxHealth { get; private set; } = 100f;
    public float health;
    public float maxHealth  = 100f;

    public void takeDamage(float dmg) {
        health -= dmg;
        Debug.Log(Mathf.InverseLerp(0, maxHealth, health));
        healthUI.GetComponent<Slider>().value = Mathf.InverseLerp(0, maxHealth, health);
        if(health <= 0){
            manager.plrDeath();
        }
    }

    public void restoreHealth() {
        health = maxHealth;
    }

    public void increaseMaxHealth(float nr){
        maxHealth += nr; 
    }

    public void decreaseMaxHealth(float nr)
    {
        maxHealth -= nr;
    }

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
        prop = sub.transform.Find("model").gameObject;
        prop = prop.transform.Find("propeller").gameObject;
        restoreHealth();
    }

    private bool isInSub()
    {
        return manager.inSub;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SubmarineStation")
        {
            manager.enterStation();
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "SubmarineStation")
        {
            manager.exitStation();
        }
    }

    private void FixedUpdate()
    {
        if(isInSub()){
            Cursor.lockState = CursorLockMode.Locked;
            // Get mouse movement along the X and Y axes
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate rotation angles based on mouse input
            float rotationX = mouseY * mouseSensitivity;
            float rotationY = mouseX * mouseSensitivity;

            // Apply rotations to the GameObject
            transform.Rotate(Vector3.up, rotationY);
            if(rotationX > -85 && rotationX < 85){
                transform.Rotate(Vector3.right, -rotationX);
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }
    // Update is called once per frame
    private Int64 frameCounter;
    void Update()
    {
        if (isInSub())
        {
            if(Input.GetMouseButton(0)){
                // be consistent without depending on FPS
                //Debug.Log(frameCounter % Math.Round((shootFramesDelay * (60 / (1.0 / Time.deltaTime)))));
                //if(frameCounter % Math.Round((shootFramesDelay * ( 60 / (1.0 / Time.deltaTime)))) <= 0.25){
                if (frameCounter % shootFramesDelay == 0)
                {
                    Debug.Log("Fire");
                    //sub.GetComponent<Rigidbody>().AddForce(sub.transform.forward * -30f);
                    //sub.GetComponent<Rigidbody>().velocity = sub.transform.forward * -(speed/3);
                    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    Debug.DrawRay(ray.origin, ray.direction * 10);
                    RaycastHit hitData;
                    Physics.Raycast(ray, out hitData);
                    //Debug.Log(hitData.transform.gameObject.name);
                    if(hitData.transform.gameObject.tag == "Shark"){
                        int metal = hitData.transform.gameObject.GetComponent<Shark>().takeDamage(100);
                        manager.pickupMetal(metal);
                    }
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                sub.GetComponent<Rigidbody>().velocity = sub.transform.forward * speed;
                prop.transform.Rotate(0f, 0f, -3.5f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                sub.GetComponent<Rigidbody>().velocity = sub.transform.forward * -speed;
                prop.transform.Rotate(0f, 0f, -3.5f);
            }

            if (Input.GetKey(KeyCode.D))
            {
                //rotates from back, used -rotspeed so it pushes to left therefor making the front go to the right
                //subRB.AddForceAtPosition(new Vector3(-rotSpeed, 0f, 0f), sub.transform.position - new Vector3(0f, 0f, 2.19f), ForceMode.Force);
                sub.GetComponent<Rigidbody>().velocity = sub.transform.right * speed;
                prop.transform.Rotate(0f, 0f, -3.5f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                sub.GetComponent<Rigidbody>().velocity = sub.transform.right * -speed;
                prop.transform.Rotate(0f, 0f, -3.5f);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (sub.transform.position.y < maxHeight)
                {
                    subRB.velocity += new Vector3(0f, heightSpeed, 0f) * Time.deltaTime;
                }
                else
                {
                    subRB.velocity -= new Vector3(0f, subRB.velocity.y, 0f);
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                subRB.velocity += new Vector3(0f, -heightSpeed, 0f) * Time.deltaTime;
            }
            // dont change
            if (inSub)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    manager.actionSub();
                }
            }
            inSub = true;
            if(sub.transform.position.y >= maxHeight){
                sub.transform.position = new Vector3(sub.transform.position.x, sub.transform.position.y - .05f, sub.transform.position.z);
            }
            frameCounter += 1;
        }
        else
        {
            inSub = false;
        }
    }
}
