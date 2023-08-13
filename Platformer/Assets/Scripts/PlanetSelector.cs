using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlanetSelector : MonoBehaviour
{
    public PlanetSelectionEvent OnPlanetSelected;
    public Button buttonLander;

    public void OnPlanetButtonSelected()
    {
        Debug.Log("Планета №" + gameObject.name);
        //OnPlanetSelected.Invoke(gameObject.name, buttonLander);
        if (gameObject.name == "Planet1Button") SceneManager.LoadScene(3);
        if (gameObject.name == "Planet2Button") SceneManager.LoadScene(4);
        if (gameObject.name == "Planet3Button") SceneManager.LoadScene(5);

    }
}

[System.Serializable]
public class PlanetSelectionEvent : UnityEvent<string, Button>
{
}
