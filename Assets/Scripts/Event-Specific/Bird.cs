using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2.5f;
    Vector2 movement = new Vector2(-1, 0);

    public List<GameObject> birds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //check if we're returning to this room
        if (PlayerPrefs.HasKey("Data_Birds"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Data_Birds", 1);

            StartCoroutine(StaggerBirds());
        }
    }
    IEnumerator StaggerBirds()
    {
        GetComponent<AudioSource>().Play();

        foreach(GameObject b in birds)
        {
            b.SetActive(false);
        }

        int index = 0;
        while(index < birds.Count)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            birds[index].SetActive(true);
            index++;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
