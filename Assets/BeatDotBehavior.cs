using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatDotBehavior : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition.x < -1500)
        {
            Destroy(gameObject);
        }

        Vector3 currentPosition = rectTransform.anchoredPosition;
        currentPosition.x -= speed * Time.deltaTime;

        // Assign the new position back to the transform
        rectTransform.anchoredPosition = currentPosition;
    }
}
