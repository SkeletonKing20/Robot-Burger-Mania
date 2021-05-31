using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Vector2 healthBarFull = new Vector2(8.4f, 0.54f);
    Vector2 currentHealthBar;
    Player player;
    SpriteRenderer spriteR;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.drawMode = SpriteDrawMode.Sliced;
        spriteR.size = healthBarFull;
    }

    private void FixedUpdate()
    {
        currentHealthBar = new Vector2(healthBarFull.x * (player.currentHp / player.maxHp), healthBarFull.y);
        spriteR.size = currentHealthBar;
    }
}
