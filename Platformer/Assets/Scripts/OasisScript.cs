using UnityEngine;
using UnityEngine.Events;

public class OasisScript : MonoBehaviour
{
    public UnityEvent OnOasisReached;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) OnOasisReached.Invoke();
    }
}
