using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public static UIHandler Instance { set; get; }

    public Slider fuelSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

}
