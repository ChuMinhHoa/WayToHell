using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHomming : MonoBehaviour
{
    public AnimationCurve positionCurve, noiseCurve;
    public Vector2 targetPoint, startPoint;
    public Vector2 horizontalVector;
    public float noisePosition;
    public Vector2 MinNoise = new Vector2(-1f, -1f);
    public Vector2 MaxNoise = new Vector2(1f, 1);
    public float time;
    public float speed;
    public float offset;
    private void Start()
    {
        time = 0f;
        startPoint = transform.position;
        Vector2 direction = targetPoint - startPoint;
        horizontalVector = Vector2.Perpendicular(direction);
    }
    private void FixedUpdate()
    {
        if (time<5)
        {
            noisePosition = noiseCurve.Evaluate(time);
            transform.position = Vector3.Lerp(startPoint, targetPoint, positionCurve.Evaluate(time)) + new Vector3(noisePosition*horizontalVector.x* offset, noisePosition* horizontalVector.y* offset);
            time += Time.deltaTime * speed;
        }
    }
}
