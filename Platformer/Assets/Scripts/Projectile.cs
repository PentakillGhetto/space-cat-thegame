using UnityEngine;

[CreateAssetMenu(fileName = "projectile_name", menuName = "Projectile")]
public class Projectile : ScriptableObject
{
    [Tooltip("Ссылка на префаб пули.")]
    public Transform projectilePrefab;

    [Tooltip("Ссылка на префаб анимации пули при попадании.")]
    public Transform projectileHitPrefab;

    [Tooltip("Скорость пули.")]
    public int movementSpeed = 70;

    [Tooltip("Урон пули.")]
    public int damage = 20;
}
