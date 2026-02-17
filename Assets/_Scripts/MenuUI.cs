using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    private void Start()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnTabAction += GameInput_OnTabAction;
        }

        if(menuCanvas != null)
            menuCanvas.SetActive(false);
    }

    private void GameInput_OnTabAction(object sender, EventArgs e)
    {
        if (menuCanvas != null)
        {
            bool isMenuOpen = !menuCanvas.activeSelf;
            menuCanvas.SetActive(isMenuOpen);
        }
    }

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnTabAction -= GameInput_OnTabAction;
        }
    }
}
