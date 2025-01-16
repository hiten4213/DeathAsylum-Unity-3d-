using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weapons : MonoBehaviour
{
    [SerializeField] Camera fps;
    [SerializeField] float gunrange = 5f;
    [SerializeField] float GunDamage = 25f;
    [SerializeField] float ShootDelay = 1f;
    [SerializeField] ammo ammoslot;
    [SerializeField] ammotype ammotype;
    [SerializeField] ParticleSystem muzzleflash;
    [SerializeField] ParticleSystem CartridgeEject;
    [SerializeField] AudioClip gunshot;
    [SerializeField] GameObject AfterHitEffect;
    [SerializeField] GameObject Bloodeffect;
    [SerializeField] TextMeshProUGUI Ammotext;

    [Header("Recoil Settings")]
    [SerializeField] float recoilAmount = 0.1f; // Amount of recoil (movement in units)
    [SerializeField] float recoilRotation = 2f; // Amount of recoil rotation in degrees
    [SerializeField] float recoilDuration = 0.1f; // Duration of the recoil effect

    AudioSource gun;
    bool canShoot = true;

    private Vector3 originalWeaponPosition;
    private Quaternion originalWeaponRotation;

    void Start()
    {
        gun = GetComponent<AudioSource>();

        // Store the original weapon position and rotation
        originalWeaponPosition = transform.localPosition;
        originalWeaponRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
        Displayammo();
        
    }
    void Displayammo()
    {
        int currentammo = ammoslot.getcurrentammo(ammotype);
        Ammotext.text = currentammo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoslot.getcurrentammo(ammotype) > 0)
        {
            muzzleflash.Play();
            CartridgeEject.Play();
            gun.PlayOneShot(gunshot, 1f);
            Raycasting();
            ammoslot.ReduceCurrentAmmo(ammotype);

            // Apply recoil
            ApplyRecoil();
        }
        yield return new WaitForSeconds(ShootDelay);
        canShoot = true;
    }

    private void Raycasting()
    {
        RaycastHit hit;
        if (Physics.Raycast(fps.transform.position, fps.transform.forward, out hit, gunrange))
        {
            createhitimpact(hit);
            enemyhealth target = hit.transform.GetComponent<enemyhealth>();
            if (target == null) return;
            target.TakeDamage(GunDamage);
        }
    }

    private void createhitimpact(RaycastHit hit)
    {
        if (hit.transform.CompareTag("enemy"))
        {
            GameObject impact = Instantiate(Bloodeffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
        else
        {
            GameObject impact = Instantiate(AfterHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1f);
        }
    }

    private void ApplyRecoil()
    {
        // Calculate recoil rotation and position
        Quaternion recoilRotationQuat = Quaternion.Euler(-recoilRotation, 0, 0); // Upward rotation for recoil
        Vector3 recoilPosition = -transform.forward * recoilAmount; // Move weapon back

        // Apply recoil effect
        StartCoroutine(RecoilRoutine(recoilPosition, recoilRotationQuat));
    }

    private IEnumerator RecoilRoutine(Vector3 recoilPosition, Quaternion recoilRotation)
    {
        // Apply recoil effect
        transform.localPosition += recoilPosition;
        transform.localRotation *= recoilRotation; // Apply rotation

        // Wait for recoil duration
        yield return new WaitForSeconds(recoilDuration);

        // Reset weapon position and rotation
        transform.localPosition = originalWeaponPosition;
        transform.localRotation = originalWeaponRotation;
    }
}
