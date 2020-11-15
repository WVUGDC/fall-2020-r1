using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public GameObject bulletPrefab;

    [Header("Visual Settings")]
    [Tooltip("List of sprites that will be cycled through")]
    public Sprite[] sprites;
    [Tooltip("Number of animation frames per second")]
    [Range(0, 60)]
    public float spriteRate = 20;
    [Tooltip("Color modifier of sprite(s). Leave as white to use base color of sprites")]
    public Color color = Color.white;
    [Tooltip("Scale modifier of bullet")]
    [Range(0, 10)]
    public float size = 1;

    [Header("Core Stats")]
    [Tooltip("Speed of the bullet")]
    [Range(0, 30)]
    public float bulletSpeed = 5;
    [Tooltip("Number of bullets fired per second")]
    [Range(0, 100)]
    public float bulletsPerSecond = 2;
    [Tooltip("Damage of bullets")]
    [Range(0, 200)]
    public float damage = 10;
    [Tooltip("Range of bullet")]
    [Range(0, 20)]
    public float range = 8;
    
    [Header("Optional Stats")]
    [Tooltip("Size of clip (use clips to create bursts of bullets). If set to 0, clips will not be used")]
    [Range(0, 300)]
    public int clipSize = 0;
    [Tooltip("Time (in seconds) for reload")]
    [Range(0, 5)]
    public float reloadClipTime = 1;
    [Tooltip("Spread of bullets (in degrees). Spread is random unless bulletsPerShot > 1")]
    [Range(0, 360)]
    public float arc = 5;
    [Tooltip("Bullets fired per shot. If greater than 1, bullets will be evenly spaced across arc")]
    [Range(1, 20)]
    public int bulletsPerShot = 1;

    private void OnEnable()
    {
        bulletPrefab = Resources.Load<GameObject>("Default/Bullet");
    }
}
