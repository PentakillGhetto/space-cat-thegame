using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Weapon weapon;
    private float timeBeforeFire = 0;

    public void Shoot()
    {
        // если оружие полуавтоматическое
        if (weapon.fireRate == 0)
        {
            //AudioManager.Instance.PlaySound(this.gameObject, "PlasmaRifle Shot");
            Instantiate(weapon.projectile.projectilePrefab, transform.position, transform.rotation);
        }
        // если автоматическое
        else if (Time.time > timeBeforeFire)
        {
            timeBeforeFire = Time.time + 1 / weapon.fireRate;
            //AudioManager.Instance.PlaySound(this.gameObject, "PlasmaRifle Shot");
            Instantiate(weapon.projectile.projectilePrefab, transform.position, transform.rotation);
        }
    }
}
