using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    public GameObject Landscape0; //original
    public GameObject Landscape1; //intersection(left/right)
                                  //next1->rotation:y+270 next2->rotation:y+90
    public GameObject Landscape2; //stone(jump)
    public GameObject Landscape3; //up & down stair
    public GameObject Landscape4; //bridge
    public GameObject Landscape5; //special(multiple control)
    public GameObject Landscape6; //hole(jump)
    public GameObject Landscape7; //intersection(right)
                                  //rotation:y+90
    public GameObject Landscape8; //intersection(left)
                                  //rotation:y+270
    GameObject nextprintStart;
    Vector3 nowVikingAngle;
    int count,num,turn;
    float RotationY;
    string leftname,rightname;
    bool printNewLand;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        turn = 0;
        printNewLand = false;
        Vector3 nowVikingAngle = transform.localEulerAngles;
        leftname = "landscape5";
        rightname = "landscape7";
    }

    // Update is called once per frame
    void Update()
    {
        num = 0;
        if (transform.localEulerAngles != nowVikingAngle)
        {
            turn++;
            printNewLand = true;
            if ((nowVikingAngle.y + 90f) % 360f == transform.localEulerAngles.y) //turn right
            {
                nextprintStart = GameObject.Find(rightname);
            }
            else if ((nowVikingAngle.y + 270f) % 360f == transform.localEulerAngles.y) // turn left
            {
                nextprintStart = GameObject.Find(leftname);
            }
        }

        if (turn%2==1 && printNewLand)
        {
            while (num < 10)
            {
                //landscape type
                GameObject LandscapeType = Landscape0;
                int random = Random.Range(1, 6);
                if (num == 3) LandscapeType = Landscape1;
                else if (num == 6) LandscapeType = Landscape7;
                else if (num == 7) LandscapeType = Landscape8;
                else
                {
                    if(random==1) LandscapeType = Landscape0;
                    else if (random == 2) LandscapeType = Landscape2;
                    else if (random == 3) LandscapeType = Landscape4;
                    else if (random == 4) LandscapeType = Landscape5;
                    else if (random == 5) LandscapeType = Landscape6;
                }

                //landscape loc
                Vector3 LandscapeLoc;
                if (num == 0) LandscapeLoc = nextprintStart.transform.GetChild(1).position;
                else if (num == 5) LandscapeLoc = GameObject.Find($"clone{count}-3").transform.GetChild(2).position;
                else if (num == 6) LandscapeLoc = GameObject.Find($"clone{count}-4").transform.GetChild(1).position;
                else if (num == 7) LandscapeLoc = GameObject.Find($"clone{count}-5").transform.GetChild(1).position;
                else if (num == 8) LandscapeLoc = GameObject.Find($"clone{count}-6").transform.GetChild(1).position;
                else if (num == 9) LandscapeLoc = GameObject.Find($"clone{count}-7").transform.GetChild(1).position;
                else LandscapeLoc = GameObject.Find($"clone{count}-{num - 1}").transform.GetChild(1).position;

                //landscape rotation
                Quaternion Landscaperotation;
                if (num == 4 || num == 6) RotationY = 0f;
                else if (num == 5 || num == 7) RotationY = 180f;
                else RotationY = 90f;
                Landscaperotation = Quaternion.Euler(0f, RotationY, 0f);

                GameObject newLandscape = Instantiate(LandscapeType, LandscapeLoc, Landscaperotation);
                newLandscape.name = $"clone{count}-{num}";
                num++;
                
            }
            rightname=$"clone{count}-9";
            leftname = $"clone{count}-8";
            count++;
            printNewLand = false;
        }
        nowVikingAngle = transform.localEulerAngles;
    }
}
