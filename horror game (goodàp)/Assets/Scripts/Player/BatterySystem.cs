using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatterySystem : MonoBehaviour
{
    //[SerializeField] private PlayerManager _playerManager;
    [SerializeField] private TextMeshProUGUI batteryText;
    

    // Start is called before the first frame update
    void Start()
    {
        //string shiftingText = string.Empty;
        //for (int i = 0; i < _playerManager.BatteryLevel; i++)
        //{
        //    shiftingText += "I";
        //}
        //Debug.Log(shiftingText);
        //batteryText.text = shiftingText;

        batteryText.text = "IIIIIII";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBatteryLevel(int level)
    {
        string shiftingText = string.Empty;
        for (int i = 0; i < level; i++)
        {
            shiftingText += "I";
        }
        batteryText.text = shiftingText;
    }
}
