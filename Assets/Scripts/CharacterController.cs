using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;

    public int healthCount;
    public int coinCount;

    public Rigidbody2D rb;
    private Animator animator;

    public GameObject healthtxt;
    public GameObject scoretxt;

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthtxt.GetComponent<Text>().text = "Health: " + healthCount;
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;
        healthtxt.GetComponent<Text>().text = "Health: " + healthCount;
        scoretxt.GetComponent<Text>().text = "Score: " + coinCount;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
        

        }
        else
        {
            animator.SetFloat("xVelocity", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpforce;
            animator.SetTrigger("JumpTrigger");
        }
        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mace")
        {
            healthCount -= 10;
        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coinCount++;
        }
    }
}
