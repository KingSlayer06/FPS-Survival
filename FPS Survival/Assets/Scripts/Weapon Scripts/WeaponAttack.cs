using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private Animator FPCameraAnimator;
    private Camera mainCamera;
    private GameObject crosshair;

    private float nextTimeToFire;
    private bool zoom;
    private bool isAiming;

    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float damage = 20f;
    
    [SerializeField] private Transform arrowBowSpawn;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject spearPrefab;

    private void Awake()
    {
        mainCamera = Camera.main;
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        weaponManager = GetComponent<WeaponManager>();
        FPCameraAnimator = GameObject.FindWithTag(Tags.FPCAMERA).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponHandler activeWeapon = weaponManager.GetActiveWeapon();
        Shoot(activeWeapon);
        ZoomInAndOut(activeWeapon);
    }

    private void Shoot(WeaponHandler activeWeapon)
    {
        if (activeWeapon.weaponFireType == WeaponFireType.MULTIPLE)
        {
            if(Input.GetKey(Keycode.MOUSE_LEFTCLICK) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                activeWeapon.ShootAnimation();
                FireBullet();
            } 
        }
        else
        {
            if (Input.GetKeyDown(Keycode.MOUSE_LEFTCLICK))
            {
                if (activeWeapon.tag == Tags.AXE)
                {
                    activeWeapon.ShootAnimation();
                }

                else if (activeWeapon.weaponBulletType == WeaponBulletType.BULLET)
                {
                    activeWeapon.ShootAnimation();
                    FireBullet();
                }
                else
                {
                    if(isAiming)
                    {
                        activeWeapon.ShootAnimation();
                        if(activeWeapon.weaponBulletType == WeaponBulletType.ARROW)
                        {
                            // Shoot Arrow
                            ShootArrow();
                        } 
                        else if(activeWeapon.weaponBulletType == WeaponBulletType.SPEAR)
                        {
                            // Throw Spear
                            ThrowSpear();
                        }
                    }
                }

            }
        }
    }

    private void ZoomInAndOut(WeaponHandler activeWeapon)
    {
        if(activeWeapon.weaponAim == WeaponAim.AIM)
        {
            // Zoom In
            if(Input.GetKeyDown(Keycode.MOUSE_RIGHTCLICK))
            {
                FPCameraAnimator.Play(AnimatorTags.ZOOM_IN);
                crosshair.SetActive(false);
            }

            // Zoom Out
            if(Input.GetKeyUp(Keycode.MOUSE_RIGHTCLICK))
            {
                FPCameraAnimator.Play(AnimatorTags.ZOOM_OUT);
                crosshair.SetActive(true);
            }
        }

        if(activeWeapon.weaponAim == WeaponAim.SELF_AIM)
        {
            // Aim 
            if(Input.GetKeyDown(Keycode.MOUSE_RIGHTCLICK))
            {
                activeWeapon.Aim(true);
                isAiming = true;
            }

            // Un Aim
            if (Input.GetKeyUp(Keycode.MOUSE_RIGHTCLICK))
            {
                activeWeapon.Aim(false);
                isAiming = false;
            }
        }
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowBowSpawn.position;
        arrow.GetComponent<ArrowAndBow>().Fire(mainCamera);
    }

    private void ThrowSpear()
    {
        GameObject spear = Instantiate(spearPrefab);
        spear.transform.position = arrowBowSpawn.position;
        spear.GetComponent<ArrowAndBow>().Fire(mainCamera);
    }

    private void FireBullet()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if(hit.transform.tag == Tags.ENEMY)
            {
                hit.transform.GetComponent<EnemyHealth>().EnemyDamage(damage);
            }
            print(hit.transform.gameObject.tag);
        }
    }
}
