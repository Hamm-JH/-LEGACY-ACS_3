using UnityEngine;
[RequireComponent(typeof(AudioSource))]


public class ButtonControl : MonoBehaviour
{
    private Vector3 startPos;
    
    private AudioSource sound;

    private bool soundOn = false;

    void Start()
    {
        startPos = transform.position;
       
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
      /*  if (Input.GetMouseButtonDown(0))
        {
            sound.Play();
           
            transform.position = startPos;
        }

        else
        {
            sound.Stop();
            
        }
*/ 
        switch(soundOn)
        {
            case true :
             if(Input.GetMouseButtonDown(0))
             {
                 sound.Stop();
                 soundOn = false;
             }
             break;

             case false :
             if(Input.GetMouseButtonDown(0)){
                 sound.Play();
                 soundOn = true;
             }
             break;
        }

        // gameobject.SetActive(true) = !gameobject.SetActive(true);(toggle 전환)

    }
}
