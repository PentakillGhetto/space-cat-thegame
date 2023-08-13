using System.Collections;
using UnityEngine;

public class ShipItemsCollector : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnAllPartsCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Collectible"))
        {
            StartCoroutine(CollectItem(collision, .1f));
            UIManager.Instance.currentScore += 1;
        }

        if (UIManager.Instance.currentScore == 3 && collision.transform.CompareTag("Player"))
        {
            OnAllPartsCollected.Invoke();
            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator CollectItem(Collider2D collision, float value)
    {
        yield return new WaitForSeconds(value);
        if (collision.gameObject != null)
            Destroy(collision.gameObject);
    }
}
