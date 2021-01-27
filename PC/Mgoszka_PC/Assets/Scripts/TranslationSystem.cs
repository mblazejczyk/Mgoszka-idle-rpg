using UnityEngine;
using UnityEngine.UI;

public class TranslationSystem : MonoBehaviour
{
    [Header("Long texts")]
    public GameObject[] PlTextsToChange;
    public GameObject[] EngTextsToChange;
    [Space(10)]
    [Header("Small texts")]
    public Text[] AllSmallTexts;
    public string[] PolishTexts;
    public string[] EnglishTexts;
    [Space(10)]
    [Header("In script text")]
    public string[] inScriptTextEng;
    public string[] inScriptTextPl;
    [Space(10)]
    [Header("Mission progress text")]
    public string[] MissionProgressPl;
    public string[] MissionProgressEng;
    [Space(5)]
    public string[] DMissionProgressPl;
    public string[] DMissionProgressEng;

    public void UpdateLanguage(int languageId)
    {
        switch (languageId)
        {
            case 0: //pl
                //Zamiana dużych tekstów
                for (int i = 0; i < PlTextsToChange.Length; i++)
                {
                    PlTextsToChange[i].SetActive(true);
                    EngTextsToChange[i].SetActive(false);
                }
                //Zamiana małych tekstów (np na przyciskach)
                foreach(Text t in AllSmallTexts)
                {
                    for (int i = 0; i < EnglishTexts.Length; i++)
                    {
                        if(t.text.ToUpper() == EnglishTexts[i].ToUpper())
                        {
                            t.text = PolishTexts[i];
                            break;
                        }
                    }
                }
                break;
            case 1: //eng
                //Zamiana dużych tekstów
                for (int i = 0; i < PlTextsToChange.Length; i++)
                {
                    PlTextsToChange[i].SetActive(false);
                    EngTextsToChange[i].SetActive(true);
                }
                //Zamiana małych tekstów (np na przyciskach)
                foreach (Text t in AllSmallTexts)
                {
                    for (int i = 0; i < PolishTexts.Length; i++)
                    {
                        if (t.text.ToUpper() == PolishTexts[i].ToUpper())
                        {
                            t.text = EnglishTexts[i];
                            break;
                        }
                    }
                }
                break;
        }
    }

    public string GetText(int TextId)
    {
        switch (PlayerPrefs.GetInt("language"))
        {
            case 1:
                return inScriptTextEng[TextId];
            case 0:
                return inScriptTextPl[TextId];
            default:
                return "";
        }
    }

    public string GetMissionText(int TextId)
    {
        switch (PlayerPrefs.GetInt("language"))
        {
            case 1:
                return MissionProgressEng[TextId];
            case 0:
                return MissionProgressPl[TextId];
            default:
                return "";
        }
    }

    public string GetDaMissionText(int TextId)
    {
        switch (PlayerPrefs.GetInt("language"))
        {
            case 1:
                return DMissionProgressEng[TextId];
            case 0:
                return DMissionProgressPl[TextId];
            default:
                return "";
        }
    }

}
