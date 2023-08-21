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
    public bool nextToSub = false;
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
        
    }

    public void updateNearSub(bool newVal){
        nextToSub = newVal;
        if (newVal){
            changeTextVisibility(subEnterText, true);
        }
        else{
            changeTextVisibility(subEnterText, false);
        }
    }

    public void enterStation(Vector3 plrSpawn){
        if(inSub){
            inStation = true;
            changeTextVisibility(subExitText, true);
            lastPlayerSpawn = plrSpawn;
        }
    }
    public void exitStation()
    {
        inStation = false;
        changeTextVisibility(subExitText, false);
    }
    // logic here cuz it's simpler
    public void actionSub(){
        if (nextToSub)
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
        player.transform.position = lastPlayerSpawn;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position = player.transform.Find("CameraPos").gameObject.transform.position;
        Camera.main.transform.rotation = player.transform.Find("CameraPos").gameObject.transform.rotation;
        GameObject sub = GameObject.Find("Titan");
        sub.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sub.GetComponent<Rigidbody>().isKinematic = true;
        inSub = false;
    }
    public void enterSub(){
        Debug.Log("go kys");
        GameObject sub = GameObject.Find("Titan");
        sub.GetComponent<Rigidbody>().isKinematic = false;
        Camera.main.transform.parent = sub.transform;
        Camera.main.transform.position = sub.transform.Find("CameraPos").gameObject.transform.position;
        Camera.main.transform.rotation = sub.transform.Find("CameraPos").gameObject.transform.rotation;
        player.SetActive(false);
        inSub = true;
        nextToSub = false;
        changeTextVisibility(subEnterText, false);
        changeTextVisibility(subExitText, true);
        Debug.Log("go kys2");
    }
}
