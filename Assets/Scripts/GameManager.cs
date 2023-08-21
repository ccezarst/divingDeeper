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
    public bool plrInStation = false;
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
        stationEnterText.SetActive(true);
        lastPlayerSpawn = plrSpawn;
    }
    public void exitStation()
    {
        inStation = false;
        stationEnterText.SetActive(false);
    }
    // logic here cuz it's simpler
    public void actionSub(){
        if (inSub && inStation){
            exitSub();
        }else if (inStation){
            enterSub();
        }
    }
    public void exitSub(){
        player.SetActive(true);
        player.transform.position = lastPlayerSpawn;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position = player.transform.Find("CameraPos").gameObject.transform.position;
        GameObject sub = GameObject.Find("Titan");
        sub.GetComponent<Rigidbody>().velocity = Vector3.zero;
        inSub = false;
    }
    public void enterSub(){
        GameObject sub = GameObject.Find("Titan");
        Camera.main.transform.parent = sub.transform;
        Camera.main.transform.position = sub.transform.Find("CameraPos").gameObject.transform.position;
        player.SetActive(false);
        inSub = true;
    }
}
