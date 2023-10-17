using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float timeToMove = 0.2f;

    private Vector2 originalPosition, targetPosition;
    private bool isMoving;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = GameInput.Instance.GetMovementVectorNormalized();

        if (moveInput != Vector2.zero && !isMoving) StartCoroutine(Move(moveInput));
    }

    private IEnumerator Move(Vector2 direction)
    {
        isMoving = true;

        float elapsedTime = 0f;
        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;


        isMoving = false;
    }
}
