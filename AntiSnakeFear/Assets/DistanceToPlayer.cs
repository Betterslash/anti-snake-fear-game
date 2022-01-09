using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject snake1;
    public GameObject snake2;
    public AudioSource audioSource;
    private bool _jumped = false;

    // Update is called once per frame
    private void Update()
    {
        var distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        Debug.Log(distance);
        if (!_jumped && distance < 120)
        {
            _jumped = true;
            LaunchSnakes();
        }

        if (audioSource.time > 4.00f)
        {
            audioSource.Stop();
        }
    }

    private void LaunchSnakes()
    {
        snake1.GetComponent<Rigidbody>().velocity = new Vector3(75, 0, 225);
        snake2.GetComponent<Rigidbody>().velocity = new Vector3(-75, 0, 225);
        audioSource.time = 1.70f;
        audioSource.Play();
    }
}