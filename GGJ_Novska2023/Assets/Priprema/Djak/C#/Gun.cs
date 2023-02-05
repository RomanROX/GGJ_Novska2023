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
    public float magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    [Header("Recoil")]
    public float recoilForce;
    Rigidbody playerRB;

    [Header("Input")]
    public KeyCode reloadButton = KeyCode.R;

    [Header("GFX")]
    public GameObject muzzleFlash;

    [Header("UI")]
    public Image magazine;
    public Text bulletsText;
    public Text reloadIndicator;

    [HideInInspector] public float bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    Camera fpsCam;
    Animator anim;
    public Transform attackPoint;

    //BUG FIXING
    bool allowInvoke = true;

    public void Start()
    {
        bulletsLeft = Mathf.Round(magazineSize);
        readyToShoot = true;
        fpsCam = Camera.main;
        anim = GetComponent<Animator>();
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
         MyInput();
        UIUpdate();
        anim.SetFloat("MovingSpeed", Input.GetAxisRaw("Vertical"));
    }

    void MyInput()
    {
        //ALLOW HOLD BUTTON OR NOT
        if (allowButtonHold) shooting = Input.GetMouseButton(0);
        else shooting = Input.GetMouseButtonDown(0);

        //SHOOT
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && !GameManager.instance.inShop && Time.timeScale != 0)
        {
            bulletsShot = 0;
            Shoot();
        }
        //else if (shooting && !reloadText.activeInHierarchy && bulletsLeft <= 0)
           //AUDIO LOL 

        if (Input.GetKeyDown(reloadButton) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            reloadIndicator.gameObject.SetActive(true);
            Invoke(nameof(ResetNekaj), .5f);
        }
    }

    void UIUpdate()
    {
        magazine.fillAmount = Mathf.Lerp(magazine.fillAmount, bulletsLeft / magazineSize, .2f);
        bulletsText.text = Mathf.Round(bulletsLeft).ToString() + " / " + Mathf.Round(magazineSize).ToString();
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
        if (muzzleFlash != null) Instantiate(muzzleFlash, attackPoint.position, attackPoint.rotation);

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
        anim.SetFloat("Duration", 1 / reloadTime);

        anim.SetTrigger("Reload");
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    void ResetNekaj()
    {
        reloadIndicator.gameObject.SetActive(false);
    }
}