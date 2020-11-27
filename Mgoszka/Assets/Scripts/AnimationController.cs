using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject player;

    public Animator playerAni;

    private float xAxi;
    private float yAxi;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(opozniacz());
    }

    // Update is called once per frame
    
    IEnumerator opozniacz()
    {
        float x;
        float y;

        xAxi = player.transform.position.x;
        yAxi = player.transform.position.y;

        yield return new WaitForSeconds(0.02f);

        
        
        x = xAxi - player.transform.position.x;
        
        y = yAxi - player.transform.position.y;
       

        //Debug.Log(x + " " + y + " " + xAxi + " " + yAxi + " " + player.transform.position.x + " " + player.transform.position.y);

        float x2;
        float y2;

        if (x < 0)
        {
            x2 = 0 - x;
        }
        else
        {
            x2 = x;
        }

        if (y < 0)
        {
            y2 = 0 - y;
        }
        else
        {
            y2 = y;
        }

        if (x2 > y2)
        {
            if (x < 0)
            {
                //Debug.Log("prawo");
                playerAni.SetInteger("transition", 3);
            }
            else
            {
                //Debug.Log("lewo");
                playerAni.SetInteger("transition", 2);
            }
        }
        else if(x2 < y2)
        {
            if (y < 0)
            {
                //Debug.Log("góra");
                playerAni.SetInteger("transition", 4);
            }
            else
            {
                //Debug.Log("dół");
                playerAni.SetInteger("transition", 1);
            }
        }
        else
        {
            //Debug.Log("stoi");
            playerAni.SetInteger("transition", 0);
        }
        //Debug.Log(x + " " + y);
        StartCoroutine(opozniacz());
    }
    
}
