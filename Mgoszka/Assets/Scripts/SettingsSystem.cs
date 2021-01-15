using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class SettingsSystem : MonoBehaviour
{
    public int sound;
    public Text soundText;
    [Space(5)]
    public int Framerate;
    public Text framerateText;
    [Space(5)]
    public int qLevel;
    public Text qLevelText;
    [Space(5)]
    public int languageId = 0;
    public Text LanguageText;
    [Space(5)]
    public GameObject eq;
    [Space(5)]
    public GameObject SavingText;
    public GameObject GameSavedText;
    private bool areTimersRemoved;
    [Space(5)]
    public GameObject fightHis;
    public int TotalTimePlayed;
    [Space(10)]
    public Text timeInGameText;
    [Space(20)]
    public GameObject[] guidesAttentions;
    public GameObject MapSystem;
    [Space(50)]
    public int areItemsKnown = 0;
    private TranslationSystem translationText;
    void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(fightHis.GetComponent<RectTransform>());
        if (areTimersRemoved == true)
        {
            GameObject tempObj = GameObject.FindGameObjectWithTag("controller");

            tempObj.GetComponent<TimeCountingSystem>().isAlive = true;
            tempObj.GetComponent<TimeCountingSystem>().isDone = true;
            tempObj.GetComponent<TimeCountingSystem>().inTravel.SetActive(false);
            tempObj.GetComponent<TimeCountingSystem>().DeadPanel.SetActive(false);
            tempObj.GetComponent<TimeCountingSystem>().DK_dead = DateTime.Now;
            tempObj.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.Now;
        }

        timeInGameText.text = "Czas w grze: ";
        TimeSpan result = TimeSpan.FromSeconds(TotalTimePlayed);
        timeInGameText.text += result.ToString("hh':'mm':'ss");
    }
    void Awake()
    {
        translationText = GameObject.FindGameObjectWithTag("controller").GetComponent<TranslationSystem>();
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().ResetAllNotifications(false);

        if(PlayerPrefs.GetInt("gobjAttentions") == 1)
        {
            foreach(GameObject gob in guidesAttentions)
            {
                gob.SetActive(false);
            }
        }

        if(PlayerPrefs.GetInt("freshStart") != 1)
        {
            PlayerPrefs.SetInt("qlevel", 1);
            PlayerPrefs.SetInt("fpslock", 60);
            PlayerPrefs.SetInt("soundSet", 1);
            PlayerPrefs.SetInt("freshStart", 1);
        }
        LoadSettings();

        if(PlayerPrefs.GetInt("saveLocated") == 1)
        {
            LoadGame();
        }
        StartCoroutine(autoSave());
        StartCoroutine(timeCounter());
    }

    public void ChangeLanguage()
    {
        switch (languageId)
        {
            case 0: //pl > eng
                languageId = 1;
                gameObject.GetComponent<TranslationSystem>().UpdateLanguage(languageId);
                LanguageText.text = "Language: Eng";
                break;
            case 1: //eng > pl
                languageId = 0;
                gameObject.GetComponent<TranslationSystem>().UpdateLanguage(languageId);
                LanguageText.text = "Język: Pl";
                break;
        }
        PlayerPrefs.SetInt("language", languageId);

        if (Framerate == -1)
        {
            framerateText.text = translationText.GetText(34);
        }
        else
        {
            framerateText.text = translationText.GetText(31) + Framerate;
        }

        if (sound == 1)
        {
            soundText.text = translationText.GetText(29);
        }
        else
        {
            soundText.text = translationText.GetText(33);
        }

        string[] names;
        names = QualitySettings.names;
        qLevelText.text = translationText.GetText(32) + names[qLevel];

    }
    public void SetGuideOpenTrue()
    {
        PlayerPrefs.SetInt("gobjAttentions", 1);
        foreach (GameObject gob in guidesAttentions)
        {
            gob.SetActive(false);
        }
    }

    public void soundToggle()
    {
        if(sound == 0)
        {
            AudioListener.volume = 1;
            sound = 1;
            soundText.text = translationText.GetText(29);
        }
        else
        {
            AudioListener.volume = 0;
            sound = 0;
            soundText.text = translationText.GetText(33);
        }
        SaveSettings();
    }

    public void ChangeFramerate()
    {
        
        switch (Framerate)
        {
            case 15:
                Framerate = 30;
                framerateText.text = translationText.GetText(31) + Framerate;
                break;
            case 30:
                Framerate = 60;
                framerateText.text = translationText.GetText(31) + Framerate;
                break;
            case 60:
                Framerate = 120;
                framerateText.text = translationText.GetText(31) + Framerate;
                break;
            case 120:
                Framerate = -1;
                framerateText.text = translationText.GetText(34);
                break;
            case -1:
                Framerate = 15;
                framerateText.text = translationText.GetText(31) + Framerate;
                break;
        }
        Application.targetFrameRate = Framerate;
        SaveSettings();
    }

    public void ChangeQuality()
    {
        string[] names;
        names = QualitySettings.names;

        if(qLevel >= 5)
        {
            qLevel = 0;
        }
        else
        {
            qLevel++;
        }
        QualitySettings.SetQualityLevel(qLevel);
        qLevelText.text = translationText.GetText(32) + names[qLevel];
        SaveSettings();
    }

    public void QuitGame()
    {
        SaveGame();
        SaveSettings();
        Application.Quit();
    }

    public void RepoerBug()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeOjpvUHLwII2c9G1A7t3iI4w02H4fKRbmUR8GBDC5oe-HC7Q/viewform?usp=sf_link");
    }

    public void JoinTranslation()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScDJZJsBymAC44Pdu_S5XA5kEraicIMl_s1SxmyW6CL9YsNUQ/viewform?usp=sf_link");
    }

    public void RemoveSave()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void UseCupon(InputField inF)
    {
        if (inF.text.Contains("add_hp"))
        {
            string s = "";
            int i = 1;
            foreach (char c in inF.text)
            {
                if (i >= 8)
                {
                    s += c;
                }
                i++;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP += int.Parse(s);
        }

        if (inF.text.Contains("add_maxhp"))
        {
            string s = "";
            int i = 1;
            foreach (char c in inF.text)
            {
                if (i >= 11)
                {
                    s += c;
                }
                i++;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP += int.Parse(s);
        }

        if (inF.text.Contains("add_coins"))
        {
            string s = "";
            int i = 1;
            foreach (char c in inF.text)
            {
                if (i >= 11)
                {
                    s += c;
                }
                i++;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins += int.Parse(s);
        }

        if (inF.text.Contains("add_item"))
        {
            string s = "";
            int i = 1;
            foreach(char c in inF.text)
            {
                if (i >= 10)
                {
                    s += c;
                }
                i++;
            }
            Debug.Log(s);

            eq.GetComponent<EqSystem>().AddItem(int.Parse(s));
        }

        if (inF.text.Contains("remove_save_yes"))
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }

        if (inF.text.Contains("timers_off"))
        {
            areTimersRemoved = true;
        }
        if (inF.text.Contains("timers_on"))
        {
            areTimersRemoved = false;
        }

        if (inF.text.Contains("set_first_skill"))
        {
            string s = "";
            int i = 1;
            foreach (char c in inF.text)
            {
                if (i >= 17)
                {
                    s += c;
                }
                i++;
            }
            Debug.Log(s);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill = int.Parse(s) + 1;
            switch (int.Parse(s))
            {
                case 0:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD *= 1.5f;
                    break;
                case 1:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD *= 1.5f;
                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor *= 1.5f;
                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().bariera *= 1.5f;
                    break;
                case 4:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy *= 1.5f;
                    break;
                case 5:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape *= 1.5f;
                    break;
                case 6:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge *= 1.5f;
                    break;
                case 7:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma = (int)(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma * 1.5f);
                    break;
                case 8:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed *= 1.5f;
                    break;
                case 9:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance *= 1.5f;
                    break;
            }
            

        }

        if (inF.text.Contains("repair_energy"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy = 100;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy = 100;
        }

        if (inF.text.Contains("set_level_progress"))
        {
            string s = "";
            int i = 1;
            foreach (char c in inF.text)
            {
                if (i >= 20 && i <= 20)
                {
                    s += c;
                }
                i++;
            }

            string d = "";
            int x = 1;
            foreach (char g in inF.text)
            {
                if (x >= 22)
                {
                    d += g;
                }
                x++;
            }

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().CurrentLevel[int.Parse(s)] = int.Parse(d);
        }


        if (inF.text.Contains("set_items_known"))
        {
            areItemsKnown = 1;
        }

        if (inF.text.Contains("set_custom_save"))
        {
            customLoadingPanel.SetActive(true);
            CustomLoad();
        }

        inF.text = "";
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("qlevel", qLevel);
        PlayerPrefs.SetInt("fpslock", Framerate);
        PlayerPrefs.SetInt("soundSet", sound);
    }

    public void LoadSettings()
    {
        qLevel = PlayerPrefs.GetInt("qlevel") - 1;
        
        Framerate = PlayerPrefs.GetInt("fpslock");
        Application.targetFrameRate = Framerate;
        
        switch (Framerate)
        {
            case 15:
                framerateText.text = "Limit FPS: 15";
                break;
            case 30:
                framerateText.text = "Limit FPS: 30";
                break;
            case 60:
                framerateText.text = "Limit FPS: 60";
                break;
            case 120:
                framerateText.text = "Limit FPS: 120";
                break;
            case -1:
                framerateText.text = "Limit FPS: brak";
                break;
        }
        sound = PlayerPrefs.GetInt("soundSet");
        if (sound == 1)
        {
            AudioListener.volume = 1; 
            soundText.text = "Dźwięk: wł.";
        }
        else
        {
            AudioListener.volume = 0;
            soundText.text = "Dźwięk: wył.";
        }
        ChangeQuality();

        gameObject.GetComponent<TranslationSystem>().UpdateLanguage(PlayerPrefs.GetInt("language"));
        languageId = PlayerPrefs.GetInt("language");
        switch (languageId)
        {
            case 1:
                LanguageText.text = "Language: Eng";
                break;
            case 0:
                LanguageText.text = "Język: Pl";
                break;
        }
    }

    public void SaveGame()
    {
        SavingText.SetActive(true);
        string savingPoint;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        
        string crLvl = "";
        for (int i = 0; i < g.GetComponent<PlayerStats>().CurrentLevel.Length; i++)
        {
            crLvl += g.GetComponent<PlayerStats>().CurrentLevel[i].ToString() + "#";
        }
        crLvl = crLvl.Substring(0, crLvl.Length - 1);

        string eqInv = "";
        for (int i = 0; i < eq.GetComponent<EqSystem>().eqIds.Length; i++)
        {
            eqInv += eq.GetComponent<EqSystem>().eqIds[i] + "#";
        }
        eqInv = eqInv.Substring(0, eqInv.Length - 1);

        string missProgr = "";
        for (int i = 0; i < this.gameObject.GetComponent<MissionSystem>().progress.Length; i++)
        {
            missProgr += this.gameObject.GetComponent<MissionSystem>().progress[i] + "#";
        }
        missProgr = missProgr.Substring(0, missProgr.Length -1);

        string missProg2 = "";
        for (int i = 0; i < this.gameObject.GetComponent<MissionSystem>().missionsProgress.Length; i++)
        {
            missProg2 += this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] + "#";
        }
        missProg2 = missProg2.Substring(0, missProg2.Length - 1);

        string isOn;
        string isMiss;
        if(this.gameObject.GetComponent<MissionSystem>().isOnMission == true)
        {
            isOn = "1";
        }
        else
        {
            isOn = "0";
        }

        if (this.gameObject.GetComponent<MissionSystem>().isMissionDone == true)
        {
            isMiss = "1";
        }
        else
        {
            isMiss = "0";
        }

        string isDo;
        if (this.gameObject.GetComponent<TimeCountingSystem>().isDone == true)
        {
            isDo = "1";
        }
        else
        {
            isDo = "0";
        }
        string isDoXp;
        if (this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp == true)
        {
            isDoXp = "1";
        }
        else
        {
            isDoXp = "0";
        }
        string isAli;
        if (this.gameObject.GetComponent<TimeCountingSystem>().isAlive == true)
        {
            isAli = "1";
        }
        else
        {
            isAli = "0";
        }
        string isEnergyOnRest;
        if(this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone == true)
        {
            isEnergyOnRest = "1";
        }
        else
        {
            isEnergyOnRest = "0";
        }

        savingPoint = g.GetComponent<PlayerStats>().HP + "@" +
            g.GetComponent<PlayerStats>().maxHP + "@" +
            g.GetComponent<PlayerStats>().Energy + "@" +
            g.GetComponent<PlayerStats>().maxEnergy + "@" +
            g.GetComponent<PlayerStats>().AD + "@" +
            g.GetComponent<PlayerStats>().MD + "@" +
            g.GetComponent<PlayerStats>().Armor + "@" +
            g.GetComponent<PlayerStats>().MagicBarier + "@" +
            g.GetComponent<PlayerStats>().Speed + "@" +
            g.GetComponent<PlayerStats>().Dodge + "@" +
            g.GetComponent<PlayerStats>().charyzma + "@" +
            g.GetComponent<PlayerStats>().escape + "@" +
            g.GetComponent<PlayerStats>().maxXP + "@" +
            g.GetComponent<PlayerStats>().currentXP + "@" +
            g.GetComponent<PlayerStats>().siła + "@" +
            g.GetComponent<PlayerStats>().intelekt + "@" +
            g.GetComponent<PlayerStats>().wytrzymalosc + "@" +
            g.GetComponent<PlayerStats>().bariera + "@" +
            g.GetComponent<PlayerStats>().kondycja + "@" +
            g.GetComponent<PlayerStats>().spryt + "@" +
            g.GetComponent<PlayerStats>().zwinnosc + "@" +
            g.GetComponent<PlayerStats>().charyzmaUpgrade + "@" +
            g.GetComponent<PlayerStats>().predkosc + "@" +
            g.GetComponent<PlayerStats>().critChance + "@" +
            g.GetComponent<PlayerStats>().xpBoost + "@" +
            g.GetComponent<PlayerStats>().Level + "@" +
            g.GetComponent<PlayerStats>().LevelPoints + "@" +
            g.GetComponent<PlayerStats>().coins + "@" +
            crLvl + "@" +
            eqInv + "@" +
            missProgr + "@" +
            missProg2 + "@" +
            isOn + "@" +
            isMiss + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DP_dead.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DK_dead.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost.ToBinary().ToString() + "@" +
            isDo + "@" +
            isDoXp + "@" +
            isAli + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().transform.position.x + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().transform.position.y + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().transform.position.z + "@" +
            this.gameObject.GetComponent<MissionSystem>().currentMissionId + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld;

        if (GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().isInFight == false)
        {
            savingPoint +=  "@" + GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + "@" +
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.y + "@" +
                            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.z;
        }
        else
        {
            savingPoint += "@" + GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().previousLoc.x + "@" +
                GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().previousLoc.y + "@" +
                GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().previousLoc.z;
        }

        savingPoint += "@" + TotalTimePlayed;
            
        savingPoint +=  "@" + this.gameObject.GetComponent<TimeCountingSystem>().DP_energyRest.ToBinary().ToString() + "@" +
                        this.gameObject.GetComponent<TimeCountingSystem>().DK_energyRest.ToBinary().ToString() + "@" +
                        isEnergyOnRest;
        savingPoint += "@" + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill;

        

        string isMissionClimedL;
        if (this.gameObject.GetComponent<MissionSystem>().isMissionClimed == true)
        {
            isMissionClimedL = "1";
        }
        else
        {
            isMissionClimedL = "0";
        }
        savingPoint += "@" + GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().Streak + "@"
            + GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().TodaysMissionId + "@" +
            isMissionClimedL;

        string missProg3 = "";
        for (int i = 0; i < this.gameObject.GetComponent<MissionSystem>().missionDayliProgress.Length; i++)
        {
            missProg3 += this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] + "#";
        }
        missProg3 = missProg3.Substring(0, missProg2.Length - 1);

        savingPoint += "@" + missProg3;

        savingPoint += "@" + this.gameObject.GetComponent<TimeCountingSystem>().lastMissionClimed.ToBinary().ToString() + "@" +
            this.gameObject.GetComponent<TimeCountingSystem>().lastMissionGenerated.ToBinary().ToString();


        string knownPlaces = "";
        for (int i = 0; i < MapSystem.GetComponent<MapSizeSystem>().knownPlaces.Length; i++)
        {
            knownPlaces += MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i].ToString() + "#";
        }
        knownPlaces = knownPlaces.Substring(0, knownPlaces.Length - 1);


        string knownItems = "";
        for (int i = 0; i < MapSystem.GetComponent<MapSizeSystem>().knownItems.Length; i++)
        {
            knownItems += MapSystem.GetComponent<MapSizeSystem>().knownItems[i].ToString() + "#";
        }
        knownItems = knownItems.Substring(0, knownItems.Length - 1);
        
        savingPoint += "@" + knownPlaces + "@" + knownItems;

        savingPoint += "@" + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().activeRune;

        string final;
        final = savingPoint + "$" + "0.5a";
        PlayerPrefs.SetString("mainSave", final);
        Debug.Log("Saved");
        PlayerPrefs.SetInt("saveLocated", 1);

        SavingText.SetActive(false);
        GameSavedText.SetActive(true);
    }

    public void LoadGame()
    {
        string[] decod;
        decod = PlayerPrefs.GetString("mainSave").Split('$');
        
        switch (decod[1])
        {
            case "0.5a":
                string[] saveDec4;
                saveDec4 = decod[0].Split('@');

                GameObject g4 = GameObject.FindGameObjectWithTag("Player");
                g4.GetComponent<PlayerStats>().HP = float.Parse(saveDec4[0]);
                g4.GetComponent<PlayerStats>().maxHP = float.Parse(saveDec4[1]);
                g4.GetComponent<PlayerStats>().Energy = float.Parse(saveDec4[2]);
                g4.GetComponent<PlayerStats>().maxEnergy = float.Parse(saveDec4[3]);
                g4.GetComponent<PlayerStats>().AD = float.Parse(saveDec4[4]);
                g4.GetComponent<PlayerStats>().MD = float.Parse(saveDec4[5]);
                g4.GetComponent<PlayerStats>().Armor = float.Parse(saveDec4[6]);
                g4.GetComponent<PlayerStats>().MagicBarier = float.Parse(saveDec4[7]);
                g4.GetComponent<PlayerStats>().Speed = float.Parse(saveDec4[8]);
                g4.GetComponent<PlayerStats>().Dodge = float.Parse(saveDec4[9]);
                g4.GetComponent<PlayerStats>().charyzma = int.Parse(saveDec4[10]);
                g4.GetComponent<PlayerStats>().escape = float.Parse(saveDec4[11]);
                g4.GetComponent<PlayerStats>().maxXP = float.Parse(saveDec4[12]);
                g4.GetComponent<PlayerStats>().currentXP = float.Parse(saveDec4[13]);
                g4.GetComponent<PlayerStats>().siła = float.Parse(saveDec4[14]);
                g4.GetComponent<PlayerStats>().intelekt = float.Parse(saveDec4[15]);
                g4.GetComponent<PlayerStats>().wytrzymalosc = float.Parse(saveDec4[16]);
                g4.GetComponent<PlayerStats>().bariera = float.Parse(saveDec4[17]);
                g4.GetComponent<PlayerStats>().kondycja = float.Parse(saveDec4[18]);
                g4.GetComponent<PlayerStats>().spryt = float.Parse(saveDec4[19]);
                g4.GetComponent<PlayerStats>().zwinnosc = float.Parse(saveDec4[20]);
                g4.GetComponent<PlayerStats>().charyzmaUpgrade = float.Parse(saveDec4[21]);
                g4.GetComponent<PlayerStats>().predkosc = float.Parse(saveDec4[22]);
                g4.GetComponent<PlayerStats>().critChance = float.Parse(saveDec4[23]);
                g4.GetComponent<PlayerStats>().xpBoost = float.Parse(saveDec4[24]);
                g4.GetComponent<PlayerStats>().Level = int.Parse(saveDec4[25]);
                g4.GetComponent<PlayerStats>().LevelPoints = int.Parse(saveDec4[26]);
                g4.GetComponent<PlayerStats>().coins = float.Parse(saveDec4[27]);

                string[] crLvl4 = saveDec4[28].Split('#');
                for (int i = 0; i < crLvl4.Length; i++)
                {
                    g4.GetComponent<PlayerStats>().CurrentLevel[i] = int.Parse(crLvl4[i]);
                }

                string[] eqInv4 = saveDec4[29].Split('#');
                for (int i = 0; i < eqInv4.Length; i++)
                {
                    eq.GetComponent<EqSystem>().eqIds[i] = eqInv4[i];
                }

                string[] missProgr4 = saveDec4[30].Split('#');
                for (int i = 0; i < missProgr4.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().progress[i] = int.Parse(missProgr4[i]);
                }

                string[] missProg44 = saveDec4[31].Split('#');
                for (int i = 0; i < missProg44.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] = int.Parse(missProg44[i]);
                }

                string isOn4 = saveDec4[32];
                string isMiss4 = saveDec4[33];
                if (isOn4 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = false;
                }

                if (isMiss4 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec4[34]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec4[35]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_dead = DateTime.FromBinary(Convert.ToInt64(saveDec4[36]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_dead = DateTime.FromBinary(Convert.ToInt64(saveDec4[37]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec4[38]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec4[39]));

                string isDo4 = saveDec4[40];
                if (isDo4 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = false;
                }

                string isDoXp4 = saveDec4[41];
                if (isDoXp4 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = false;
                }
                string isAli4 = saveDec4[42];
                if (isAli4 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost = float.Parse(saveDec4[43]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec4[44]);
                this.gameObject.GetComponent<TimeCountingSystem>().transform.position = new Vector3(float.Parse(saveDec4[45]), float.Parse(saveDec4[46]), float.Parse(saveDec4[47]));

                eq.GetComponent<EqSystem>().UpdateItems();
                this.gameObject.GetComponent<MissionSystem>().currentMissionId = int.Parse(saveDec4[48]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec4[49]);

                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(float.Parse(saveDec4[50]), float.Parse(saveDec4[51]), float.Parse(saveDec4[52])); ;
                TotalTimePlayed = int.Parse(saveDec4[53]);

                this.gameObject.GetComponent<TimeCountingSystem>().DP_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec4[54]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec4[55]));
                string isEnCooldown4 = saveDec4[56];
                if (isEnCooldown4 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = false;
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill = int.Parse(saveDec4[57]);
                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().Streak = int.Parse(saveDec4[58]);
                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().TodaysMissionId = int.Parse(saveDec4[59]);
                string isMissionClimedL4 = saveDec4[60];
                if (isMissionClimedL4 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = false;
                }

                string[] missProg45 = saveDec4[61].Split('#');
                for (int i = 0; i < missProg45.Length; i++)
                {
                    float x;
                    if (Single.TryParse(missProg45[i], out x))
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = x;
                    }
                    else
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = 0f;
                    }
                }

                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionClimed = DateTime.FromBinary(Convert.ToInt64(saveDec4[62]));
                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionGenerated = DateTime.FromBinary(Convert.ToInt64(saveDec4[63]));

                string[] knownPlaces4 = saveDec4[64].Split('#');
                for (int i = 0; i < knownPlaces4.Length; i++)
                {
                    int x;
                    if (int.TryParse(knownPlaces4[i], out x))
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = x;
                    }
                    else
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = 0;
                    }
                }

                string[] KnownItems4 = saveDec4[65].Split('#');
                for (int i = 0; i < KnownItems4.Length; i++)
                {
                    int x;
                    if (int.TryParse(KnownItems4[i], out x))
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = x;
                    }
                    else
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = 0;
                    }
                }

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().activeRune = int.Parse(saveDec4[66]);

                break;
        }
    }

    IEnumerator autoSave()
    {
        yield return new WaitForSeconds(30);
        SaveGame();
        StartCoroutine(autoSave());
    }

    IEnumerator timeCounter()
    {
        yield return new WaitForSeconds(1);
        TotalTimePlayed++;
        StartCoroutine(timeCounter());
    }

    public void CustomSave(InputField InputF)
    {
        string savingPoint = InputF.text;

        PlayerPrefs.SetString("mainSave", savingPoint);
        Debug.Log("Saved");
        PlayerPrefs.SetInt("saveLocated", 1);

        SavingText.SetActive(false);
        GameSavedText.SetActive(true);
    }

    public GameObject customLoadingPanel;
    public InputField customSaving;
    public void CustomLoad()
    {
        string cs = PlayerPrefs.GetString("mainSave");
        customSaving.text = cs;
    }
}
