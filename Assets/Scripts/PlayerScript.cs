using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text winText;
    private int scoreVal = 0;
    private int livesVal = 3;
    public float jumpPower;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;
    private bool airborne = false;
    private int privateState = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreVal.ToString();
        lives.text = "Lives: " + livesVal.ToString();
        winText.text = "";

        musicSource.clip = musicClipOne;
        musicSource.Play();

        anim = GetComponent<Animator>();
    }

    void Update(){
        
        if(airborne){
            anim.SetInteger("State", 5);
            //jump anim
        } else {
            if (Input.GetKeyDown(KeyCode.D))
            {
            privateState = 2;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            //running right

            if (Input.GetKeyDown(KeyCode.A))
            {
            privateState = 3;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            //running left

        if (Input.GetKeyUp(KeyCode.D))
            {
            privateState = 0;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            //idle right

            if (Input.GetKeyUp(KeyCode.A))
            {
            privateState = 0;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            //idle left
            anim.SetInteger("State", privateState);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        //float verMovement = Input.GetAxis("Vertical");

        //rd2d.AddForce(new Vector2(hozMovement*speed, verMovement*speed));
        rd2d.AddForce(new Vector2(hozMovement*speed, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Coin"){
            Destroy(collision.collider.gameObject);
            scoreVal++;
            score.text = scoreVal.ToString();
            if(scoreVal == 4){
                transform.position = new Vector2(53.0f, 0.5f);
                livesVal = 3;
                lives.text = "Lives: " + livesVal.ToString();
            }
            if(scoreVal >= 8){
                winText.text = "Congratulations! You Win!\n- Caden Henderson";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                Destroy(this);
                
            }
        }
        if(collision.collider.tag == "Enemy"){
            Destroy(collision.collider.gameObject);
            livesVal--;
            lives.text = "Lives: " + livesVal.ToString();
            if(livesVal <= 0){
                winText.text = "You lose!";
                Destroy(this);
            }
        }
    }

/*    private void OnTriggerEnter(Collider other){
        
    }*/

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.collider.tag == "Floor"){
            if(Input.GetKey(KeyCode.W)){
                rd2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            }
            airborne = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.collider.tag == "Floor"){
            airborne = true;
        }
    }


    

     

}
