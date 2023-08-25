using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class depthEffect : MonoBehaviour
{
    public GameObject sub;
    public float maxDeth = -331f;
    public float minIntesity = 0.2f;
    public float maxIntensity = 1f;
    public Volume volumeComponent;
    // Start is called before the first frame update
    void Start()
    {
       volumeComponent = this.GetComponent<Volume>();   
    }

    // Update is called once per frame
    void Update()
    {
        float intensity = sub.transform.position.y / maxDeth;
        intensity = Mathf.Clamp(intensity, 0, 1);
        intensity = Mathf.Lerp(minIntesity, maxIntensity, intensity);
        volumeComponent.weight = intensity;
    }
}
