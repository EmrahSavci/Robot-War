using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public static CameraControll instance;

    public Transform targetPlayer;
    public Vector3 offset;
    public Vector3 direction;
    public float followSpeed=5;
    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
      
        
    }
    public void SetOffset()
    {
        //offset = transform.position - targetPlayer.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetPlayer != null)
        {
            direction = offset+targetPlayer.position;
            transform.position=Vector3.MoveTowards(transform.position, direction, followSpeed*Time.deltaTime);
        }
    }
}
