using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Reference of grid, which is need to create borders of spawning food
    public BoxCollider2D gridArea;

    private void Start()
    {
        // Randomize position of food at the begining of the game
        RandomizePosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Randomize position of food when it collide with object which have tag "Player"
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }
    private void RandomizePosition()
    {
        // Create new object "bounds" from Bounds class and assign borders of grid to bounds object
        Bounds bounds = this.gridArea.bounds;

        // Randomize x and y  values following the bounds borders
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Changing the position
        transform.position = new Vector2(Mathf.Round(x), Mathf.Round(y));
    }
}
