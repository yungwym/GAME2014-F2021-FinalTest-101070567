using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{

    public Transform[] waypoints;
    public float moveSpeed;
    public int waypointIndex;

    private Vector3 initialScale;

    private float duration = 2.0f;

    private bool collidingWithPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBetweenPoints();
    }

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShrinkPlatform());
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetPlatform());
        }
    }


    IEnumerator ShrinkPlatform()
    {
        Debug.Log("Shrinking");

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

    IEnumerator ResetPlatform()
    {
        Debug.Log("Restting");

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
