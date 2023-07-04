using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float screenPositionFollowThreshold;
    private Vector3 clickedScreenPosition;
    private bool canMove;

    private PlayerSize playerSize;

    // Start is called before the first frame update
    void Start()
    {
        playerSize = GetComponent<PlayerSize>();

        playerSize.onIncreaseMoveSpeed += IncreaseMoveSpeed;

        PlayerTimer.onTimerOver += DisableMovement;

        GameManager.onStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        playerSize.onIncreaseMoveSpeed -= IncreaseMoveSpeed;
        PlayerTimer.onTimerOver -= DisableMovement;
        GameManager.onStateChanged -= GameStateChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            ManageControl();
        }
    }

    private void IncreaseMoveSpeed(float targetScale)
    {
        // REMOVE TARGETSCALE IF OU DONT WANT TO INCREASE THE SIZE TOO
        moveSpeed += targetScale / 10; // Adjust the move speed based on the targetScale
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // First calculate the difference in screen position
            Vector3 difference = Input.mousePosition - clickedScreenPosition;

            Vector3 direction = difference.normalized;

            float maxScreenDistance = screenPositionFollowThreshold * Screen.height;

            if (difference.magnitude > maxScreenDistance)
            {
                clickedScreenPosition = Input.mousePosition - direction * maxScreenDistance;
                difference = Input.mousePosition - clickedScreenPosition;
            }

            difference /= Screen.width;

            difference.z = difference.y;
            difference.y = 0;

            Vector3 TargetPosition = transform.position + difference * moveSpeed * Time.deltaTime;

            transform.position = TargetPosition;
        }
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.GAME:
                EnableMovement();
                break;
        }
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void DisableMovement()
    {
        canMove = false;
    }

}
