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
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetStats(float range, float speed, float damage, Color color, Sprite[] sprites, float spriteRate)
    {
        StartCoroutine(StartFade(range/speed));
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
            particles = GetComponent<ParticleSystem>();
            var main = particles.main;
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

    IEnumerator StartFade(float time) 
    {
        for (float t = 0; t < 1; t += Time.deltaTime/(time*.75f))
            yield return null;
        Color startColor = sr.color;
        Color endColor = startColor;
        endColor.a = .2f;
        for (float t = 0; t < 1; t += Time.deltaTime / (time * .25f))
        {
            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        KillMe();
    }
    void KillMe()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        if (particles)
            particles.Stop();
        Destroy(this.gameObject, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Collider2D>().enabled = false;
        KillMe();
    }
}
