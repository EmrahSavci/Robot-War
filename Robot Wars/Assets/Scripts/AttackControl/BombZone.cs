using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombZone : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 10;
    public bool isActiveBomb;
    [Space(20)] 
    [Header("BOMB")]
    public GameObject bombPrefab;
    public float bombDownSpeed;
    public float spawnRadius;
    public int bombCount;
    void Start()
    {
        joystick = SetJoystick.Instance.bombZoneJoystick;
    }
    private void Update()
    {
       
        if (isActiveBomb)
        {
            Move();
            if (Input.GetMouseButtonUp(0))
            {
                isActiveBomb = false;
                StartCoroutine(SpawnBomb());
            }
        }
    }

    private void Move()
    {
        float horizontal = joystick.Horizontal * moveSpeed;
        float vertical = joystick.Vertical * moveSpeed;

        transform.position += new Vector3(horizontal, 0, vertical);
    }

    public IEnumerator SpawnBomb()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        yield return delay;
        for (int i = 0; i < bombCount; i++)
        {
            GameObject bomb = PhotonNetwork.Instantiate("Bomb/Rocket_Blue", transform.position + (Vector3.up * 35) + Random.insideUnitSphere * spawnRadius, Quaternion.Euler(0, 0, 180));
           // GameObject bomb = Instantiate(bombPrefab, transform.position + (Vector3.up * 35) + Random.insideUnitSphere*spawnRadius, Quaternion.Euler(0, 0, 180));
            bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.up * bombDownSpeed, ForceMode.Impulse);
            yield return delay;
        }
        FireButtonControll.instance.BombZoneButtonActive();
        gameObject.SetActive(false);
    }
}
