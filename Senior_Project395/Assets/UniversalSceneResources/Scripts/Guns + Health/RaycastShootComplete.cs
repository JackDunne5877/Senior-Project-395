using UnityEngine;
using System.Collections;
using StarterAssets;
using Photon.Pun;

namespace Com.Orion.MP
{
    public class RaycastShootComplete : MonoBehaviourPun
    {

        public int gunDamage = 1;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
        public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
        public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
        public float hitForce = 100f;                                        // Amount of force which will be added to objects with a rigidbody shot by the player
        public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
        public bool isFullAuto = false;
        public float laserDuration = 0.07f;
        public int ammoCapacity = 20;
        private int currentAmmo;
        public AudioClip emptyMagazineSound;
        public AudioClip reloadSound;


        public Camera fpsCam;                                                // Holds a reference to the first person camera
        private WaitForSeconds shotDuration;   // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
        private randomSoundPlayer gunAudioPlayer;                                        // Reference to the audio source which will play our shooting sound effect
        private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline
        private float nextFire;       // Float to store the time the player will be allowed to fire again, after firing
        private InteractableGun interactableGun;

        private PhotonView pv;

        void Start()
        {
            pv = PhotonView.Get(this);

            // Get and store a reference to our LineRenderer component
            laserLine = GetComponent<LineRenderer>();

            // Get and store a reference to our AudioSource component
            gunAudioPlayer = GetComponent<randomSoundPlayer>();


            shotDuration = new WaitForSeconds(0.07f);

            interactableGun = gameObject.GetComponentInChildren<InteractableGun>();

            currentAmmo = ammoCapacity;
        }

        void Update()
        {

            if (!photonView.IsMine)
            {
                return;
            }
            // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
            if (interactableGun.isEquipped)
            {
                GetComponentInParent<HUD_Controller>().updateAmmoCount(currentAmmo);
            }

            if (interactableGun.isEquipped && (Input.GetButtonDown("Fire1") || (isFullAuto && Input.GetButton("Fire1"))) && Time.time > nextFire)
            {
                if (currentAmmo > 0)
                {
                    //Shoot();
                    NetworkShoot();
                }
                else
                {
                    IndicateOutOfAmmo();
                }

            }

            if (interactableGun.isEquipped && GetComponentInParent<StarterAssetsInputs>().reload)
            {
                currentAmmo = ammoCapacity;
                GetComponentInParent<StarterAssetsInputs>().reload = false;
                IndicateReload();
            }
        }

        public void NetworkShoot()
        {
            //calls the shoot function on all the local versions of this player so that the shot is synced
            pv.RPC("Shoot", RpcTarget.All);
        }

        [PunRPC]
        public void Shoot()
        {
            if (!this.gameObject.activeInHierarchy)
            {
                return;
            }
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            if (fpsCam)
            {
                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                // Declare a raycast hit to store information about what our raycast has hit
                RaycastHit hit;

                // Set the start position for our visual effect for our laser to the position of gunEnd
                laserLine.SetPosition(0, gunEnd.position);

                //use up ammo
                currentAmmo--;


                // Check if our raycast has hit anything
                if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
                {
                    // Set the end position for our laser line 
                    laserLine.SetPosition(1, hit.point);
                    Debug.Log("raycast hit: " + hit.collider.gameObject.name);

                    // Get a reference to a health script attached to the collider we hit
                    Damageable dmgb = hit.collider.GetComponent<Damageable>();

                    // If there was a health script attached
                    if (dmgb != null)
                    {
                        // Call the damage function of that script, passing in our gunDamage variable
                        int pointsEarned = dmgb.doDamage(gunDamage);
                        if (pointsEarned > 0)
                        {
                            Debug.Log("trying to add points in raycastShoot");
                            GetComponentInParent<HUD_Controller>().AddPoints(pointsEarned);
                        }
                    }

                    // Check if the object we hit has a rigidbody attached
                    if (hit.rigidbody != null)
                    {
                        // Add force to the rigidbody we hit, in the direction from which it was hit
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                }
                else
                {
                    // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                    laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
                }
            }
        }

        private void IndicateOutOfAmmo()
        {
            gunAudioPlayer.playThisSound(emptyMagazineSound);
        }

        private void IndicateReload()
        {
            gunAudioPlayer.playThisSound(reloadSound);
        }

        private IEnumerator ShotEffect()
        {
            // Play the shooting sound effect
            gunAudioPlayer.playRandomSound();

            // Turn on our line renderer
            laserLine.enabled = true;

            //Wait for .07 seconds
            yield return shotDuration;

            // Deactivate our line renderer after waiting
            laserLine.enabled = false;
        }
    }
}