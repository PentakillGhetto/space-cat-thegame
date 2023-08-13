using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent OnActive;

    public void Activate()
    {
        OnActive.Invoke();
    }
}
