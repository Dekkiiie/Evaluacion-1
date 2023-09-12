using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class Player : MonoBehaviour
{
    public StudioEventEmitter m_audio,m_audio2,m_landing;
    public StudioParameterTrigger t_audio;
    public StudioParameterTrigger t_audio2;
    private Vector2 input;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool canJump2;
    private float saveScale;
     Vector3 oldPos;
    public float totalDistance,wallDistance = 0;

    bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        saveScale = rb.gravityScale;
        InputManager.OnPlayerMovement += Move;
        InputManager.OnPress += Jump;
        
    }

    private void Move(Vector2 input)
    {
        this.input = input;
        if (input.x > 0)
        {
            isPlaying = true;
            
        }
        else 
        {
            isPlaying = false;
            
        }
    }

    private void Jump(bool canJump)
    {
       
        if (canJump == true && canJump2 == true)
        {
            m_audio2.Play();
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
            canJump2 = false;
        }
    }
    private void Update()
    {

        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        totalDistance += distanceThisFrame;
        wallDistance += distanceThisFrame;


        oldPos = transform.position;

        Debug.Log(totalDistance);

        if (isPlaying == true)
        {
            m_audio.Play();
        }else if(isPlaying == false)
        {
            m_audio.Stop();
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(input.x, 0);
        rb.position += velocity * speed * Time.fixedDeltaTime ;

       
        if(rb.velocity.y < -.5f) 
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }

    private void OnEnable()
    {
        InputManager.OnPlayerMovement += Move;
        InputManager.OnPress += Jump;
    }

    private void OnDisable()
    {
        InputManager.OnPlayerMovement -= Move;
        InputManager.OnPress -= Jump;
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Suelo"))
        {
            m_landing.Play();
            rb.gravityScale = 1;
            canJump2 = true;
        }

        if (collision.CompareTag("Muralla"))
        {
            t_audio.TriggerParameters();
          
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Muralla"))
        {
            t_audio2.TriggerParameters();

        }

    }


}
