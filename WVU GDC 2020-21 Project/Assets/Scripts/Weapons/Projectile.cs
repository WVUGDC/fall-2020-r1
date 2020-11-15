using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    public SpriteRenderer sr;
    private float speed;
    private Sprite[] sprites;
    private float timeLeft;
    private int currentIndex;
    private float spriteRate;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillMe", 10);
    }

    public void SetStats(float range, float speed, float damage, Color color, Sprite[] sprites, float spriteRate)
    {
        Invoke("KillMe", range/speed);
        this.speed = speed;
        this.damage = damage;
        sr.color = color;
        if (sprites.Length > 0)
            sr.sprite = sprites[0];
        
        this.sprites = sprites;
        if (sprites.Length > 0)
            this.spriteRate = spriteRate;
        else
            this.spriteRate = 0;
        if (GetComponent<ParticleSystem>())
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            var main = ps.main;
            main.startColor = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.up * Time.deltaTime * speed;

        if (spriteRate == 0)
            return;
        if (timeLeft > 0)
        {
            timeLeft -= Time.unscaledDeltaTime;
        }
        else
        {
            currentIndex++;
            if (currentIndex >= sprites.Length)
                currentIndex = 0;
                sr.sprite = sprites[currentIndex];
            timeLeft = 1 / spriteRate;
        }
    }

    void KillMe()
    {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        KillMe();
    }
}
