/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    #region
    /*** Variables ***/
    [Header("General Settings")]
    public GameObject paddle;
    private int score;
    public Text scoreTxt;
    [Header("Ball Settings")]
    public int numBalls;
    public Text ballTxt; 
    public Vector3 initForce;
    public float speed;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isInPlay;
    #endregion



    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();
    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = "Score: " + score;
        ballTxt.text = "Balls: " + numBalls;
        if (!isInPlay)
        {
            Vector3 curPos = this.transform.position;
            curPos.x = paddle.transform.position.x;
            this.transform.position = curPos;
        }
        if(Input.GetKeyDown(KeyCode.Space) && !isInPlay)
        {
            isInPlay = true;
            Move();
        }
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            Vector3 normalizedVel = rb.velocity.normalized;
            rb.velocity = (speed * normalizedVel);
        }

    }//end LateUpdate()

    private void Move()
    {
        rb.AddForce(initForce);
    }

    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    private void OnCollisionEnter(Collision other)
    {
        audioSource.Play();
        GameObject otherObj = other.gameObject;
        if(otherObj.tag == "Brick")
        {
            score += 100;
            Destroy(otherObj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "OutBounds")
        {
            numBalls--;
            if (numBalls > 0)
            {
                Invoke("SetStartingPos", 2.0f);
            }
        }
    }




}
