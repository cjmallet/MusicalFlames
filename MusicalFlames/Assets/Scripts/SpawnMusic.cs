using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns music notes when the corresponding candle is lit
/// </summary>
public class SpawnMusic : MonoBehaviour
{
    [SerializeField]
    private GameObject musicNote;

    [SerializeField]
    private Transform spawnPosition;

    private GameObject spawnedMusicNote;

    public void SpawnMusicNote()
    {
        spawnedMusicNote= Instantiate(musicNote, spawnPosition.position,spawnPosition.rotation,null);
    }

    public void DeleteMusicNote()
    {
        Destroy(spawnedMusicNote);
    }
}
