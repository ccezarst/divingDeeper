 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubScript : MonoBehaviour
{
    public bool inSub = false;
    private GameObject sub;
    private GameObject subModel;
    private GameObject CameraPos;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        sub = this.gameObject;
        subModel = sub.transform.Find("TitanModel").gameObject;
        CameraPos = subModel.transform.Find("CameraPos").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.parent == subModel.gameObject.transform)
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
                sub.transform.Rotate(0f,0.02f,0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                sub.transform.Rotate(0f, -0.02f, 0f);

            }

            if (Input.GetKey(KeyCode.E))
            {
                sub.transform.position += new Vector3(0f, 10f, 0f) * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                sub.transform.position += new Vector3(0f, -10f, 0f) * Time.deltaTime;
            }
        }
        else
        {
            inSub = false;
        }
    }
}
