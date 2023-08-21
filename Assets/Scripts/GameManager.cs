using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject subExitText;
    public GameObject subEnterText;
    public GameObject player;
    public bool inStation = false;
    public bool inSub = true;
    public bool onPlatform = false;
    private Vector3 lastPlayerSpawn;
    // Start is called before the first frame update

    private void changeTextVisibility(GameObject textObj, bool visibility){
        textObj.GetComponent<TextMeshProUGUI>().enabled = visibility;    
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeTextVisibility(subEnterText, onPlatform);
    }

    IEnumerator checkActive(GameObject thingToCheck,bool value)
    {
        //after 0.01 seconds rechecks active value and attempts to activate/deactivate if it wasnt how it was intended
        yield return new WaitForSeconds(0.01f);
        if(thingToCheck.active != value)
        {
            thingToCheck.SetActive(value);
        }

    }

    public void enterStation(){
        if(inSub){
            inStation = true;
            changeTextVisibility(subExitText, true);
        }
    }


    public void exitStation()
    {
        inStation = false;
        changeTextVisibility(subExitText, false);
    }


    // logic here cuz it's simpler
    public void actionSub(){
        if (onPlatform)
        {
            enterSub();
        }
        if (inSub && inStation){
            exitSub();
        }
    }
    public void exitSub(){
        changeTextVisibility(subExitText, false);
        player.SetActive(true);
        StartCoroutine(checkActive(player,true));
        player.transform.position = GameObject.Find("PlayerSpawn").transform.position;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position = player.transform.Find("CameraPos").gameObject.transform.position;
        Camera.main.transform.rotation = player.transform.Find("CameraPos").gameObject.transform.rotation;
        GameObject sub = GameObject.Find("Titan");
        sub.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sub.GetComponent<Rigidbody>().isKinematic = true;
        inSub = false;
    }
    public void enterSub(){
        if (onPlatform)
        {
            GameObject sub = GameObject.Find("Titan");
            sub.GetComponent<Rigidbody>().isKinematic = false;
            Camera.main.transform.parent = sub.transform;
            Camera.main.transform.position = sub.transform.Find("CameraPos").gameObject.transform.position;
            Camera.main.transform.rotation = sub.transform.Find("CameraPos").gameObject.transform.rotation;
            player.SetActive(false);
            StartCoroutine(checkActive(player, false));
            inSub = true;
            onPlatform = false;
            changeTextVisibility(subEnterText, false);
            changeTextVisibility(subExitText, true);
        }
    }
}
