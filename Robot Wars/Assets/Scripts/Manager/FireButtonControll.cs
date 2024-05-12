using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FireButtonControll : MonoBehaviour
{
   public static FireButtonControll instance;

    [Header("Stander Fire Button")]
    public Button standartFireButton;

    [Header("Double Fire Button")]
    public Button doubleFireButton;
    public int doubleFireDelay;
    public TextMeshProUGUI doubleFireDelay_TMP;
    public Image doubleFireFillImg;
    [Space(15)]
    [Header("Skill Button")]
    public Button skillFireButton;
    public TextMeshProUGUI skillFireDelay_TMP;
    public int skillFireDelay;
    public Image skillFireFillImg;
    [Space(15)]
    [Header("Bomb Zone Button")]
    public Button BombZoneButton;
    public Image bombZoneFillImg;
    public float bombZoneFillValue = 0;
    public float bombZoneFillDelay = 0.1f;
    private void Awake()
    {
        instance = this;
        DoubleFireButtonActive();
        SkillFireButtonActive();
        BombZoneButtonActive();
    }
    public void DoubleFireButtonActive()
    {
        StartCoroutine(DoubleFireButton());
      
    }
    public void SkillFireButtonActive()
    {
        
        StartCoroutine(SkillFireButton());
        
    }
    public void BombZoneButtonActive()
    {
       
        StartCoroutine(BombZoneButtonFill());
    }
    IEnumerator DoubleFireButton()
    {
        doubleFireDelay_TMP.gameObject.SetActive(true);
        doubleFireButton.interactable = false;
        LeanTween.value(1, 0, doubleFireDelay).setOnUpdate((float value) => doubleFireFillImg.fillAmount = value);
        for (int i = doubleFireDelay; i >0; i--)
        {
           
            doubleFireDelay_TMP.text=i.ToString()+"s";
            yield return new WaitForSeconds(1);

        }
        doubleFireDelay_TMP.gameObject.SetActive(false);
        doubleFireButton.interactable = true;
    }
    IEnumerator SkillFireButton()
    {
        skillFireDelay_TMP.gameObject.SetActive(true);
        skillFireButton.interactable = false;
        LeanTween.value(1, 0, skillFireDelay).setOnUpdate((float value) => skillFireFillImg.fillAmount = value);
        for (int i = skillFireDelay; i > 0; i--)
        {
            
            skillFireDelay_TMP.text = i.ToString() + "s";
            yield return new WaitForSeconds(1);

        }
        skillFireDelay_TMP.gameObject.SetActive(false);
        skillFireButton.interactable = true;
    }
    IEnumerator BombZoneButtonFill()
    {
        BombZoneButton.gameObject.SetActive(true);
        SetJoystick.Instance.bombZoneJoystick.gameObject.SetActive(false);

        bombZoneFillImg.fillAmount = 0;
        bombZoneFillValue = 0;
        while (bombZoneFillValue <= 1) 
        {
            bombZoneFillValue += Time.deltaTime*bombZoneFillDelay;
            bombZoneFillImg.fillAmount = bombZoneFillValue;
            yield return null;
        }
    }
}
