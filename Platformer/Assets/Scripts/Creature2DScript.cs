using UnityEngine;
using UnityEngine.Events;

public class ImpactEvent : UnityEvent<float>
{
}

[RequireComponent(typeof(Rigidbody2D))]
public class Creature2DScript : MonoBehaviour
{
    /// <summary>
    /// Ссылка на ассет существа.
    /// </summary>
    public Creature2D creature;

    /// <summary>
    /// Ссылка на объект в руках
    /// </summary>
    public GameObject handObject;

    /// <summary>
    /// Слои предметов
    /// </summary>
    public LayerMask layersWhatIsItem;

    /// <summary>
    /// Активируемые слои
    /// </summary>
    public LayerMask layersWhatIsActivatable;

    /// <summary>
    /// Слои оружия
    /// </summary>
    public LayerMask layersWhatIsWeapon;

    public Animator animator;

    /// <summary>
    /// Возвращает true, если смотрит вправо и false, если нет.
    /// </summary>
    public bool IsFacingRight { get; protected set; }

    /// <summary>
    /// Находится ли существо на поверхности.
    /// </summary>
    public bool IsGrounded { get; private set; }

    /// <summary>
    /// Возвращает true, если игрок прыгнул, т.е нажат пробел.
    /// </summary>
    private bool isJumped = false;

    [SerializeField]
    public int currentHealth;

    [SerializeField]
    [Tooltip("Радиус 'просмотра' вокруг игрока")]
    public float radius = 0.18f;

    public UnityEvent OnCreatureEliminated;

    public ImpactEvent OnCreatureImpacted;

    /// <summary>
    /// Сжимает значение здоровья до числа между 0 и 1
    /// </summary>
    public int CurrentHealthClamped { get { return currentHealth; } set { currentHealth = Mathf.Clamp(value, 0, creature.maxHealth); } }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - 0.7f), radius);
    }

    private void Start()
    {
        currentHealth = creature.maxHealth;
        IsFacingRight = true;
        IsGrounded = false;
    }

    private void FixedUpdate()
    {
        // если стоим, то цикл изменит это значение
        IsGrounded = false;

        // OverlapCircleAll это массив коллайдеров, которые находятся в радиусе DPRadius объекта "ног" DownSidePosition
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(creature.groundPosition, creature.GroundCheckRadius, creature.layersWhatIsGround))
            IsGrounded = (collider.gameObject != gameObject ? true : IsGrounded);

            // после того, как игрок (может быть) прыгнул, снова обнуляем поле
            isJumped = false;
    }

    protected void Flip()
    {
        // смотрю направо = смотрю налево и наоборот
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// Наносим урон
    /// </summary>
    public void Impact(int damage)
    {
        currentHealth -= damage;
        OnCreatureImpacted.Invoke(CurrentHealthClamped);
        if (currentHealth <= 0) Eliminate();
    }

    /// <summary>
    /// Вызывает встроенный метод Destroy, уничтожающий существо.
    /// </summary>
    public void Eliminate()
    {
        OnCreatureEliminated.Invoke();
        Destroy(gameObject);
    }

    public void Move(Vector2 direction)
    {
        if (IsGrounded || creature.AirControlled)
        {
            // к ригидбади(физический компонент) добавить силу с вектором
            GetComponent<Rigidbody2D>().velocity = direction * creature.maxSpeed;

            //animator.SetFloat("Speed", GetComponent<Rigidbody2D>().velocity.magnitude);
            
            animator.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.magnitude));

            // двигаюсь вправо и смотрю налево
            if (direction.x > 0 && !IsFacingRight)
            {
                Flip();
            }
            // двигаюсь влево и смотрю вправо
            else if (direction.x < 0 && IsFacingRight)
            {
                Flip();
            }
        }
    }

    public void Jump()
    {
        if (!IsGrounded || isJumped) return;
        isJumped = true;
        GetComponent<Rigidbody2D>().AddForce(transform.up * creature.jumpForce, ForceMode2D.Force);
    }

    public void Jet()
    {
        animator.SetBool("isJump", true);
        GetComponent<Rigidbody2D>().AddForce(transform.up * creature.jumpForce, ForceMode2D.Force);
    }

    public void DisableJet()
    {
        animator.SetBool("isJump", false);
    }

    /// <summary>
    /// Выбросить предмет из рук
    /// </summary>
    public void DropItem()
    {
        if (handObject != null)
        {
            handObject.GetComponent<Collider2D>().enabled = true;
            handObject.transform.parent = null;
            handObject.GetComponent<Rigidbody2D>().isKinematic = false;
            if (IsFacingRight)
            {
                handObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0.5f, 0.5f, 0f));
            }
            else
            {
                handObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-0.5f, 0.5f, 0f));
            }
            handObject = null;
        }
    }

    /// <summary>
    /// Взять предмет в руки
    /// </summary>
    public void PickItem()
    {
        //Массив предметов вокруг игрока
        Collider2D[] collidersItems = Physics2D.OverlapCircleAll
        (
            creature.groundPosition, radius, layersWhatIsItem
        );

        //Массив активируемых объектов вокруг игрока
        Collider2D[] collidersActivatable = Physics2D.OverlapCircleAll
        (
            creature.groundPosition, radius, layersWhatIsActivatable
        );

        //Массив оружия вокруг игрока
        Collider2D[] collidersWeapons = Physics2D.OverlapCircleAll
        (
            creature.groundPosition, radius, layersWhatIsWeapon
        );


        //Если в руках ничего нет, и вокруг игрока есть предметы
        //то берем в руки
        if (handObject == null && collidersItems.Length != 0)
        {
            handObject = collidersItems[0].gameObject;
            handObject.transform.parent = transform;
            handObject.transform.position = transform.Find("itemSlot").position;
            handObject.GetComponent<Rigidbody2D>().isKinematic = true;
            handObject.GetComponent<Collider2D>().enabled = false;
        }

        //Если в руках ничего нет, и вокруг игрока есть оружие
        //то берем в руки
        if (handObject == null && collidersWeapons.Length != 0)
        {
            handObject = collidersWeapons[0].gameObject;
            handObject.transform.parent = transform;
            handObject.transform.position = transform.Find("weaponSlot").position;
            handObject.GetComponent<Rigidbody2D>().isKinematic = true;
            handObject.GetComponent<Collider2D>().enabled = false;
        }

        if (collidersActivatable.Length != 0)
        {
            Debug.Log(collidersActivatable[0].name);
            collidersActivatable[0].GetComponent<Activator>().Activate();
        }
    }
}
