using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse_timer : MonoBehaviour
{
    public float time = 4;

    // Start is called before the first frame update
    
    void Update()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(timer());
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
