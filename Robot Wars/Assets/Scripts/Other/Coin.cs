using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Coin : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotateSpeed*Time.deltaTime, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        ITrigger trigger=collision.gameObject.GetComponent<ITrigger>();
        if(trigger != null)
        {
            trigger.CollectItem(1);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
