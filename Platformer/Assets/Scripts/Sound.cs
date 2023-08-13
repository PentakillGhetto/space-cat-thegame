using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Класс Звука содержит в себе аудиодорожку и необходимые для ее воспроизведения элементы.
/// </summary>
[CreateAssetMenu(fileName = "sound_name", menuName = "Sound")]
public class Sound : ScriptableObject
{
    /// <summary>
    /// Возвращает название текущей дорожки.
    /// </summary>
    public string Name { get { return audioClip.name; } set { audioClip.name = value; } }

    /// <summary>
    /// Возвращает объект типа AudioClip - текущую дорожку.
    /// </summary>
    public AudioClip audioClip;

    /// <summary>
    /// Громкость аудиодорожки.
    /// </summary>
    [Range(0f, 1f)]
    public float volume = 0.7f;

    /// <summary>
    /// Высота звука ауидиодорожки. 
    /// </summary>
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    /// <summary>
    /// Возвращает true, если текущая дорожка зациклена.
    /// </summary>
    public bool loop = false;

    /// <summary>
    /// Место, откуда будет исходить звук.
    /// </summary>
    [HideInInspector]
    public AudioSource audioSource;

    /// <summary>
    /// Канал звука.
    /// </summary> 
    public AudioMixerGroup mixerGroup;
}
