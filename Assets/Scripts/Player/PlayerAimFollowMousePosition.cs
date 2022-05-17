using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimFollowMousePosition : MonoBehaviour
{
    public GameObject myAim;
    public SpriteRenderer avatarSpriteRenderer;
    Vector2 mousePos;
    public float angle;
    private void FixedUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myAim.transform.eulerAngles = new Vector3(0, 0, angle);
        FlipCheck();
    }
    void FlipCheck() {
        if ((avatarSpriteRenderer.flipX && mousePos.x > transform.position.x) ||
            (!avatarSpriteRenderer.flipX && mousePos.x < transform.position.x))
        {
            avatarSpriteRenderer.flipX = !avatarSpriteRenderer.flipX;
            Transform myAimTransform = myAim.transform;
            Vector3 scale = myAimTransform.localScale;
            scale.y *= -1;
            myAimTransform.localScale = scale;
        }
    }
}
