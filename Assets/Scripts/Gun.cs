//////////////////////////////////////////////////////////////////
///
/// ---------------------- Gun.cs -----------------------------
/// 
/// Made by: Bram Reuling
/// 
/// Description: Script for shooting the gun.
/// 
/// Gun.cs contains the following classes:
/// - Fire()
/// 
//////////////////////////////////////////////////////////////////
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float maxRange = 100f;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private Transform gunBarrel;

    [SerializeField]
    private ParticleSystem flash;
    [SerializeField]
    private AudioSource audioShoot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0 && !audioShoot.isPlaying)
        {
            Fire();
            audioShoot.Play();
        }
    }

    private void Fire()
    {
        flash.Play();

        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, maxRange))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

        }

    }
}
