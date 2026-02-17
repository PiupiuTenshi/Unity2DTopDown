using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] pages;
    private void Awake()
    {
        ActivePage(0);

        for (int i = 0; i < pages.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() =>
            {
                ActivePage(index);
            });
        }
    }

    private void ActivePage(int tabNum)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            buttons[i].image.color = Color.grey;
        }
        pages[tabNum].SetActive(true);
        buttons[tabNum].image.color = Color.white;
    }
}
