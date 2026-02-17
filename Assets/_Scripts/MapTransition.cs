using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D mapBoundM1;
    [SerializeField] private PolygonCollider2D mapBoundM2;

    [SerializeField] private Direction directionM1;
    [SerializeField] private Direction directionM2;
    private CinemachineConfiner cinemachineConfiner;

    private void Awake()
    {
        cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (cinemachineConfiner.m_BoundingShape2D == mapBoundM1)
            {
                cinemachineConfiner.m_BoundingShape2D = mapBoundM2;
                FlashTransition(collision.gameObject, directionM1);
            }
            else
            {
                cinemachineConfiner.m_BoundingShape2D = mapBoundM1;
                FlashTransition(collision.gameObject, directionM2);
            }
            cinemachineConfiner.InvalidatePathCache();
        }
    }

    private void FlashTransition(GameObject player, Direction direction)
    {
        Vector3 flashTransition = player.transform.position;
        switch (direction)
        {
            case Direction.Up:
                flashTransition.y += 2;
                break;
            case Direction.Down:
                flashTransition.y -= 2;
                break;
            case Direction.Left:
                flashTransition.x -= 2;
                break;
            case Direction.Right:
                flashTransition.x += 2;
                break;
        }

        player.transform.position = flashTransition;
    }
}
