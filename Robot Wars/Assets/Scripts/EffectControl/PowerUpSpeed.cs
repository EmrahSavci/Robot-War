using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    public ParticleSystem childEffect;
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            childEffect.gameObject.SetActive(true);
            childEffect.transform.parent = null;
            childEffect.Play();
           // UIManager.instance.SpeedEffectFillAmount();
            Vector3 pos=Camera.main.WorldToScreenPoint(other.gameObject.transform.position);
            
            UIManager.instance.EnergyIconSpawn(pos);
            FireButtonControll.instance.bombZoneFillValue += 0.05f;
            gameObject.SetActive(false);    
        }
    }
}
