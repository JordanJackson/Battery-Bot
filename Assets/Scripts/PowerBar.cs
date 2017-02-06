using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour 
{
    Player player;
    IntData powerData;
    Slider powerSlider;
    Text powerText;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        IntData[] datas = player.GetComponents<IntData>();
        powerSlider = GetComponent<Slider>();
        powerText = GetComponentInChildren<Text>();

        foreach (IntData d in datas)
        {
            if (d.Name == "Power")
            {
                powerData = d;
                break;
            }
        }

        if (powerData != null && powerSlider != null)
        {
            powerSlider.minValue = 0;
            powerSlider.maxValue = player.maxPower;
        }
    }

    private void Update()
    {
        if (powerData != null && powerSlider != null)
        {
            powerSlider.value = powerData.data;
        }
        if (powerData != null && powerText != null)
        {
            powerText.text = powerData.data.ToString();
        }
    }
}
