using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public void TouchPicture()
    {
        PlayerManager.Instance.PictureCollected();

    }
    public void DeletePicture()
    {
        Destroy(gameObject);
    }
}
