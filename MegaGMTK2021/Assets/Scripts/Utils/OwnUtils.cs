using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class OwnUtils
{
    public static TextMeshPro CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize,
        Color color, TextAnchor textAnchor, TextAlignmentOptions textAlignment)
    {
        GameObject gameObject = new GameObject("Text", typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        //textMesh.isOverlay = true;
        return textMesh;
    }

    public static TextMeshPro CreateWorldText(string text, Transform parent = null,
        Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null,
        TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color) color, textAnchor, textAlignment);
    }

    // public static Vector3 GetMouseWorldPosition(Camera camera) {
    //     Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, camera);
    //     vec.z = 0f;
    //     Print(vec);
    //     return vec;
    // }

    public static void Print(Vector3 position)
    {
        Debug.Log(
            "position: " + (position.x).ToString() + " " + (position.y).ToString() + " " + (position.z).ToString());
    }

    public static void Print(string info)
    {
        Debug.Log(info);
    }

    public static Vector3 GetMouseWorldPosition(Camera camera, bool needZ = true)
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = camera.ScreenPointToRay(mouse);
        RaycastHit hit;
        Vector3 vec = new Vector3(-1, -1, -1);
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            vec = hit.point;
            if (!needZ)
                vec.z = 0f;
        }

        //Print(vec);
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}