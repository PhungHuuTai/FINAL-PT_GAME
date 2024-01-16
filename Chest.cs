using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of the monster's movement
    private Vector2 lastPlayerPosition; // To store the last position of the player
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save the starting position
    }
    public void ResetToStartPosition()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
}

