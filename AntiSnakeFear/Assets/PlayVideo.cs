using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject player;

    private void Update()
    {
        var distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        Debug.Log(distance);
        if (!videoPlayer.isPlaying && distance < 70)
        {
            videoPlayer.Play();
        }
    }
}