using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource ass;
    public AudioSource ass1;
    public AudioSource ass2;
    public AudioSource assMM;

    public AudioListener CameraLis;

    public AudioClip movePlayer;
    public bool MusicState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && ass.isPlaying == false) ass.Play();
        if ((Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)) ass.Stop();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.instance.inShop) ass1.Play();
        else if (Input.GetKeyUp(KeyCode.Mouse0) == true) ass1.Stop();

        MusicState = FindObjectOfType<MainPlant>().health <= FindObjectOfType<MainPlant>().maxHealth / 5 || FindObjectOfType<MainPlant>().water <= FindObjectOfType<MainPlant>().maxWater / 5 ? true : false;


        ///    if ((FindObjectOfType<MainPlant>().health <= FindObjectOfType<MainPlant>().maxHealth / 5 || FindObjectOfType<MainPlant>().water <= FindObjectOfType<MainPlant>().maxWater / 5) && ass2.isPlaying == false) ass2.Play();

        //    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) ass.PlayOneShot(movePlayer);*/

        if (GameManager.instance.IsDead)
        {
            CameraLis.enabled = false;
        }
        else
        {
            CameraLis.enabled = true;

        }
    }
}
