using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public Sound sound;

    public void Play(GameObject sender)
    {
        sound.audioSource = sender.GetComponent<AudioSource>();
        if (sound.audioSource == null) sound.audioSource = sender.AddComponent<AudioSource>();

        sound.audioSource.volume = sound.volume;
        sound.audioSource.pitch = sound.pitch;
        sound.audioSource.clip = sound.audioClip;
        sound.audioSource.loop = sound.loop;

        sound.audioSource.Play();
    }

    public void Stop()
    {
        if (sound.audioSource != null) sound.audioSource.Stop();
    }
}
