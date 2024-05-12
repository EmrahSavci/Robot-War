using UnityEngine;
using System.Collections;
using Photon.Pun;
namespace EpicToonFX
{
    public class ETFXProjectileScript : MonoBehaviour
    {
        public GameObject impactParticle; // Effect spawned when projectile hits a collider
        public GameObject projectileParticle; // Effect attached to the gameobject as child
        public GameObject muzzleParticle; // Effect instantly spawned when gameobject is spawned
        Rigidbody rb;
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)] // This is an offset that moves the impact effect slightly away from the point of impact to reduce clipping of the impact effect
        public float collideOffset = 0.15f;
        public LayerMask wallLayer;
        public int bounceCount = 1;

        [Space(20)]
        [Header("Bounce Value")]
        public Vector3 lastVelocity;
        public float curSpeed;
        public Vector3 direction;

        PhotonView photonView;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
               // Destroy(muzzleParticle, 1.5f); // 2nd parameter is lifetime of effect in seconds
            }
            photonView= GetComponent<PhotonView>();
        }
        GameObject impactP;
        void FixedUpdate()
        {	
			if (rb.velocity.magnitude != 0)
			{
			    transform.rotation = Quaternion.LookRotation(rb.velocity); // Sets rotation to look at direction of movement
			}
			
            RaycastHit hit;
			
            float radius; // Sets the radius of the collision detection
            if (transform.GetComponent<SphereCollider>())
                radius = transform.GetComponent<SphereCollider>().radius;
            else
                radius = colliderRadius;

            Vector3 direction = rb.velocity; // Gets the direction of the projectile, used for collision detection
            if (rb.useGravity)
                direction += Physics.gravity * Time.deltaTime; // Accounts for gravity if enabled
            direction = direction.normalized;

            float detectionDistance = rb.velocity.magnitude * Time.deltaTime; // Distance of collision detection for this frame
/*
            if (Physics.SphereCast(transform.position, radius, direction, out hit, detectionDistance,wallLayer)) // Checks if collision will happen
            {
                //transform.position = hit.point + (hit.normal * collideOffset); // Move projectile to point of collision

                 impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject; // Spawns impact effect

                ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                //Component at [0] is that of the parent i.e. this object (if there is any)
                for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
                {
                    ParticleSystem trail = trails[i];

                    if (trail.gameObject.name.Contains("Trail"))
                    {
                        trail.transform.SetParent(null); // Detaches the trail from the projectile
                        Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                    }
                }

                Destroy(projectileParticle, 3f); // Removes particle effect after delay
                Destroy(impactP, 3.5f); // Removes impact effect after delay
               // Destroy(gameObject); // Removes the projectile
            }
*/
        }
        private void LateUpdate()
        {
            lastVelocity=rb.velocity;
        }
        public void DestroyEffect(Transform LastPos)
        {
            impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up,LastPos.position)) as GameObject; // Spawns impact effect

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>(); // Gets a list of particle systems, as we need to detach the trails
                                                                                 //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++) // Loop to cycle through found particle systems
            {
                ParticleSystem trail = trails[i];

                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null); // Detaches the trail from the projectile
                    Destroy(trail.gameObject, 2f); // Removes the trail after seconds
                }
            }

            Destroy(projectileParticle, 3f); // Removes particle effect after delay
            Destroy(impactP, 3.5f); // Removes impact effect after delay
            PhotonNetwork.Destroy(gameObject);  // Removes the projectile
        }
        private void OnCollisionEnter(Collision collision)
        {   IDamagable damagable=collision.gameObject.GetComponent<IDamagable>();
            if(damagable!=null)
            {
                damagable.GetDamage(10);
                
                FireButtonControll.instance.bombZoneFillValue += 0.1f;
                DestroyEffect(collision.transform);
            }
            else if(bounceCount<1)
            {
                DestroyEffect(collision.transform);
            }
            else
            {  curSpeed=lastVelocity.magnitude;
                direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
                rb.velocity = direction * Mathf.Max(curSpeed, 0);
                bounceCount--;

            }
            
        }
    }
}