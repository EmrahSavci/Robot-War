using CharacterMove;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI playerName_TMP;
    [Header("Speed UI")]
    [SerializeField] Button speedEffectBtn;
    [SerializeField] Slider speedSliderBar;
    [SerializeField] GameObject energyIcon;
    [SerializeField] Transform energyIconParent;
    [SerializeField] ParticleSystem energyUIEffect;

    [Space(20)]
    [Header("Healty UI")]
    public Slider healtyBar;
    [SerializeField] GameObject healIcon;
    [SerializeField] ParticleSystem healUIEffect;
    [SerializeField] GameObject hpValueText;

    [Space(20)]
    [Header("Coin UI")]
    [SerializeField] int coinAmount;
    [SerializeField] TextMeshProUGUI coinAmount_TMP;
    [SerializeField] GameObject coinUIPrefab;
    [SerializeField] Transform coinUIPrefabParent;


    public BombZone bombZone;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinAmount_TMP.text=coinAmount.ToString();
    }
    #region SPEED_EFFECT_BUTTON
    public void SpeedEffectFillAmount()
    {
        float currentSliderValue = speedSliderBar.value;
        float nextSliderValue = currentSliderValue + 0.2f;
        LeanTween.value(currentSliderValue,nextSliderValue,0.2f).setOnUpdate((float value)=>speedSliderBar.value = value);
       // speedSliderBar.value += 0.2f;
        if(nextSliderValue >= 1)
        {
            speedEffectBtn.interactable = true;
        }
    }
    public void Run()
    {
        
        CharacterMoveControll.instance.MoveSpeedIncrease();
    }
    public void SpeedImgFillAmounDecrease()
    {
        speedSliderBar.value -=Time.deltaTime*0.1f;
        if(speedSliderBar.value <= 0 )
        {
            speedEffectBtn.interactable =false;
            CharacterMoveControll.instance.MoveSpeedDecrease();
            
        }
    }
    public void EnergyIconSpawn(Vector3 playerPos)
    {
        GameObject _energyIcon=Instantiate(energyIcon,playerPos,Quaternion.Euler(0,0,-20), energyIconParent);
        LeanTween.moveLocal(_energyIcon, Vector3.zero,0.3f).setOnComplete(()=>
        {
            energyUIEffect.Play();
            _energyIcon.SetActive(false);
            LeanTween.scale(energyIconParent.gameObject, Vector3.one * 1.2f, 0.2f).setOnComplete(()=> LeanTween.scale(energyIconParent.gameObject, Vector3.one, 0.2f));
            SpeedEffectFillAmount();
        });

    }
    #endregion
    #region COÝN
    public void CoinIconSpawn(Vector3 playerPos)
    {
        GameObject _coinIcon = Instantiate(coinUIPrefab, playerPos, Quaternion.Euler(0, 0, -20), coinUIPrefabParent);
        LeanTween.moveLocal(_coinIcon, Vector3.zero, 0.3f).setOnComplete(() =>
        {
            coinAmount++;
            coinAmount_TMP.text = coinAmount.ToString();
            _coinIcon.SetActive(false);
            LeanTween.scale(coinUIPrefabParent.gameObject, Vector3.one * 1.2f, 0.2f).setOnComplete(() => LeanTween.scale(coinUIPrefabParent.gameObject, Vector3.one, 0.2f));
            
        });
    }
    #endregion

}
