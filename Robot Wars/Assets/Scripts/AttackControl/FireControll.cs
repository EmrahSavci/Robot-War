using EpicToonFX;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControll : MonoBehaviour
{
    public GameObject Effect;
    public Transform spawnPos;
    public float speed = 2;
    public BulletInfo bullet;
    [Space(15)]
    [Header("Standart Fire")]

    public Joystick FireJoystick;
    Animator animation;
    public bool isFireActive = false;
    [Space(15)]
    [Header("Bomb Zone")]
    [SerializeField] BombZone bombZone;
    [SerializeField] GameObject bombZoneAreaActiveBtn;
    [SerializeField] Joystick bombZoneJoystick;

    PhotonView photonView;
    void Start()
    {
        FireJoystick = SetJoystick.Instance.standartFireJoystick;
        bombZoneJoystick = SetJoystick.Instance.bombZoneJoystick;

        bombZone = UIManager.instance.bombZone;
        bombZoneAreaActiveBtn = FireButtonControll.instance.BombZoneButton.gameObject;
        FireButtonControll.instance.BombZoneButton.onClick.AddListener(() => BombAttack());

        animation = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            float horizontal = FireJoystick.Horizontal;
            float vertical = FireJoystick.Vertical;

            if (vertical != 0 || horizontal != 0)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(FireJoystick.Horizontal, 0, FireJoystick.Vertical) * 2 * Time.fixedDeltaTime);

                FireAttack();
            }
            else if (isFireActive && Input.GetMouseButtonUp(0))
            {
                isFireActive = false;
                animation.SetBool("Fire", true);

                Attack(0);
            }
        }
        
    }
    public void Attack(int _bounceCount)
    {  
       GameObject projectile =  PhotonNetwork.Instantiate("GunBullet/"+bullet.bulletName, spawnPos.position, Quaternion.identity);
      //GameObject projectile = Instantiate(bullet.bulletPrefab, spawnPos.position, Quaternion.identity) as GameObject;
        //projectile.transform.LookAt(transform.forward);
        projectile.GetComponent<Rigidbody>().AddForce(-spawnPos.transform.right * bullet.bulletSpeed, ForceMode.Impulse);
        projectile.GetComponent<ETFXProjectileScript>().bounceCount = _bounceCount;
       
    }
    public void BombAttack()
    {  if(FireButtonControll.instance.bombZoneFillValue>=1)
        {
            bombZone.gameObject.SetActive(true);
            bombZone.transform.position = transform.position;
            bombZoneAreaActiveBtn.SetActive(false);
            bombZoneJoystick.gameObject.SetActive(true);
            bombZone.isActiveBomb = true;
        }
       
    }
    public void FireAttack()
    {
        isFireActive = true;
        animation.SetFloat("Blend", 0);
        animation.SetBool("Attack", true);
        animation.SetBool("Idle",false);
    }
}
