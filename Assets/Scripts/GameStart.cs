using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    bool timerstart = false;
    Text TimeLabel;
    Text WarningLabel;
    GameObject Warning;

    float timepast = -5;
    int gametimemin = 0;
    int gametimesec = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(GameObject.Find("Icon"));
            timerstart = true;
            Warning = GameObject.Find("WarningLabel");
            TimeLabel = GameObject.Find("TimeAmount").GetComponent<Text>();
            WarningLabel = GameObject.Find("WarningLabel").GetComponentInChildren<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerstart)
        {
            timepast += Time.deltaTime;
            if (timepast > -4 && timepast<-3) WarningLabel.text = "3";         
            else if (timepast > -3 && timepast < -2) WarningLabel.text = "2";
            else if (timepast > -2 && timepast < -1) WarningLabel.text = "1";
            else if (timepast > -1 && timepast < 0) WarningLabel.text = "Go!";
            else if(timepast>0)
            {
                if((int)timepast==0)Warning.SetActive(false);
                gametimemin = (int)timepast / 60;
                gametimesec = (int)timepast % 60;
                if (gametimesec < 10)
                {
                    TimeLabel.text = $"{gametimemin}:0{gametimesec}";
                }
                else
                {
                    TimeLabel.text = $"{gametimemin}:{gametimesec}";
                }
            }
            
            
        }

    }
}
