using System.Collections;
using UnityEngine;

public class ActiveFalse_timer : MonoBehaviour
{
    public float time = 4;
    
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
