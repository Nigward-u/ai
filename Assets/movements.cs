using UnityEngine;

public class movements: MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Apply movement to the player
        transform.Translate(movement);
    }
}
