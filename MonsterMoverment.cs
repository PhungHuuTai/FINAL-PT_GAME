using UnityEngine;
using System.Collections;

public class MonsterMovement : MonoBehaviour
{   
    public float moveSpeed = 1f; // Speed of the monster's movement
    private Vector2 lastPlayerPosition; // To store the last position of the player
    private bool isMoving = false; // To check if the monster is already moving
    private Vector2 startPosition;
    // This method can be called by the PlayerMovement script to update the monster's target position

    void Start()
    {
        startPosition = transform.position; // Save the starting position
    }

    public void MoveInDirection(Vector2 playerDirection)
    {
        if (!isMoving)
        {
            Vector2 targetPosition = (Vector2)transform.position + playerDirection;
            StartCoroutine(MoveMonster(targetPosition));
        }
    }

    IEnumerator MoveMonster(Vector2 targetPosition)
    {
        isMoving = true;
        float time = 0;

        while (time < 1f)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, time);
            time += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            gameObject.SetActive(false); // Destroy the monster when it hits a wall
        }
    }

    public void ResetToStartPosition()
    {
        transform.position = startPosition; // Reset position
        gameObject.SetActive(true); // Make monster reappear if it was inactive
    }
}
    