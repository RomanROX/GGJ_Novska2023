using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour
{
    public float bucketAmount;
    private float water = 0;
    public LayerMask plantLayer;
    bool inUse = false;
    public Slider bucketSlider;
    public GameObject bucket;
    Animator anim;

    private void Start()
    {
        bucketSlider.maxValue = bucketAmount;
        bucketSlider.value = water;
        anim = bucket.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5f, plantLayer) && Input.GetKey(KeyCode.F))
        {
            bucket.SetActive(true);
            inUse = true;
            if (hit.transform.CompareTag("Plant"))
            {
                if (water > 0 && hit.transform.GetComponent<MainPlant>().water < hit.transform.GetComponent<MainPlant>().maxWater -20)
                {
                    hit.transform.GetComponent<MainPlant>().water += 20f * Time.deltaTime;
                    water -= 20f * Time.deltaTime;
                }
                else water = 0;
            }
            else
            {
                if (water < bucketAmount)
                {
                    hit.transform.GetComponent<Water>().waterAmount -= 20f * Time.deltaTime;
                    water += 20f * Time.deltaTime;
                    if (hit.transform.GetComponent<Water>().waterAmount <= 0) Destroy(hit.transform.gameObject);
                }
                else water = bucketAmount; 
            }
            bucketSlider.value = water;
        }
        else 
        {
            anim.SetTrigger("Stop");
            Invoke(nameof(StopUsing), 1f);
        }
    }

    void StopUsing()
    {
        inUse = false;
        bucket.SetActive(false);
    }
}