using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            if (hits[0].GetComponent<EnemyHealth>().isActiveAndEnabled)
            {
                hits[0].GetComponent<EnemyHealth>().EnemyDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
