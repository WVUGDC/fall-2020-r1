using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public GameObject bulletPrefab;
    public float fireSpeed;
    public float f;
}
