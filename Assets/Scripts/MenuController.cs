using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour, IPointerClickHandler
{
    AudioSource au;
    public AudioClip ButtonClick;

    

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData e)
    {
        if (e.selectedObject.name == "StartButton")
        {
            SceneManager.LoadScene(1);
        }
        else if (e.selectedObject.name == "ExitButton")
        {
            Application.Quit();
        }
        else if (e.selectedObject.name == "IntroduceButton")
        {
            SceneManager.LoadScene(2);
        }
        else if (e.selectedObject.name == "BackToMenuButton")
        {

            SceneManager.LoadScene(0);
        }
        au.PlayOneShot(ButtonClick);
    }

}
