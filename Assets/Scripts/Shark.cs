using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public enum SharkState{
        Idle,
        Angwy,
        Attacking
    }
    public float startHealth = 100f;
    public float health { get; private set; } 
    public bool isAlive { get; private set; }
    public SharkState state
    {
        get;
        private set;
    }
    public int metalDrop = 3;
    public int respawnTime = 5;
    public float angwyRadius = 30;
    public float sharkSpeed = 0.3f;
    public float dmg = 20f;

    private GameObject sub;
    private GameObject model;
    private bool attack;
    IEnumerator deathCo()
    {
        isAlive = false;
        state = SharkState.Idle;
        this.gameObject.tag = "DeadShark";
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        model.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(respawnTime);
        this.gameObject.tag = "Shark";
        this.GetComponent<Collider>().enabled = true;
        model.GetComponent<Renderer>().enabled = true;
        health = startHealth;
        isAlive = true;
    }

    private void died(){
        StartCoroutine(deathCo());
    }
    public int takeDamage(float dmg){
        health -= dmg;
        if (health <= 0) {
            died();
            return metalDrop;
        }else{
            return 0;
        }
    }

    IEnumerator attackCo()
    {
        while(attack){
            sub.GetComponent<SubScriptNew>().takeDamage(dmg);
            yield return new WaitForSeconds(2);
        }
    }

    private void stateChange(){
        if(state == SharkState.Idle){
            attack = false;
        }
        else if(state == SharkState.Angwy){
            attack = false;
            this.transform.LookAt(sub.transform);
            this.transform.position = this.transform.position + this.transform.TransformDirection(new Vector3(0, 0, sharkSpeed));
        }else if(state == SharkState.Attacking){
            //this.transform.LookAt(sub.transform);
            //this.transform.position = this.transform.position + this.transform.TransformDirection(new Vector3(0, 0, sharkSpeed/3));
            if (attack == false){
                attack = true;
                StartCoroutine(attackCo());
            }
            attack = true;
        }
    }
    void Start()
    {
        health = startHealth;
        isAlive = true;
        sub = GameObject.Find("Titan");
        state = SharkState.Idle;
        model = this.transform.Find("model").Find("default").gameObject;
        attack = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distToSub = Vector3.Distance(sub.transform.position, this.gameObject.transform.position);
        if(isAlive){
            if (distToSub <= 8)
            {
                state = SharkState.Attacking;
                stateChange();
            }else{
                if (distToSub <= angwyRadius)
                {
                    state = SharkState.Angwy;
                    stateChange();
                }
                else
                {
                    state = SharkState.Idle;
                    stateChange();
                }
            }
        }
    }
}
