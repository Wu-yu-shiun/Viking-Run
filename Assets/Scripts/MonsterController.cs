using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    bool monsterAttack,move;
    float runningSpeed = 5f;
    Animator an;
    Text MentionLabel;

    // Start is called before the first frame update
    void Start()
    {
        monsterAttack = false;
        move= false;
        an = GetComponent<Animator>();
        MentionLabel = GameObject.Find("MentionLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        an.SetBool("isAttack", monsterAttack);
        if(MentionLabel.text== "Monster catch you!"|| MentionLabel.text == "You fell off a cliff!")
        {
            if (!move) transform.position = GameObject.Find("viking").transform.position;
            move = true;
            monsterAttack = true;

        }
    }
}
