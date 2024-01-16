using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public int MoveLevel = 40;
    public int MaxMove = 40;
    public Text movesLeftText;
    private Vector2 startPosition;
    public float moveSpeed = 1f;
    private bool isMoving = false;
    private Vector2 lastPosition;

    private bool hasTouchedKey = false; // Added a variable to track if the player has touched the key

    public MonsterMovement monsterMovement;

    void Start()
    {
        startPosition = transform.position;
        UpdateMovesLeftText();
    }

    void Update()
    {
        if (MaxMove > 0 && !isMoving)
        {
            Vector2 targetPosition = transform.position;

            if (Input.GetKeyDown(KeyCode.W))
            {
                targetPosition += Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                targetPosition += Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                targetPosition += Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                targetPosition += Vector2.right;
            }

            if (targetPosition != (Vector2)transform.position)
            {
                lastPosition = transform.position;
                StartCoroutine(MovePlayer(targetPosition));
            }
        }
        else if (MaxMove <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    IEnumerator MovePlayer(Vector2 targetPosition)
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
        MaxMove--;
        UpdateMovesLeftText();
        isMoving = false;
    }

    void UpdateMovesLeftText()
    {
        if (movesLeftText != null)
        {
            movesLeftText.text = MaxMove.ToString();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            transform.position = lastPosition;
            MaxMove -= 1;
            UpdateMovesLeftText();
        }
        if (collision.gameObject.CompareTag("Block"))
        {
            transform.position = lastPosition;
            UpdateMovesLeftText();
        }

        if (collision.gameObject.CompareTag("Key"))
        {
            hasTouchedKey = true;
            collision.gameObject.SetActive(false); // Hide the key
        }

        if (collision.gameObject.CompareTag("Chest") && hasTouchedKey)
        {
            collision.gameObject.SetActive(false); // Hide the chest
        }
    }

    void ResetGame()
    {
        transform.position = startPosition;
        MaxMove = MoveLevel;
        UpdateMovesLeftText();

        MonsterMovement[] allMonsters = FindObjectsOfType<MonsterMovement>(true);
        foreach (MonsterMovement monster in allMonsters)
        {
            monster.ResetToStartPosition();
        }

        BlockMovement[] allBlock = FindObjectsOfType<BlockMovement>(true);
        foreach (BlockMovement block in allBlock)
        {
            block.ResetToStartPosition();
        }
        KeyMovement[] allKey = FindObjectsOfType<KeyMovement>(true);
        foreach (KeyMovement key in allKey)
        {
            hasTouchedKey = false;
            key.ResetToStartPosition();
        }
        ChestMovement[] allChest = FindObjectsOfType<ChestMovement>(true);
        foreach (ChestMovement chest in allChest)
        {
            chest.ResetToStartPosition();
        }
    }
}
