﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class LevelSelector : MonoBehaviour
{
    //public GameObject loadingImage;

    public void LoadScene(int level)
    {
        //loadingImage.SetActive(true);
        Application.LoadLevel(level);
    }

    
}
