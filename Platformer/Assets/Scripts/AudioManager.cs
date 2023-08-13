using UnityEngine;

/// <summary>
/// Сингл-тон версия аудиоменеджера, отвечающая за воспроизведение и остановку той или иной звуковой дорожки.
/// </summary>
[System.Serializable]
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Возвращает синглтон экземпляр текущего аудиоменеджера.
    /// </summary>
    public static AudioManager Instance { get; private set; }

    // массив звуков
    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        // если при запуске проекта или сцены менеджер отсутствует
        // возвращается ссылка на созданный юнити менеджер
        if (Instance == null) Instance = this;

        // если ссылка указывает на объект
        // и этот объект не текущий аудиоменеджер
        else if (Instance != this)
            // удаляется не this, а gameObject, который прикреплен к менеджеру, что вызовет повторный запуск Awake()
            // *хз на самом деле, но иных вариков я не вижу
            // с последующим созданием менеджера
            Destroy(gameObject);

        // не удалять менеджер при перезапуске сцены
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < sounds.Length; i++)
        {
            // создаем объект GameObject, на который будем добавлять все остальные компоненты
            GameObject gameObject = new GameObject("Sound " + i + " " + sounds[i].Name);
            // прикрепляем его к менеджеру
            gameObject.transform.SetParent(this.transform);
        }
    }

    /// <summary>
    /// Позволяет проиграть звуковую дорожку с указанным именем. 
    /// </summary>
    /// <param name="Название дорожки"></param>
    public void PlaySound(GameObject sender, string name)
    {
        // поиск звука в массиве
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].Name == name)
            {
                //var v = sender.GetComponent<SoundScript>();
                //sounds[i].Play(sender);
                return;
            }
        }
        Debug.Log("AUDIO MANAGER: Звук не найден. " + name);
    }

    /// <summary>
    /// Позволяет остановить звуковую дорожку с указанным именем. 
    /// </summary>
    /// <param name="Название дорожки"></param>
    public void StopSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].Name == name)
            {
                //sounds[i].Stop();
                return;
            }
        }
        Debug.LogError("AUDIO MANAGER: Звук не найден. " + name);
    }
}



