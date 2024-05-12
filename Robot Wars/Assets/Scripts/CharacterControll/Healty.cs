using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Healty : MonoBehaviour,IDamagable
{
    public float maxHealty;
    public float currentHealty;

    public Transform headCanvas;
    public Slider canvasHealtyBar;
    public Slider headHealtyBar;
    public GameObject myArrowIcon;
    Camera camera;
    CharacterAnimation characterAnimation;
    public ParticleSystem healtyPowerEffect;
    public TextMeshProUGUI healtyValueTextAnim;
    void Start()
    {
        camera = Camera.main;
        characterAnimation=GetComponent<CharacterAnimation>();

        if(GetComponent<PhotonView>().IsMine)
        {
            myArrowIcon.SetActive(false);
            headHealtyBar.gameObject.SetActive(true);
            LeanTween.moveLocalY(myArrowIcon,0.45f,0.2f).setLoopPingPong();
            headHealtyBar.maxValue = maxHealty;
            headHealtyBar.value = currentHealty;
            canvasHealtyBar = UIManager.instance.healtyBar;
            canvasHealtyBar.maxValue = maxHealty;
            canvasHealtyBar.value = currentHealty;

            UIManager.instance.playerName_TMP.text = PhotonNetwork.NickName;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(camera!=null)
        {
            headCanvas.LookAt(camera.transform.position);
        }
    }
    public void Damage(float damageValue)
    {
        float healty = currentHealty;
        LeanTween.value(currentHealty, (healty - damageValue), 0.2f).setOnUpdate((float value) => headHealtyBar.value = value ).setOnComplete(()=>
        {
            currentHealty -= damageValue;
            headHealtyBar.value = currentHealty;
            canvasHealtyBar.value = currentHealty;
        });
        characterAnimation.Damage(true);
        GetDamageAndHealtyTextAnimation(damageValue, Color.red, "-");
    }
    public void  HealtyIncrease()
    {
        float healty = currentHealty;
        LeanTween.value(currentHealty, (healty +50), 0.2f).setOnUpdate((float value) => headHealtyBar.value = value).setOnComplete(() =>
        {
            currentHealty += 50;
            headHealtyBar.value = currentHealty;
            canvasHealtyBar.value = currentHealty;
        });
        healtyPowerEffect.Play();
        GetDamageAndHealtyTextAnimation(50, Color.green, "+");
    }
    void GetDamageAndHealtyTextAnimation(float value,Color textColor,string plusorminus)
    {
        healtyValueTextAnim.transform.localPosition = Vector3.zero;
        healtyValueTextAnim.gameObject.SetActive(true);
        healtyValueTextAnim.text=plusorminus+(int)value;
        healtyValueTextAnim.color= textColor;
        LeanTween.moveLocalY(healtyValueTextAnim.gameObject, 3, 1f).setOnComplete(() => { healtyValueTextAnim.gameObject.SetActive(false); });
    }
    public void GetDamage(float value)
    {
        Damage(value);
    }

    public void GetHealty()
    {
        HealtyIncrease();
    }
}
