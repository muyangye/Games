using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConvertToRelativePosition : MonoBehaviour {

    private const string menuPath = "Utilities/RectTransform/";

    // Convert a UI element's pixel offsets into anchor-relative position values.
    [MenuItem(menuPath + "Make RectTransform Use Relative Position")]
    static void Convert() {
        RectTransform rt = Selection.activeTransform.GetComponent<RectTransform>();
        ConvertRectTransform(rt);
    }

    private static void ConvertRectTransform(RectTransform rt) {

        RectTransform parentRt = rt.parent.GetComponent<RectTransform>();
        Vector2 anchorMin = rt.anchorMin;
        Vector2 anchorMax = rt.anchorMax;
        Vector2 offsetMin = rt.offsetMin;
        Vector2 offsetMax = rt.offsetMax;
        Rect rect = parentRt.rect;

        // Left
        anchorMin.x += (offsetMin.x / rect.width);
        offsetMin.x = 0.0f;

        // Right
        anchorMax.x += (offsetMax.x / rect.width);
        offsetMax.x = 0.0f;

        // Top
        anchorMax.y += (offsetMax.y / rect.height);
        offsetMax.y = 0.0f;

        // Bottom
        anchorMin.y += (offsetMin.y / rect.height);
        offsetMin.y = 0.0f;

        // Set the values.
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = offsetMin;
        rt.offsetMax = offsetMax;

    }

    // Validate the menu item, so that it will be deactivated if the user doesn't have
    // an appropriate UI element selected.
    [MenuItem(menuPath + "Make RectTransform Use Relative Position", true, 500)]
    static bool ValidateConvert() {
        return ValidateMenuItems();
    }

    // Convert a UI element's anchor-relative position values into pixel offsets.
    [MenuItem(menuPath + "Make RectTransform Use Absolute Position")]
    static void Reverse() {

        RectTransform rt = Selection.activeTransform.GetComponent<RectTransform>();
        RectTransform parentRt = Selection.activeTransform.parent.GetComponent<RectTransform>();
        Vector2 anchorMin = rt.anchorMin;
        Vector2 anchorMax = rt.anchorMax;
        Vector2 offsetMin = rt.offsetMin;
        Vector2 offsetMax = rt.offsetMax;
        Rect rect = parentRt.rect;

        // Left
        offsetMin.x += (anchorMin.x * rect.width);
        anchorMin.x = 0.0f;

        // Right
        offsetMax.x += (anchorMax.x * rect.width);
        anchorMax.x = 0.0f;

        // Top
        offsetMax.y += (anchorMax.y * rect.height);
        anchorMax.y = 0.0f;

        // Bottom
        offsetMin.y += (anchorMin.y * rect.height);
        anchorMin.y = 0.0f;

        // Set the values.
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = offsetMin;
        rt.offsetMax = offsetMax;

    }

    // Validate the menu item, so that it will be deactivated if the user doesn't have
    // an appropriate UI element selected.
    [MenuItem(menuPath + "Make RectTransform Use Absolute Position", true, 501)]
    static bool ValidateReverse() {
        return ValidateMenuItems();
    }

    // These conversion functions require a RectTransform with a RectTransform parent.
    static bool ValidateMenuItems() {

        if (Selection.activeTransform != null) {
            if (Selection.activeTransform.GetComponent<RectTransform>() != null) {
                if (Selection.activeTransform.parent != null) {
                    if (Selection.activeTransform.parent.GetComponent<RectTransform>() != null) {
                        return true;
                    }
                }
            }
        }

        return false;

    }

    // Convert the pixel offsets of all UI elements in a Canvas to anchor-relative position values.
    [MenuItem(menuPath + "Make Canvas Use Only Relative Positions")]
    static void ConvertCanvas() {

        Canvas canvas = Selection.activeTransform.GetComponent<Canvas>();
        List<RectTransform> allRectTransforms = new List<RectTransform>();

        RecursivelyAdd(allRectTransforms, canvas.transform);
        
        foreach (RectTransform rt in allRectTransforms) {
            if (rt.parent.GetComponent<RectTransform>() != null) {
                ConvertRectTransform(rt);
            }
        }

    }

    private static void RecursivelyAdd(List<RectTransform> list, Transform t) {

        RectTransform rtObj;
        foreach (Transform child in t) {

            rtObj = child.GetComponent<RectTransform>();
            if (rtObj != null) {
                list.Add(rtObj);
            }

            RecursivelyAdd(list, child);

        }

    }

    // Validate the menu item, so that it will be deactivated if the user doesn't have
    // an appropriate UI element selected.
    [MenuItem(menuPath + "Make Canvas Use Only Relative Positions", true, 600)]
    static bool ValidateConvertCanvas() {

        if (Selection.activeTransform != null) {
            if (Selection.activeTransform.GetComponent<Canvas>() != null) {
                return true;
            }
        }

        return false;

    }

}
