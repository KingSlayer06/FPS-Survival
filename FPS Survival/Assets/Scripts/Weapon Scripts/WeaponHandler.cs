using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    NONE,
    BULLET,
    SPEAR,
    ARROW
}

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioSource shootSound, reloadSound;
    [SerializeField] private AudioClip[] axeSoundClip; 
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject muzzleFlash;

    public WeaponAim weaponAim;
    public WeaponFireType weaponFireType;
    public WeaponBulletType weaponBulletType;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        animator.SetTrigger(AnimatorTags.SHOOT);
    }

    public void Aim(bool canAim)
    {
        animator.SetBool(AnimatorTags.AIM, canAim);
    }

    public void MuzzleFlashOn()
    {
        muzzleFlash.SetActive(true);
    }

    public void MuzzleFlashOff()
    {
        muzzleFlash.SetActive(false);
    }

    public void PlayShootSound()
    {
        shootSound.Play();
    }

    public void PlayReloadSound()
    {
        reloadSound.Play();
    }

    public void AxeAttackSound()
    {
        shootSound.clip = axeSoundClip[Random.Range(0, axeSoundClip.Length)];
        shootSound.Play();
    }

    public void AttackPointOn()
    {
        attackPoint.SetActive(true);
    }

    public void AttackPointOff()
    {
        if(attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }
}
