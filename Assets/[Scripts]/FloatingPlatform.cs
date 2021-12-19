/* File Header - FloatingPlatform.cs
 * 
 * Robert Wymer 
 * Student Number - 101070567 
 * Date Modified - Dec 18, 2021
 * 
 * Description - Handles Shrinking and Expanding of Floating Platform
 * Events of Begin Collision and end Collsion With Player
 * Plays Shrinking and Expanding Sounds in respective events
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed;
    public int waypointIndex;

    private float duration = 2.0f;

    public AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBetweenPoints();
    }


    //Moves Platform between 2 waypoints 
    void MoveBetweenPoints()
    {
        Vector3 targetPos = waypoints[waypointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            if (waypointIndex < waypoints.Length - 1)
                waypointIndex++;
            else
                waypointIndex = 0;
        }
    }

    //Checks for Collision with Player and calls ShrinkPlatform Coroutine
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShrinkPlatform());
        }
    }

    //Checks for Collision with Player and calls ResetPlatform Coroutine
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetPlatform());
        }
    }

    //Shrinking Platform Coroutine - Decreases local scale of the platform over 2 seconds, plays shrinking sound 
    IEnumerator ShrinkPlatform()
    {
        Debug.Log("Shrinking");

        sounds[0].Play();

        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 targetScale = new Vector3(0.0f, 1.0f, 1.0f);

        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        yield return null;
    }

    //Reset Platform Coroutine - Increases local scale of the platform over 2 seconds, plays expanding sound 
    IEnumerator ResetPlatform()
    {
        Debug.Log("Restting");

        sounds[1].Play();

        Vector3 startScale = new Vector3(0.0f, 1.0f, 1.0f);
        Vector3 targetScale = new Vector3(1.0f, 1.0f, 1.0f);

        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        yield return null;
    }
}
