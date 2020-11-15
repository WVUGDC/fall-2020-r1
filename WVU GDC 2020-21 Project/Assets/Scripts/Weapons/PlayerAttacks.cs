using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public Weapon currentWeapon;

    private float reloadDelay;
    private int bulletsLeftInClip;
    // Start is called before the first frame update
    void Start()
    {
        bulletsLeftInClip = currentWeapon.clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        reloadDelay -= Time.deltaTime;
        if (reloadDelay < -currentWeapon.reloadClipTime)
            bulletsLeftInClip = currentWeapon.clipSize;
        if (reloadDelay < 0 && Input.GetMouseButton(0))
        {
            if (currentWeapon.bulletsPerShot <= 1)
                CreateBullet(Random.Range(-currentWeapon.arc / 2, currentWeapon.arc / 2));
            else
            {
                for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
                    CreateBullet(Mathf.Lerp(-currentWeapon.arc / 2, currentWeapon.arc / 2, ((float)i + .5f) / currentWeapon.bulletsPerShot));
            }
            reloadDelay = 1 / currentWeapon.bulletsPerSecond;
            bulletsLeftInClip--;
            if (currentWeapon.clipSize != 0 && bulletsLeftInClip < 1)
            {
                bulletsLeftInClip = currentWeapon.clipSize;
                reloadDelay += currentWeapon.reloadClipTime;
            }
        }
    }
    void CreateBullet(float angle)
    {
        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, transform.position, GetAngleToMouse(angle));
        bullet.transform.localScale *= currentWeapon.size;
        bullet.GetComponent<Projectile>().SetStats(currentWeapon.range, currentWeapon.bulletSpeed,
            currentWeapon.damage, currentWeapon.color, currentWeapon.sprites, currentWeapon.spriteRate);
    }

    Quaternion GetAngleToMouse(float angleOffset)
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.z = objectPos.z;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = ((Mathf.Atan2(mousePos.y, mousePos.x) + (Mathf.PI / 2))) * Mathf.Rad2Deg + angleOffset;
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
