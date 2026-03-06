using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private const string PLAYER = "Player";
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    [SerializeField] private float minDropItem = 1f;
    [SerializeField] private float maxDropItem = 1.5f;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();
        Slot originalSlot = originalParent.GetComponent<Slot>();


        // Raycast to the slot
        if (dropSlot != null)
        {
            // Has Item in this slot
            if (dropSlot.currentItem != null)
            {
                // Swap
                dropSlot.currentItem.transform.SetParent(originalParent.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            }
            else
            {
                originalSlot.currentItem = null;

            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            // if the item in the panel return the parent position
            // if the item without the panel drop it
            if (!IsWithinInventory(eventData.position))
            {
                DropItem(originalSlot);
            }
            else
            {
                transform.SetParent(originalParent.transform);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private bool IsWithinInventory(Vector2 mousePosition)
    {
        RectTransform inventoryRectTransform = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRectTransform, mousePosition);
    }

    private void DropItem(Slot currentSlot)
    {
        Transform playerPosition = GameObject.FindGameObjectWithTag(PLAYER)?.transform;
        if (playerPosition == null)
        {
            Debug.LogError("Missing player!!!");
            return;
        }

        // the range drop item
        Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropItem, maxDropItem);
        Vector2 dropPosition = (Vector2)playerPosition.position + dropOffset;

        currentSlot.currentItem = null;
        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        dropItem.GetComponent<BounceEffect>().StartBounce();

        Destroy(gameObject);
    }
}
