using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class VikingController : MonoBehaviour
{
    Rigidbody rb;
    Animator an;
    Text TimeLabel,CoinLabel,WarningLabel,MentionLabel;
    GameObject Warning,Sensor;

    float timepast = -5, nextJumpTime, endTime, hurtTime;
    float jumpCD = 1.5f, runningSpeed = 5f;
    int coin, hurt;

    bool isJumping, isHurt, gameOver, mySwitch;
    bool timeToChooseLR, timeToLeft, timeToRight, timeToTurn, turnLeft, turnRight;

    // Start is called before the first frame update
    void Start()
    {
        coin = 0;
        hurt = 0;
        isJumping = false; isHurt = false; gameOver = false; mySwitch = false; 
        timeToChooseLR = false; timeToTurn = false; timeToLeft = false; timeToRight = false;
        turnLeft = false; turnRight = false;

        rb = GetComponent<Rigidbody>();
        an = GetComponent<Animator>();
        Warning = GameObject.Find("WarningLabel");
        TimeLabel = GameObject.Find("TimeAmount").GetComponent<Text>();
        CoinLabel = GameObject.Find("CollectAmount").GetComponent<Text>();
        WarningLabel = GameObject.Find("WarningLabel").GetComponentInChildren<Text>();
        MentionLabel = GameObject.Find("MentionLabel").GetComponent<Text>();
        MentionLabel.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        //time
        timepast += Time.deltaTime;
        //hurt recover
        if (timepast > hurtTime + 10)
        {
            hurt = 0;
            MentionLabel.text = "You are safe!";
            MentionLabel.color = Color.green;
        }
        //gameover and back to menu in 3sec
        if (gameOver && timepast > endTime + 5)
        {
            SceneManager.LoadScene(0);
        }
        //run
        if (!Warning.activeSelf)
        {
            transform.localPosition += runningSpeed * Time.deltaTime * transform.forward;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping && an.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            isJumping = true;
            rb.velocity = new Vector3(0, 7, 0);
            nextJumpTime = timepast + jumpCD;
        }
        if (timepast >= nextJumpTime)
        {
            isJumping = false;
        }
        //left and right(fixed)
        if (timeToChooseLR)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                turnLeft = true;
                turnRight = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                turnLeft = false;
                turnRight = true;
            }
        }
        if (timeToLeft && Input.GetKeyDown(KeyCode.LeftArrow)) turnLeft = true;
        if (timeToRight && Input.GetKeyDown(KeyCode.RightArrow)) turnRight = true;
        if (timeToTurn)
        {
            if (turnLeft)
            {
                Debug.Log("turn left");
                transform.localEulerAngles -= new Vector3(0, 90, 0);
                turnLeft = false;
                Sensor.SetActive(false);
            }
            else if (turnRight)
            {
                Debug.Log("turn right");
                transform.localEulerAngles += new Vector3(0, 90, 0);
                turnRight = false;
                Sensor.SetActive(false);
            }
            timeToTurn = false;
        }
        //left and right(customized)
        if (mySwitch)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localPosition -= runningSpeed * Time.deltaTime * transform.right;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localPosition += runningSpeed * Time.deltaTime * transform.right;
            }
        }
        //hurt and go back
        AnimatorStateInfo info = an.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Hurt"))
        {
            if (!mySwitch) transform.localPosition -= runningSpeed * 3 * Time.deltaTime * transform.forward;
            else transform.localPosition -= runningSpeed * 2 * Time.deltaTime * transform.forward;
            isHurt = false;
        }

        //animation
        an.SetBool("isRun", !Warning.activeSelf);
        an.SetBool("isJump", isJumping);
        an.SetBool("isHurt", isHurt);
        

    }

    //sensors
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "L/R sensor")
        {
            timeToChooseLR = true;
            Sensor = collider.gameObject;
            MentionLabel.text = "Choose left/right";
        }
        else if (collider.name == "L sensor")
        {
            timeToLeft = true;
            Sensor = collider.gameObject;
            MentionLabel.text = "Turn left";
        }
        else if (collider.name == "R sensor")
        {
            timeToRight = true;
            Sensor = collider.gameObject;
            MentionLabel.text = "Turn right";
        }
        else if (collider.name == "hurt sensor")
        {
            Debug.Log("hurt");
            isHurt = true;
            hurt++;
            if (hurt == 1)
            {

                MentionLabel.text = "Monster is approaching!";
                MentionLabel.color = Color.red;
                hurtTime = timepast;
            }
            else Die("monster");
        }
        else if (collider.name == "die sensor") Die("flop");

        else if (collider.name == "turn sensor")
        {
            transform.localEulerAngles += new Vector3(0, 180, 0);
            transform.localPosition += runningSpeed * 80 * Time.deltaTime * transform.forward;
        }
        else if (collider.name == "switch sensor")
        {
            if (mySwitch)
            {
                mySwitch = false;
                Debug.Log("switch off");
            }
            else
            {
                mySwitch = true;
                Debug.Log("switch on");
            }
        }
        else if (collider.name == "Collectable")
        {
            collider.gameObject.SetActive(false);
            coin++;
            CoinLabel.text = $"{coin}";
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        timeToChooseLR = false;
        timeToLeft = false;
        timeToRight = false;
        timeToTurn = true;
    }

    private void Die(string cause)
    {
        Debug.Log("die");
        if(cause=="monster") MentionLabel.text = "Monster catch you!";
        else MentionLabel.text = "You fell off a cliff!";
        //show point
        Warning.SetActive(true);
        string[] tmp = TimeLabel.text.Split(':');
        int a = int.Parse(tmp[0]) * 60 + int.Parse(tmp[1]);
        int b = int.Parse(CoinLabel.text);
        WarningLabel.text = $"You survive {TimeLabel.text} and get {b} shields!\n" +
            $"Your total point: {a + b}";
        gameOver = true;
        endTime = timepast;
    }
}