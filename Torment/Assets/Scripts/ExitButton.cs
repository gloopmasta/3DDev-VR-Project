using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGameButton()
    {
        Debug.Log("Quit the application");
        Application.Quit();
    }
}
