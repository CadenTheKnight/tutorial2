using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    private int scoreVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreVal.ToString();
        winText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement*speed, verMovement*speed));
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Coin"){
            Destroy(collision.collider.gameObject);
            scoreVal++;
            score.text = scoreVal.ToString();
            if(scoreVal >= 3){
                winText.text = "Congratulations! You Win!\n- Caden Henderson";
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.collider.tag == "Floor"){
            if(Input.GetKey(KeyCode.W)){
                rd2d.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            }
        }
    }
}
