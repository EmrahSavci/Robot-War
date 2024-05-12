using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffect : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable!=null)
        {
            damagable.GetHealty();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
