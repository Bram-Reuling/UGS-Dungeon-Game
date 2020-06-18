using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int damage = 10;
    public float maxRange = 100f;
    public Camera mainCamera;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxRange))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }
    }
}
