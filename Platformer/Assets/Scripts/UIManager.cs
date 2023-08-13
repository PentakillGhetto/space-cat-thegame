using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image fadeImage;
    public Image healthBar;
    public Image itemsScore;
    [HideInInspector] public int currentScore;
    [HideInInspector] public string planetName;
    [Tooltip("Ссылка на префаб карты")]
    public GameObject map;

    void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void OnPlanetSelected(string planetName, UnityEngine.UI.Button buttonLander)
    {
        this.planetName = planetName;
        buttonLander.interactable = true;
    }

    public void OnPlayerImpacted(float currentHealth)
    {
        healthBar.fillAmount = currentHealth;
    }

    public void OnPartCollected()
    {
        itemsScore.fillAmount = currentScore / 3;
    }

    public IEnumerator Transition()
    {
        float alphaColor = Mathf.Lerp(fadeImage.color.a, 1, Time.deltaTime * 2f);
        fadeImage.color = new Color(0, 0, 0, alphaColor);
        if (alphaColor <= 0.99)
        {
            yield return new WaitForSeconds(0.1f);
            Transition();
        }
        yield return null;
    }

    public void ToggleMap()
    {
        if (map.activeSelf)
            map.SetActive(false);
        else
            map.SetActive(true);
    }
}
