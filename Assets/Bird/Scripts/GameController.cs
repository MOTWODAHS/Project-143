using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int gameStage;

    private string songString;

    private List<AudioSource> song = new List<AudioSource>();

    public AudioSource[] audioClips;

    public void AddNote(string note)
    {
        songString = songString + note;
        int index = int.Parse(note) - 1;
        song.Add(audioClips[index]);
    }

    [ContextMenu("PlaySong")]
    public void PlaySong()
    {
        StartCoroutine(PlaySongEnum());
    }

    public IEnumerator PlaySongEnum()
    {
        foreach (AudioSource note in song)
        {
            yield return new WaitForSeconds(0.5f);
            note.Play();
        }
    }

}
