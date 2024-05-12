using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
namespace CharacterMove
{


    public class CharacterMoveControll : MonoBehaviour,ITrigger
    {
        public static CharacterMoveControll instance;
        public Joystick joystick;
        public FireControll fireControl;
        [Header("Companents")]
        PhotonView pw;
        Rigidbody rb;
        [SerializeField] CharacterAnimation animation;
        [Header("Move Values")]
        public float moveSpeed;
        public float currentSpeed = 300;
        public float rotateSpeed;

        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            joystick = SetJoystick.Instance.moveControllJoystick;
            fireControl=GetComponent<FireControll>();

            pw= GetComponent<PhotonView>();
            rb= GetComponent<Rigidbody>();

            moveSpeed = currentSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (pw.IsMine)
            {

                
                    

                float horizontal = joystick.Horizontal;
                float vertical = joystick.Vertical;

                if (vertical != 0 || horizontal != 0)
                {
                    animation.MoveForward(true);
                    Vector3 move = new Vector3(joystick.Horizontal, rb.velocity.y, joystick.Vertical) * moveSpeed * Time.fixedDeltaTime;

                    rb.velocity = transform.forward * moveSpeed * Time.fixedDeltaTime;
                    if (!fireControl.isFireActive)
                        transform.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, rb.velocity.y, joystick.Vertical) * rotateSpeed * Time.fixedDeltaTime);
                    if(moveSpeed>currentSpeed)
                    {
                        UIManager.instance.SpeedImgFillAmounDecrease();
                    }
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    animation.MoveForward(false);

                }


            }
        }
        public void MoveSpeedIncrease()
        {
            moveSpeed += 300;
            animation.Run(true);
        }
        public void MoveSpeedDecrease()
        {
            moveSpeed = currentSpeed;
            animation.Run(false);
        }

        public void CollectItem(float value)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            UIManager.instance.CoinIconSpawn(pos);
        }
    }
}