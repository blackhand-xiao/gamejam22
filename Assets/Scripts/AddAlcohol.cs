using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;

public class AddAlcohol : MonoBehaviour
{
    public ExcelDataManager excelDataManager;
    public Cup cup;
    public ChangeGlassColor ChangeGlassColor;
    public AlcoholPourer Pourer;

    public int sweet;
    public int intensity;
    public int mellow;
    public int ID;

    bool buttonDown;
    bool addAlcohol;
    public float switchSpeedTime;
    float speed=1;

    private NoRepeatRandom nrr;
    private Vector2 originalPosition;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.Find("Slider").TryGetComponent<Slider>(out slider);
        transform.parent.Find("CupDetails").TryGetComponent<Cup>(out cup);
        sweet = GetAlcohol().sweet;
        intensity = GetAlcohol().intensity;
        mellow = GetAlcohol().mellow;
        nrr = new NoRepeatRandom(1000);
        originalPosition = ((RectTransform) transform).anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        switchSpeedTime += Time.deltaTime;
        if (switchSpeedTime >= 1)
        {
            speed = nrr.Next() * 2.5f;
            switchSpeedTime = 0;
        }
        
        if (buttonDown && addAlcohol)
        {
            slider.value += Time.deltaTime* speed;
        }
        else if (buttonDown && !addAlcohol)
        {
            slider.value -= Time.deltaTime* speed;
        }
        if (slider.value == 1)
        {
            addAlcohol = false;
        }else if (slider.value == 0)
        {
            addAlcohol= true;
        }
    }
    
    public void OnClickAddAlcoholButton(bool down)
    {
        buttonDown = down;
        if (down)
        {
            slider.value = 0;
            //Debug.Log("加酒");
            Debug.Log(ID);
        }else
        {
            //Debug.Log("停止加酒,加了:"+slider.value);
            cup.sweet += slider.value * sweet;
            cup.intensity += slider.value * intensity;
            cup.mellow += slider.value * mellow;
            
            ChangeGlassColor.Add(ID, (int)Mathf.Ceil(slider.value * 3f));
        }
    }

    public void Reset()
    {
        EndPourAlcohol();
    }

    public void AddToGlass(float sliderValue)
    {
        cup.sweet += sliderValue * sweet;
        cup.intensity += sliderValue  * intensity;
        cup.mellow += sliderValue  * mellow;
            
        ChangeGlassColor.Add(ID, (int)Mathf.Ceil(slider.value * 3f));
        EndPourAlcohol();
    }

    public void StartPourAlcohol()
    {
        ((RectTransform) transform).anchoredPosition = new Vector2(-72f, 8.1f);
        transform.rotation = Quaternion.Euler(0f, 0f, -95f);
        if (Pourer.CallbackAlcohol != null&& Pourer.CallbackAlcohol!=this)
        {
            Pourer.Reset();
        }

        Pourer.StartTry(this);
    }

    public void EndPourAlcohol()
    {
        ((RectTransform) transform).anchoredPosition = originalPosition;
        transform.rotation = Quaternion.identity;
    }

    public AlcoholItem GetAlcohol()
    {
        switch (gameObject.name)
        {
            case "Votka":
                return excelDataManager.AlcoholItem[2];
            case "Whisky":
                return excelDataManager.AlcoholItem[0];
            case "rum":
                return excelDataManager.AlcoholItem[1];
            case "liqueur":
                return excelDataManager.AlcoholItem[3];
            case "barandy":
                return excelDataManager.AlcoholItem[4];
            case "力娇酒":
                return excelDataManager.AlcoholItem[5];
            default :
                return excelDataManager.AlcoholItem[0];

        }
        
    }

}
