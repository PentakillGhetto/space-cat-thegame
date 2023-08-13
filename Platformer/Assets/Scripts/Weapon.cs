using UnityEngine;

[CreateAssetMenu(fileName = "weapon_name", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float fireRate = 0;
    public Projectile projectile;
}
