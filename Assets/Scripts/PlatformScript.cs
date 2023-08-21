using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private GameObject platform;
    private GameManager manager;
    private GameObject subEnterCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(platformWait(true));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(platformWait(false));
        }
    }

    IEnumerator platformWait(bool OPValue)
    {
        yield return new WaitForSeconds(0.1f);
        subEnterCanvas.SetActive(OPValue);
        manager.onPlatform = OPValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        platform = this.gameObject;
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        subEnterCanvas = GameObject.Find("Canvas").transform.Find("subEnter").gameObject;
        print(subEnterCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
