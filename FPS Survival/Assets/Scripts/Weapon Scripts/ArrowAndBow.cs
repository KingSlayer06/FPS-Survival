using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndBow : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] private float speed = 30f;
    [SerializeField] private float deactivateTimer = 3f;
    [SerializeField] private float damage = 15f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactivate", deactivateTimer);
    }

    void Deactivate()
    {
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Fire(Camera mainCamera)
    {
        rigidbody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + rigidbody.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.ENEMY)
        {
            other.gameObject.GetComponent<EnemyHealth>().EnemyDamage(damage);
        }
    }
}
