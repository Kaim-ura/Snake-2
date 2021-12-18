using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    // Reference of Vector2 and assign default direction of Sneak
    private Vector2 direction = Vector2.right;

    // Reference of List contains Transforms
    private List<Transform> listOfSegments;

    // Reference of Sneak segments Transforms
    public Transform segmentPrefab;

    public int initialSize = 4;

    // It will be needed to if statements in key bindings
    public bool horizontal, vertical;
    

    private void Start()
    {
        // let the sneak to turn only up or down because default direction is right
        vertical = true;
        horizontal = false;

        // Initialization of list
        listOfSegments = new List<Transform>();

        // Adding to list head of the sneak
        listOfSegments.Add(transform);

        // Fresh start
        ResetState();
    }
    private void Update()
    {
        // Binding the keys to stering the Sneak and blocking posibility to collide Sneak with own tail
        if (Input.GetKeyDown(KeyCode.UpArrow) && vertical)
        {
            direction = Vector2.up;
            vertical = false;
            horizontal = true;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && vertical)
        {
            direction = Vector2.down;
            vertical = false;
            horizontal = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && horizontal)
        {
            direction = Vector2.left;
            vertical = true;
            horizontal = false;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) && horizontal)
        {
            direction = Vector2.right;
            vertical = true;
            horizontal = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);
        }

        Debug.Log(listOfSegments.Count);
    }

    private void FixedUpdate()
    {
        // This loop starts counting all segments of Sneak, but starting to an end and say to last segment "hey take place of segment before you"
        for (int i = listOfSegments.Count - 1; i > 0; i--)
        {
            listOfSegments[i].position = listOfSegments[i - 1].position;
        }

        // Moving Sneak by using the Vectors from binded directions
        transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }

        else if (collision.tag == "Obstacle")
        {
            // when Sneak collide with own tail or walls reset game
            ResetState();

            SceneManager.LoadScene(2);
        }
    }

    private void ResetState()
    {
        for (int i = 1; i < listOfSegments.Count; i++)
        {
            Destroy(listOfSegments[i].gameObject);
        }

        listOfSegments.Clear();
        listOfSegments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            listOfSegments.Add(Instantiate(segmentPrefab));
        }

        transform.position = Vector2.zero;
    }
    
    // what happen when Sneak eat Food 
    private void Grow()
    {
        // Create object segment form Transform class and assing to this object Instantiate prefab
        Transform segment = Instantiate(segmentPrefab);
        
        // If you create next segment let it position will be the position of older segment
        segment.position = listOfSegments[listOfSegments.Count - 1].position;

        // Adding created segment to list
        listOfSegments.Add(segment);
    }
}
