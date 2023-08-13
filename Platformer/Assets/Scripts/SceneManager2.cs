using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
{
    public static SceneManager2 Instance;
    public int menuSceneIndex;
    public int gameSceneIndex;
    public int starShipSceneIndex;
    public int planet1 = 3;
    public int planet2 = 4;
    public int planet3 = 5;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
        DontDestroyOnLoad(this);

        // получше ничего я придумать не мог
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                //AudioManager.Instance.StopSound("Theme Ending");
                //AudioManager.Instance.PlaySound(this.gameObject, "Theme Opening");

                break;
            case 1:
                //AudioManager.Instance.StopSound("Theme Opening");
                //AudioManager.Instance.PlaySound(this.gameObject, "Theme 1");
                //Instance.canvasGroup.interactable = false;
                break;
            case 2:
                //AudioManager.Instance.StopSound("Theme 1");
                //AudioManager.Instance.PlaySound(this.gameObject, "Theme Ending");
                break;
        }
    }

    /// <summary>
    /// Загрузка сцены главного меню.
    /// </summary>
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(menuSceneIndex);
    }

    /// <summary>
    /// Завершить работу приложения.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Запуск/Перезапуск текущей сцены.
    /// </summary>
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Запуск игровой сцены.
    /// </summary>
    public void LoadGameScene()
    {
        //UIManager.Instance.Transition();
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void LoadShipScene()
    {
        //UIManager.Instance.Transition();
        SceneManager.LoadScene(starShipSceneIndex);
    }

    public void LoadPlanetScene()
    {
        //Debug.Log("загружена планета: " + UIManager.Instance.planetName);
        LoadPlanetScene(UIManager.Instance.planetName);
    }

    public void LoadPlanetScene(string planetName)
    {
        if (planetName.Equals("Planet1Button")) LoadPlanetScene(planet1);
        if (planetName.Equals("Planet2Button")) LoadPlanetScene(planet2);
        if (planetName.Equals("Planet3Button")) LoadPlanetScene(planet3);
    }

    public void LoadPlanetScene(int index)
    {
        Debug.Log("загружаем сцену с индексом " + index);
        //UIManager.Instance.Transition();
        SceneManager.LoadScene(index);
    }
}
