using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VersionController : MonoBehaviour
{
    public string ThisVersion;
    [Space(20)]
    public GameObject newVerObj;
    public Text aboutText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetVer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetVer()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://drive.google.com/uc?export=download&id=1kd5vYCmiKc2w_8Aijw4wx781xtDIfTS-");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            if(www.downloadHandler.text != ThisVersion)
            {
                newVerObj.SetActive(true);
            }
            aboutText.text += "\n" + "<size=40>" + "Ta gra ma wersje: <color=white>" + ThisVersion + "</color></size>";
            
        }
    }
}
