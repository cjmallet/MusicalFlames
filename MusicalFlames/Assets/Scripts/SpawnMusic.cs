using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMusic : MonoBehaviour
{
    [SerializeField]
    private GameObject musicNote;

    [SerializeField]
    private Transform spawnPosition;

    private GameObject spawnedMusicNote;

    public void SpawnMusicNote()
    {
        spawnedMusicNote=GameObject.Instantiate(musicNote, spawnPosition,true);
    }

    public void DeleteMusicNote()
    {
        Destroy(spawnedMusicNote);
    }
}
