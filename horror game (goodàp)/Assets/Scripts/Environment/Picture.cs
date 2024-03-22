using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    public void TouchPicture()
    {
    }
    public void DeletePicture()
    {
        PlayerManager.Instance.PictureCollected();
        Destroy(gameObject);
    }
}
