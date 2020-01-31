using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(horizontal);
        float vertical = Input.GetAxis("Vertical");
        Debug.Log(vertical);
        Vector2 position = rb2D.position;
        position.x = position.x + 5.0f * horizontal * Time.deltaTime;
        position.y = position.y + 5.0f * vertical * Time.deltaTime;
        rb2D.position = position;
    }
}
