using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Creature2DScript))]
public class SimpleAI : MonoBehaviour
{
    public Vector2 leftPoint;
    public Vector2 rightPoint;
    public LayerMask layerMask;
    private Creature2DScript creature;
    [Range(0.001f, 5f)]
    public float attackSpeed;
    [Range(0, 100)]
    public int damage;
    private float timeBeforeAttack;

    private void Start()
    {
        creature = GetComponent<Creature2DScript>();
        StartCoroutine(Searhing());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // зацепили коллайдер
        Creature2DScript player = collision.CompareTag("Player") ? collision.gameObject.GetComponent<Creature2DScript>() : null;

        // игрок и можем атаковать
        if (player != null && timeBeforeAttack < Time.time)
        {
            // следующее время атаки текущее + кд
            timeBeforeAttack = Time.time + 1 / attackSpeed;
            player.Impact(damage);
        }
    }

    private IEnumerator Searhing()
    {
        Vector2 v1 = new Vector2(transform.position.x - leftPoint.x, transform.position.y);
        Vector2 v2 = new Vector2(transform.position.x + rightPoint.x, transform.position.y);

        RaycastHit2D raycast = Physics2D.Linecast(v1, v2, layerMask.value);

        Debug.DrawLine(v1, v2, Color.cyan);
        if (raycast.collider != null)
        {
            //Debug.Log("collider not null");
            Debug.DrawLine(v1, v2, Color.yellow);
            if (raycast.collider.tag.Equals("Player"))
            {

                Debug.DrawLine(v1, v2, Color.red);
                //Debug.Log("collider player");
                creature.Move(
                    new Vector2(
                    raycast.collider.GetComponent<Creature2DScript>().transform.position.x
                    -
                    transform.position.x, 0.0f).normalized);
            }
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Searhing());
    }
}
