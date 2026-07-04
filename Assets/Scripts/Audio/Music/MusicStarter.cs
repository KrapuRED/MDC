using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    public string musicTrack;

    private void Start()
    {
        MusicManager.Instance.PlayMusic(musicTrack);
    }
}
