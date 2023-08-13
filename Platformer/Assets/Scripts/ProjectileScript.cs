using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Projectile projectile;

    private void Start()
    {
        // после того, как была инстанциирована пуля, уничтожить ее через 1 секунду
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        // двигать пулю туда, куда смотрит игрок, умножив на скорость
        transform.Translate(Vector3.right * Time.deltaTime * projectile.movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision with " + collision.collider.name);

        // создаем ссылку на противника(если игрок действительно попал по нему)
        Creature2DScript creature = collision.collider.GetComponent<Creature2DScript>();

        // проверяем, не пустая ли она, т.е. попал ли игрок
        if (creature != null)
        {
            creature.Impact(projectile.damage);
            //Debug.Log("Hit enemy " + creature.name);
        }

        // проигрываем анимацию
        StartProjectileHitAnimation(collision.transform);

        // уничтожаем пулю
        Destroy(gameObject);
    }

    private void StartProjectileHitAnimation(Transform objectTransform)
    {
        // создать экземпляр анимации пули 
        // с текущими параметрами самой пули (местоположение и ротация)
        // и вернуть ссылку сюда
        Transform bulletHitInstance = Instantiate(projectile.projectileHitPrefab, gameObject.transform.position, gameObject.transform.rotation);
        //AudioManager.Instance.PlaySound(bulletHitInstance.gameObject, "PlasmaRifle Hit");

        // удалить ее через ~секунду
        // можно варьировать значение в зависимости оттого, как долго проигрывается анимация
        Destroy(bulletHitInstance.gameObject, 0.7f);
    }
}
