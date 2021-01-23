using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;

    bool iswalking = false;
    bool isonground = true;

    public int healthCount;
    public int coinCount;

    public Rigidbody2D rb;
    private Animator animator;

    public GameObject healthtxt;
    public GameObject scoretxt;

    public AudioClip[] AudioClipArr;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            iswalking = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
            iswalking = true;

        }
        else
        {
            animator.SetFloat("xVelocity", 0);
            iswalking = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isonground == true)
        {      
            vVelocity = jumpforce;
            animator.SetTrigger("JumpTrigger");
            audioSource.PlayOneShot(AudioClipArr[2]);
            isonground = false;
        }

        if(iswalking == true && isonground == true)

        {
            audioSource.clip = AudioClipArr[0];

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }     
        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mace")
        {
            audioSource.PlayOneShot(AudioClipArr[1]);
            healthCount -= 10;
        }
        if (collision.gameObject.tag == "Coin")
        {
            audioSource.PlayOneShot(AudioClipArr[3]);
            Destroy(collision.gameObject);
            coinCount++;
        }
        if(collision.gameObject.tag == "Ground")
        {
            isonground = true;
        }
    }
}
