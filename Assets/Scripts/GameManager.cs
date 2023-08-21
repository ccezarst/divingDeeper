using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GameManager : MonoBehaviour
{
    public GameObject stationEnterText;
    public GameObject stationExitText;
    public GameObject player;
    public bool inStation = false;
    public bool inSub = true;

    private Vector3 lastPlayerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enterStation(Vector3 plrSpawn){
        inStation = true;
        stationEnterText.active = true;
        lastPlayerSpawn = plrSpawn;
    }
    public void exitStation()
    {
        inStation = false;
        stationEnterText.active = false;
    }
    // logic here cuz it's simpler
    public void actionSub(){
        if (inSub){
            exitSub();
        }else{
            enterSub();
        }
    }
    public void exitSub(){
        player.active = true;
        player.transform.position = lastPlayerSpawn;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position = player.transform.Find("CameraPos").gameObject.transform.position;
        inSub = true;
    }
    public void enterSub(){
        GameObject sub = GameObject.Find("Titan");
        Camera.main.transform.parent = sub.transform;
        Camera.main.transform.position = sub.transform.Find("CameraPos").gameObject.transform.position;
        player.active = false;
        inSub = false;
    }
}
