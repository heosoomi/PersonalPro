using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragLineDrawer : MonoBehaviour
{
    public RectTransform lineImage;
    public RectTransform canvasRect;
    private Vector2 dragStartPos;
    private bool dragging = false;
    private Coroutine fadeCoroutine;

    public void StartDrag(Vector2 screenPos)
    {
        dragging = true;
        dragStartPos = screenPos;
        if (lineImage != null)
        {
            lineImage.gameObject.SetActive(true);
            var img = lineImage.GetComponent<Image>();
            if (img) img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
        }
        UpdateLine(screenPos);
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
    }

    public void DragTo(Vector2 screenPos)
    {
        if (!dragging) return;
        UpdateLine(screenPos);
    }

    public void EndDrag()
    {
        dragging = false;
        fadeCoroutine = StartCoroutine(FadeOutLine());
    }

    private IEnumerator FadeOutLine()
    {
        var img = lineImage.GetComponent<Image>();
        float t = 0f;
        Color start = img.color;
        Color end = new Color(start.r, start.g, start.b, 0f);
        float duration = 0.4f;
        while (t < duration)
        {
            t += Time.deltaTime;
            img.color = Color.Lerp(start, end, t / duration);
            yield return null;
        }
        img.color = end;
        lineImage.gameObject.SetActive(false);
        fadeCoroutine = null;
    }

    private void UpdateLine(Vector2 currentScreenPos)
    {
        Vector2 startLocalPos, endLocalPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, dragStartPos, null, out startLocalPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, currentScreenPos, null, out endLocalPos);

        Vector2 dir = endLocalPos - startLocalPos;
        float length = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        lineImage.anchoredPosition = startLocalPos;
        lineImage.sizeDelta = new Vector2(length, lineImage.sizeDelta.y);
        lineImage.rotation = Quaternion.Euler(0, 0, angle);
    }
}

