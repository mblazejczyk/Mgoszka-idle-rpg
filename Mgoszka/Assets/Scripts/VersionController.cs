using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VersionController : MonoBehaviour
{
    public string ThisVersion;
    [Space(20)]
    public GameObject newVerObj;
    public Text aboutText;

    void Start()
    {
        StartCoroutine(GetVer());
    }

    IEnumerator GetVer()
    {
        TranslationSystem TranslationObject = GameObject.FindGameObjectWithTag("controller").GetComponent<TranslationSystem>();
        UnityWebRequest www = UnityWebRequest.Get("https://drive.google.com/uc?export=download&id=1CCYhjwYagJo4vgGX8ZYtcwOwH917-IEr");
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
            aboutText.text += "\n" + "<size=40>" + TranslationObject.GetText(23) + "<color=white>" + ThisVersion + "</color></size>";
            
        }
    }
}
