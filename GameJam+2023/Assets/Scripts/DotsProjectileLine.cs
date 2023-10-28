using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsProjectileLine : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [Range(0.01f, 1f)][SerializeField] private float size;
    [Range(0.1f, 2f)][SerializeField] private float delta;
    [Range(0f, 1f)][SerializeField] private float alpha;

    private List<Vector2> positionList = new();
    private List<GameObject> dotGameObjectList = new();

    private void FixedUpdate()
    {
        if (positionList.Count > 0)
        {
            DestroyAllDots();
            positionList.Clear();
        }
    }

    private void DestroyAllDots()
    {
        foreach (GameObject dot in dotGameObjectList)
        {
            Destroy(dot);
        }
        dotGameObjectList.Clear();
    }

    private GameObject GetOneDot()
    {
        GameObject newGameObject = new();
        newGameObject.transform.localScale = Vector3.one * size;
        newGameObject.transform.parent = transform;

        SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, alpha);
        spriteRenderer.sprite = sprite;

        return newGameObject;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            positionList.Add(point);
            point += direction * delta;
        }

        Render();
    }

    private void Render()
    {
        foreach (Vector2 position in positionList)
        {
            GameObject newDot = GetOneDot();
            newDot.transform.position = position;
            dotGameObjectList.Add(newDot);
        }
    }
}
