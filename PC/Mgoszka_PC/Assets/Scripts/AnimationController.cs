using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject player;

    public Animator playerAni;

    private float xAxi;
    private float yAxi;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(opozniacz());
    }

    IEnumerator opozniacz()
    {
        float x;
        float y;

        xAxi = player.transform.position.x;
        yAxi = player.transform.position.y;

        yield return new WaitForSeconds(0.02f);

        x = xAxi - player.transform.position.x;
        
        y = yAxi - player.transform.position.y;
       
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
                playerAni.SetInteger("transition", 3);
            }
            else
            {
                playerAni.SetInteger("transition", 2);
            }
        }
        else if(x2 < y2)
        {
            if (y < 0)
            {
                playerAni.SetInteger("transition", 4);
            }
            else
            {
                playerAni.SetInteger("transition", 1);
            }
        }
        else
        {
            playerAni.SetInteger("transition", 0);
        }
        StartCoroutine(opozniacz());
    }
    
}
