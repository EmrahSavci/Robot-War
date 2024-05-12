using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{  
    public static CharacterAnimation instance;

    Animator animator;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        FireButtonControll.instance.doubleFireButton.onClick.AddListener(() => AttackAnim(1));
        FireButtonControll.instance.skillFireButton.onClick.AddListener(() => AttackAnim(1.5f));
    }

    public void MoveForward(bool isBool)
    {
        animator.SetBool("Move", isBool);
        animator.SetBool("Idle", !isBool);
       
    }
    public void AttackAnim(float blendValue)
    {
        animator.SetBool("Attack", true);
        animator.SetFloat("Blend", blendValue);
       
    }
    public void Idle()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", true);
        Debug.Log("Idle animasyon çalýþtý");
    }
    public void Run(bool isBool)
    {
        animator.SetBool("Move", !isBool);
        animator.SetBool("Run", isBool);
        
    }
    public void Damage(bool isBool)
    {
        animator.SetBool("Damage", isBool);
    }
    public void DamageStop()
    {
        animator.SetBool("Damage", false);
    }
    public void Die()
    {
        animator.SetBool("Die", true);
        
    }
}
