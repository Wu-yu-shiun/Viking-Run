using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    public AudioClip GetShield;
    public AudioClip GetTenShield;
    public AudioClip MonsterAttack;
    public AudioClip GetHurt;

    AudioSource au;
    Text CoinLabel,MentionLabel;
    string nowCoin;
    Color mentionLabelColor;
    

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        CoinLabel = GameObject.Find("CollectAmount").GetComponent<Text>();
        MentionLabel = GameObject.Find("MentionLabel").GetComponent<Text>();
        nowCoin = CoinLabel.text;
        mentionLabelColor = MentionLabel.color;
    }

    // Update is called once per frame
    void Update()
    {
        if ( nowCoin!=CoinLabel.text)
        {
            au.PlayOneShot(GetShield);
            nowCoin = CoinLabel.text;
        }

        int coinNum = int.Parse(CoinLabel.text);
        if (coinNum % 10 == 0 && coinNum != 0)
        {
            au.PlayOneShot(GetTenShield);
        }

        if (MentionLabel.text == "Monster catch you!" || MentionLabel.text == "You fell off a cliff!")
        {
            au.PlayOneShot(MonsterAttack);
        } 

        if (mentionLabelColor!=MentionLabel.color)
        {
            if (MentionLabel.color == Color.red)
            {
                au.PlayOneShot(GetHurt);
            }
            mentionLabelColor = MentionLabel.color;
        }

        

    }

  


}
