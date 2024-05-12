using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Bullet Info",menuName ="Bullets/Rocket")]
public class BulletInfo : ScriptableObject
{
    public GameObject bulletPrefab;
    public string bulletName;
    public float bulletSpeed;
    public int damage;
    public int bounceCount = 1;
}
