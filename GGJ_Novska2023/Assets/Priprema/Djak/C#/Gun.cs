using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bullet;
    public float shootForce;

    [Header("Gun Settings")]
    public float timeBetweenShooting;
    public float spread; 
    public float reloadTime; 
    public float timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    [Header("Recoil")]
    public float recoilForce;
    Rigidbody playerRB;

    [Header("Input")]
    public KeyCode reloadButton = KeyCode.R;

    [Header("GFX")]
    public GameObject muzzleFlash;

    [Header("UI")]
    public Slider magazine;
    public Text bulletsText;

    [HideInInspector] public int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    Camera fpsCam;
    Animator anim;
    public Transform attackPoint;

    //BUG FIXING
    bool allowInvoke = true;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        fpsCam = Camera.main;
        anim = GetComponent<Animator>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        magazine.maxValue = magazineSize;
    }

    private void Update()
    {
         MyInput();
        UIUpdate();
    }

    void MyInput()
    {
        //ALLOW HOLD BUTTON OR NOT
        if (allowButtonHold) shooting = Input.GetMouseButton(0);
        else shooting = Input.GetMouseButtonDown(0);

        //SHOOT
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
        //else if (shooting && !reloadText.activeInHierarchy && bulletsLeft <= 0)
           //AUDIO LOL 

        if (Input.GetKeyDown(reloadButton) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            //YOUHAVETORELOADTEXTSTUFF();
        }
    }

    void UIUpdate()
    {
        magazine.value = bulletsLeft;
        bulletsText.text = bulletsLeft.ToString() + " / " + magazineSize.ToString();
    }

    public void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new(.5f, .5f, 0));

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        //CALCULATE DIRECTION FROM GUN TO TARGET POINT
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //ADD SPREAD
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //FINAL DIRECTION
        Vector3 direction = directionWithoutSpread + new Vector3(x, y, 0);

        //INSTANTIATE
        GameObject projectile = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        projectile.transform.forward = direction.normalized;
        //projectile.layer = LayerMask.NameToLayer("NoCollision"); //bug fixing
        bulletsLeft--;

        projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        //ADD RECOIL TO PLAYER
        if (allowInvoke)
            playerRB.AddForce(-direction.normalized * recoilForce, ForceMode.Impulse);
        //anim.SetTrigger("Shoot");
        Invoke(nameof(ResetShoot), timeBetweenShooting);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    void Reload()
    {
        //CHANGE ANIMATION SPEED THROUGH PARAMETER
        //anim.SetFloat("Duration", 1 / reloadTime);

        //anim.SetTrigger("Reload");
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}