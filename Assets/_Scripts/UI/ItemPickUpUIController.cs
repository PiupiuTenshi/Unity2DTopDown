using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUpUIController : MonoBehaviour
{
    public static ItemPickUpUIController Instance { get; private set; }
    private static string ITEM_ICON = "ItemIcon";

    [SerializeField] private GameObject popUpPrefab;
    private int maxPopUp = 5;
    private float popUpDuration = 3f;

    private readonly Queue<GameObject> activePopUp = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowItemPopUp(string itemName, Sprite itemIcon)
    {
        GameObject newPopUp = Instantiate(popUpPrefab, transform);
        newPopUp.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopUp.transform.Find(ITEM_ICON)?.GetComponent<Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }

        activePopUp.Enqueue(newPopUp);
        if (activePopUp.Count > maxPopUp)
        {
            Destroy(activePopUp.Dequeue());
        }

        //Fade out and Destroy
        StartCoroutine(FadeOutAndDestroy(newPopUp));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popUp)
    {
        yield return new WaitForSeconds(popUpDuration);
        if (popUp == null) yield return null;

        CanvasGroup canvasGroup = popUp.GetComponent<CanvasGroup>();
        for (float timePassed = 0; timePassed < popUpDuration; timePassed += Time.deltaTime)
        {
            if (popUp == null) yield break;
            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }
        Destroy(popUp);
    }
}
