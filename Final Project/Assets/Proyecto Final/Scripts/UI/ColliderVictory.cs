using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ColliderVictory : MonoBehaviour
{
    public VideoPlayer victoryVideo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            victoryVideo.Play();
        }
    }
}
