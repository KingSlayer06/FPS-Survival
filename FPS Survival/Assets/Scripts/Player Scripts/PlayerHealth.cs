using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private WeaponAttack WeaponAttack;
    private WeaponManager weaponManager;
    private MouseLook mouseLook;
    private PlayerStats playerStats;

    [SerializeField] private float health = 100f;
    private bool isDead;


    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        WeaponAttack = GetComponent<WeaponAttack>();
        weaponManager = GetComponent<WeaponManager>();
        mouseLook = GetComponentInChildren<MouseLook>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void PlayerDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;

        if(health > 0)
        {
            playerStats.DisplayHealth(health);
        }

        if (health <= 0f)
        {
            isDead = true;
            Dead();
        }
        // Show GUI
    }

    private void Dead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY);
        for(int i=0; i<enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyController>().enabled = false;
        }

        playerMovement.enabled = false;
        WeaponAttack.enabled = false;
        weaponManager.GetActiveWeapon().gameObject.SetActive(false);
        mouseLook.enabled = false;

        Invoke("Restart", 3f);
    }

    private void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
