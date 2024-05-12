using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombExplosionEffect;
    public float damageRadius;
    public int DamageValue;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {


        Explosion();
        PhotonNetwork.Destroy(gameObject);

    }
    void Explosion()
    {
        Instantiate(bombExplosionEffect, transform.position - Vector3.up * 2, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider collider in colliders)
        {  
            IDamagable trigger=collider.GetComponent<IDamagable>();
            if(trigger!=null)
            {
                collider.enabled = false;
                Vector3 direction = transform.position - collider.transform.position;
                Debug.Log("DÝRECTÝON: " + direction.sqrMagnitude+"   value: "+ (10 / direction.sqrMagnitude)+ "     value2: "+ (10 / direction.sqrMagnitude) * DamageValue);
                //collider.GetComponent<Healty>().Damage((int)((10f/direction.sqrMagnitude)*DamageValue));
                trigger.GetDamage((10f / direction.sqrMagnitude) * DamageValue);
            }
        }
    }
}
