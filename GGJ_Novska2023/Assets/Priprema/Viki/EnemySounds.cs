using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource as2;
    bool canplay = true;
    float randTime;
     // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        randTime = Random.Range(5, 15);
        if (canplay) StartCoroutine(aaa());
    }
    

    public IEnumerator aaa()
    {
        canplay = false;
        yield return new WaitForSeconds(randTime);
        as2.Play();
        canplay = true;
    }
}
