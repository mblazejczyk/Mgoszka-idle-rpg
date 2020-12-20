using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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
    
    void Start()
    {
        
    }
    void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(fightHis.GetComponent<RectTransform>());
        if (areTimersRemoved == true)
        {
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().isAlive = true;
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().isDone = true;
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().inTravel.SetActive(false);
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().DeadPanel.SetActive(false);
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().DK_dead = DateTime.Now;
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().DK_podroz = DateTime.Now;
        }

        timeInGameText.text = "Czas w grze: ";
        TimeSpan result = TimeSpan.FromSeconds(TotalTimePlayed);
        timeInGameText.text += result.ToString("hh':'mm':'ss");
    }
    void Awake()
    {
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
            AudioListener.volume = 0;
            sound = 1;
            soundText.text = "Dźwięk: wł.";
        }
        else
        {
            AudioListener.volume = 1;
            sound = 0;
            soundText.text = "Dźwięk: wył.";
        }
        SaveSettings();
    }

    public void ChangeFramerate()
    {
        
        switch (Framerate)
        {
            case 15:
                framerateText.text = "Limit FPS: 30";
                Framerate = 30;
                break;
            case 30:
                framerateText.text = "Limit FPS: 60";
                Framerate = 60;
                break;
            case 60:
                framerateText.text = "Limit FPS: 120";
                Framerate = 120;
                break;
            case 120:
                framerateText.text = "Limit FPS: brak";
                Framerate = -1;
                break;
            case -1:
                framerateText.text = "Limit FPS: 15";
                Framerate = 15;
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
        qLevelText.text = "Jakość: " + names[qLevel];
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
        if (sound == 0)
        {
            AudioListener.volume = 0; 
            soundText.text = "Dźwięk: wył.";
        }
        else
        {
            AudioListener.volume = 1;
            soundText.text = "Dźwięk: wł.";
        }
        ChangeQuality();
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

            case "0.1a":
                string[] saveDec;
                saveDec = decod[0].Split('@');
                GameObject g = GameObject.FindGameObjectWithTag("Player");
                g.GetComponent<PlayerStats>().HP = float.Parse(saveDec[0]);
                g.GetComponent<PlayerStats>().maxHP = float.Parse(saveDec[1]);
                g.GetComponent<PlayerStats>().Energy = float.Parse(saveDec[2]);
                g.GetComponent<PlayerStats>().maxEnergy = float.Parse(saveDec[3]);
                g.GetComponent<PlayerStats>().AD = float.Parse(saveDec[4]);
                g.GetComponent<PlayerStats>().MD = float.Parse(saveDec[5]);
                g.GetComponent<PlayerStats>().Armor = float.Parse(saveDec[6]);
                g.GetComponent<PlayerStats>().MagicBarier = float.Parse(saveDec[7]);
                g.GetComponent<PlayerStats>().Speed = float.Parse(saveDec[8]);
                g.GetComponent<PlayerStats>().Dodge = float.Parse(saveDec[9]);
                g.GetComponent<PlayerStats>().charyzma = int.Parse(saveDec[10]);
                g.GetComponent<PlayerStats>().escape = float.Parse(saveDec[11]);
                g.GetComponent<PlayerStats>().maxXP = float.Parse(saveDec[12]);
                g.GetComponent<PlayerStats>().currentXP = float.Parse(saveDec[13]);
                g.GetComponent<PlayerStats>().siła = float.Parse(saveDec[14]);
                g.GetComponent<PlayerStats>().intelekt = float.Parse(saveDec[15]);
                g.GetComponent<PlayerStats>().wytrzymalosc = float.Parse(saveDec[16]);
                g.GetComponent<PlayerStats>().bariera = float.Parse(saveDec[17]);
                g.GetComponent<PlayerStats>().kondycja = float.Parse(saveDec[18]);
                g.GetComponent<PlayerStats>().spryt = float.Parse(saveDec[19]);
                g.GetComponent<PlayerStats>().zwinnosc = float.Parse(saveDec[20]);
                g.GetComponent<PlayerStats>().charyzmaUpgrade = float.Parse(saveDec[21]);
                g.GetComponent<PlayerStats>().predkosc = float.Parse(saveDec[22]);
                g.GetComponent<PlayerStats>().critChance = float.Parse(saveDec[23]);
                g.GetComponent<PlayerStats>().xpBoost = float.Parse(saveDec[24]);
                g.GetComponent<PlayerStats>().Level = int.Parse(saveDec[25]);
                g.GetComponent<PlayerStats>().LevelPoints = int.Parse(saveDec[26]);
                g.GetComponent<PlayerStats>().coins = float.Parse(saveDec[27]);

                string[] crLvl = saveDec[28].Split('#');
                for (int i = 0; i < crLvl.Length; i++)
                {
                    g.GetComponent<PlayerStats>().CurrentLevel[i] = int.Parse(crLvl[i]);
                }

                string[] eqInv = saveDec[29].Split('#');
                for (int i = 0; i < eqInv.Length; i++)
                {
                    eq.GetComponent<EqSystem>().eqIds[i] = eqInv[i];
                }

                string[] missProgr = saveDec[30].Split('#');
                for (int i = 0; i < missProgr.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().progress[i] = int.Parse(missProgr[i]);
                }

                string[] missProg2 = saveDec[31].Split('#');
                for (int i = 0; i < missProg2.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] = int.Parse(missProg2[i]);
                }

                string isOn = saveDec[32];
                string isMiss = saveDec[33];
                if (isOn == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = false;
                }

                if (isMiss == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec[34]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec[35]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_dead = DateTime.FromBinary(Convert.ToInt64(saveDec[36]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_dead = DateTime.FromBinary(Convert.ToInt64(saveDec[37]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec[38]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec[39]));

                string isDo = saveDec[40];
                if (isDo == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = false;
                }

                string isDoXp = saveDec[41];
                if (isDoXp == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = false;
                }
                string isAli = saveDec[42];
                if (isAli == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost = float.Parse(saveDec[43]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec[44]);
                this.gameObject.GetComponent<TimeCountingSystem>().transform.position = new Vector3(float.Parse(saveDec[45]), float.Parse(saveDec[46]), float.Parse(saveDec[47]));

                eq.GetComponent<EqSystem>().UpdateItems();
                this.gameObject.GetComponent<MissionSystem>().currentMissionId = int.Parse(saveDec[48]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec[49]);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(float.Parse(saveDec[50]), float.Parse(saveDec[51]), float.Parse(saveDec[52]));
                TotalTimePlayed = int.Parse(saveDec[53]);
                break;
            case "0.1.1a":
                string[] saveDec1;
                saveDec1 = decod[0].Split('@');


                GameObject g1 = GameObject.FindGameObjectWithTag("Player");
                g1.GetComponent<PlayerStats>().HP = float.Parse(saveDec1[0]);
                g1.GetComponent<PlayerStats>().maxHP = float.Parse(saveDec1[1]);
                g1.GetComponent<PlayerStats>().Energy = float.Parse(saveDec1[2]);
                g1.GetComponent<PlayerStats>().maxEnergy = float.Parse(saveDec1[3]);
                g1.GetComponent<PlayerStats>().AD = float.Parse(saveDec1[4]);
                g1.GetComponent<PlayerStats>().MD = float.Parse(saveDec1[5]);
                g1.GetComponent<PlayerStats>().Armor = float.Parse(saveDec1[6]);
                g1.GetComponent<PlayerStats>().MagicBarier = float.Parse(saveDec1[7]);
                g1.GetComponent<PlayerStats>().Speed = float.Parse(saveDec1[8]);
                g1.GetComponent<PlayerStats>().Dodge = float.Parse(saveDec1[9]);
                g1.GetComponent<PlayerStats>().charyzma = int.Parse(saveDec1[10]);
                g1.GetComponent<PlayerStats>().escape = float.Parse(saveDec1[11]);
                g1.GetComponent<PlayerStats>().maxXP = float.Parse(saveDec1[12]);
                g1.GetComponent<PlayerStats>().currentXP = float.Parse(saveDec1[13]);
                g1.GetComponent<PlayerStats>().siła = float.Parse(saveDec1[14]);
                g1.GetComponent<PlayerStats>().intelekt = float.Parse(saveDec1[15]);
                g1.GetComponent<PlayerStats>().wytrzymalosc = float.Parse(saveDec1[16]);
                g1.GetComponent<PlayerStats>().bariera = float.Parse(saveDec1[17]);
                g1.GetComponent<PlayerStats>().kondycja = float.Parse(saveDec1[18]);
                g1.GetComponent<PlayerStats>().spryt = float.Parse(saveDec1[19]);
                g1.GetComponent<PlayerStats>().zwinnosc = float.Parse(saveDec1[20]);
                g1.GetComponent<PlayerStats>().charyzmaUpgrade = float.Parse(saveDec1[21]);
                g1.GetComponent<PlayerStats>().predkosc = float.Parse(saveDec1[22]);
                g1.GetComponent<PlayerStats>().critChance = float.Parse(saveDec1[23]);
                g1.GetComponent<PlayerStats>().xpBoost = float.Parse(saveDec1[24]);
                g1.GetComponent<PlayerStats>().Level = int.Parse(saveDec1[25]);
                g1.GetComponent<PlayerStats>().LevelPoints = int.Parse(saveDec1[26]);
                g1.GetComponent<PlayerStats>().coins = float.Parse(saveDec1[27]);

                string[] crLvl1 = saveDec1[28].Split('#');
                for (int i = 0; i < crLvl1.Length; i++)
                {
                    g1.GetComponent<PlayerStats>().CurrentLevel[i] = int.Parse(crLvl1[i]);
                }

                string[] eqInv1 = saveDec1[29].Split('#');
                for (int i = 0; i < eqInv1.Length; i++)
                {
                    eq.GetComponent<EqSystem>().eqIds[i] = eqInv1[i];
                }

                string[] missProgr1 = saveDec1[30].Split('#');
                for (int i = 0; i < missProgr1.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().progress[i] = int.Parse(missProgr1[i]);
                }

                string[] missProg21 = saveDec1[31].Split('#');
                for (int i = 0; i < missProg21.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] = int.Parse(missProg21[i]);
                }

                string isOn1 = saveDec1[32];
                string isMiss1 = saveDec1[33];
                if (isOn1 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = false;
                }

                if (isMiss1 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec1[34]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec1[35]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_dead = DateTime.FromBinary(Convert.ToInt64(saveDec1[36]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_dead = DateTime.FromBinary(Convert.ToInt64(saveDec1[37]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec1[38]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec1[39]));

                string isDo1 = saveDec1[40];
                if (isDo1 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = false;
                }

                string isDoXp1 = saveDec1[41];
                if (isDoXp1 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = false;
                }
                string isAli1 = saveDec1[42];
                if (isAli1 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost = float.Parse(saveDec1[43]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec1[44]);
                this.gameObject.GetComponent<TimeCountingSystem>().transform.position = new Vector3(float.Parse(saveDec1[45]), float.Parse(saveDec1[46]), float.Parse(saveDec1[47]));
                
                eq.GetComponent<EqSystem>().UpdateItems();
                this.gameObject.GetComponent<MissionSystem>().currentMissionId = int.Parse(saveDec1[48]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec1[49]);

                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(float.Parse(saveDec1[50]), float.Parse(saveDec1[51]), float.Parse(saveDec1[52]));;
                TotalTimePlayed = int.Parse(saveDec1[53]);
                
                this.gameObject.GetComponent<TimeCountingSystem>().DP_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec1[54]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec1[55]));
                string isEnCooldown = saveDec1[56];
                if (isEnCooldown == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = false;
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill = int.Parse(saveDec1[57]);
                break;
            case "0.3a":
                string[] saveDec2;
                saveDec2 = decod[0].Split('@');


                GameObject g2 = GameObject.FindGameObjectWithTag("Player");
                g2.GetComponent<PlayerStats>().HP = float.Parse(saveDec2[0]);
                g2.GetComponent<PlayerStats>().maxHP = float.Parse(saveDec2[1]);
                g2.GetComponent<PlayerStats>().Energy = float.Parse(saveDec2[2]);
                g2.GetComponent<PlayerStats>().maxEnergy = float.Parse(saveDec2[3]);
                g2.GetComponent<PlayerStats>().AD = float.Parse(saveDec2[4]);
                g2.GetComponent<PlayerStats>().MD = float.Parse(saveDec2[5]);
                g2.GetComponent<PlayerStats>().Armor = float.Parse(saveDec2[6]);
                g2.GetComponent<PlayerStats>().MagicBarier = float.Parse(saveDec2[7]);
                g2.GetComponent<PlayerStats>().Speed = float.Parse(saveDec2[8]);
                g2.GetComponent<PlayerStats>().Dodge = float.Parse(saveDec2[9]);
                g2.GetComponent<PlayerStats>().charyzma = int.Parse(saveDec2[10]);
                g2.GetComponent<PlayerStats>().escape = float.Parse(saveDec2[11]);
                g2.GetComponent<PlayerStats>().maxXP = float.Parse(saveDec2[12]);
                g2.GetComponent<PlayerStats>().currentXP = float.Parse(saveDec2[13]);
                g2.GetComponent<PlayerStats>().siła = float.Parse(saveDec2[14]);
                g2.GetComponent<PlayerStats>().intelekt = float.Parse(saveDec2[15]);
                g2.GetComponent<PlayerStats>().wytrzymalosc = float.Parse(saveDec2[16]);
                g2.GetComponent<PlayerStats>().bariera = float.Parse(saveDec2[17]);
                g2.GetComponent<PlayerStats>().kondycja = float.Parse(saveDec2[18]);
                g2.GetComponent<PlayerStats>().spryt = float.Parse(saveDec2[19]);
                g2.GetComponent<PlayerStats>().zwinnosc = float.Parse(saveDec2[20]);
                g2.GetComponent<PlayerStats>().charyzmaUpgrade = float.Parse(saveDec2[21]);
                g2.GetComponent<PlayerStats>().predkosc = float.Parse(saveDec2[22]);
                g2.GetComponent<PlayerStats>().critChance = float.Parse(saveDec2[23]);
                g2.GetComponent<PlayerStats>().xpBoost = float.Parse(saveDec2[24]);
                g2.GetComponent<PlayerStats>().Level = int.Parse(saveDec2[25]);
                g2.GetComponent<PlayerStats>().LevelPoints = int.Parse(saveDec2[26]);
                g2.GetComponent<PlayerStats>().coins = float.Parse(saveDec2[27]);

                string[] crLvl2 = saveDec2[28].Split('#');
                for (int i = 0; i < crLvl2.Length; i++)
                {
                    g2.GetComponent<PlayerStats>().CurrentLevel[i] = int.Parse(crLvl2[i]);
                }

                string[] eqInv2 = saveDec2[29].Split('#');
                for (int i = 0; i < eqInv2.Length; i++)
                {
                    eq.GetComponent<EqSystem>().eqIds[i] = eqInv2[i];
                }

                string[] missProgr2 = saveDec2[30].Split('#');
                for (int i = 0; i < missProgr2.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().progress[i] = int.Parse(missProgr2[i]);
                }

                string[] missProg22 = saveDec2[31].Split('#');
                for (int i = 0; i < missProg22.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] = int.Parse(missProg22[i]);
                }

                string isOn2 = saveDec2[32];
                string isMiss2 = saveDec2[33];
                if (isOn2 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = false;
                }

                if (isMiss2 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec2[34]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec2[35]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_dead = DateTime.FromBinary(Convert.ToInt64(saveDec2[36]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_dead = DateTime.FromBinary(Convert.ToInt64(saveDec2[37]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec2[38]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec2[39]));

                string isDo2 = saveDec2[40];
                if (isDo2 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = false;
                }

                string isDoXp2 = saveDec2[41];
                if (isDoXp2 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = false;
                }
                string isAli2 = saveDec2[42];
                if (isAli2 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost = float.Parse(saveDec2[43]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec2[44]);
                this.gameObject.GetComponent<TimeCountingSystem>().transform.position = new Vector3(float.Parse(saveDec2[45]), float.Parse(saveDec2[46]), float.Parse(saveDec2[47]));

                eq.GetComponent<EqSystem>().UpdateItems();
                this.gameObject.GetComponent<MissionSystem>().currentMissionId = int.Parse(saveDec2[48]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec2[49]);

                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(float.Parse(saveDec2[50]), float.Parse(saveDec2[51]), float.Parse(saveDec2[52])); ;
                TotalTimePlayed = int.Parse(saveDec2[53]);

                this.gameObject.GetComponent<TimeCountingSystem>().DP_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec2[54]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec2[55]));
                string isEnCooldown2 = saveDec2[56];
                if (isEnCooldown2 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = false;
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill = int.Parse(saveDec2[57]);
                //------------------------------------------------------------
                
                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().Streak = int.Parse(saveDec2[58]);
                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().TodaysMissionId = int.Parse(saveDec2[59]);
                string isMissionClimedL = saveDec2[60];
                if (isMissionClimedL == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = false;
                }


                string[] missProg32 = saveDec2[61].Split('#');
                for (int i = 0; i < missProg32.Length; i++)
                {
                    float x;
                    if (Single.TryParse(missProg32[i], out x))
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = x;
                    }
                    else
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = 0f;
                    }
                }
                
                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionClimed = DateTime.FromBinary(Convert.ToInt64(saveDec2[62]));
                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionGenerated = DateTime.FromBinary(Convert.ToInt64(saveDec2[63]));


                break;
            case "0.4a":
                string[] saveDec3;
                saveDec3 = decod[0].Split('@');


                GameObject g3 = GameObject.FindGameObjectWithTag("Player");
                g3.GetComponent<PlayerStats>().HP = float.Parse(saveDec3[0]);
                g3.GetComponent<PlayerStats>().maxHP = float.Parse(saveDec3[1]);
                g3.GetComponent<PlayerStats>().Energy = float.Parse(saveDec3[2]);
                g3.GetComponent<PlayerStats>().maxEnergy = float.Parse(saveDec3[3]);
                g3.GetComponent<PlayerStats>().AD = float.Parse(saveDec3[4]);
                g3.GetComponent<PlayerStats>().MD = float.Parse(saveDec3[5]);
                g3.GetComponent<PlayerStats>().Armor = float.Parse(saveDec3[6]);
                g3.GetComponent<PlayerStats>().MagicBarier = float.Parse(saveDec3[7]);
                g3.GetComponent<PlayerStats>().Speed = float.Parse(saveDec3[8]);
                g3.GetComponent<PlayerStats>().Dodge = float.Parse(saveDec3[9]);
                g3.GetComponent<PlayerStats>().charyzma = int.Parse(saveDec3[10]);
                g3.GetComponent<PlayerStats>().escape = float.Parse(saveDec3[11]);
                g3.GetComponent<PlayerStats>().maxXP = float.Parse(saveDec3[12]);
                g3.GetComponent<PlayerStats>().currentXP = float.Parse(saveDec3[13]);
                g3.GetComponent<PlayerStats>().siła = float.Parse(saveDec3[14]);
                g3.GetComponent<PlayerStats>().intelekt = float.Parse(saveDec3[15]);
                g3.GetComponent<PlayerStats>().wytrzymalosc = float.Parse(saveDec3[16]);
                g3.GetComponent<PlayerStats>().bariera = float.Parse(saveDec3[17]);
                g3.GetComponent<PlayerStats>().kondycja = float.Parse(saveDec3[18]);
                g3.GetComponent<PlayerStats>().spryt = float.Parse(saveDec3[19]);
                g3.GetComponent<PlayerStats>().zwinnosc = float.Parse(saveDec3[20]);
                g3.GetComponent<PlayerStats>().charyzmaUpgrade = float.Parse(saveDec3[21]);
                g3.GetComponent<PlayerStats>().predkosc = float.Parse(saveDec3[22]);
                g3.GetComponent<PlayerStats>().critChance = float.Parse(saveDec3[23]);
                g3.GetComponent<PlayerStats>().xpBoost = float.Parse(saveDec3[24]);
                g3.GetComponent<PlayerStats>().Level = int.Parse(saveDec3[25]);
                g3.GetComponent<PlayerStats>().LevelPoints = int.Parse(saveDec3[26]);
                g3.GetComponent<PlayerStats>().coins = float.Parse(saveDec3[27]);

                string[] crLvl3 = saveDec3[28].Split('#');
                for (int i = 0; i < crLvl3.Length; i++)
                {
                    g3.GetComponent<PlayerStats>().CurrentLevel[i] = int.Parse(crLvl3[i]);
                }

                string[] eqInv3 = saveDec3[29].Split('#');
                for (int i = 0; i < eqInv3.Length; i++)
                {
                    eq.GetComponent<EqSystem>().eqIds[i] = eqInv3[i];
                }

                string[] missProgr3 = saveDec3[30].Split('#');
                for (int i = 0; i < missProgr3.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().progress[i] = int.Parse(missProgr3[i]);
                }

                string[] missProg33 = saveDec3[31].Split('#');
                for (int i = 0; i < missProg33.Length; i++)
                {
                    this.gameObject.GetComponent<MissionSystem>().missionsProgress[i] = int.Parse(missProg33[i]);
                }

                string isOn3 = saveDec3[32];
                string isMiss3 = saveDec3[33];
                if (isOn3 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isOnMission = false;
                }

                if (isMiss3 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionDone = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().DP_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec3[34]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_podroz = DateTime.FromBinary(Convert.ToInt64(saveDec3[35]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_dead = DateTime.FromBinary(Convert.ToInt64(saveDec3[36]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_dead = DateTime.FromBinary(Convert.ToInt64(saveDec3[37]));
                this.gameObject.GetComponent<TimeCountingSystem>().DP_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec3[38]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_xpBoost = DateTime.FromBinary(Convert.ToInt64(saveDec3[39]));

                string isDo3 = saveDec3[40];
                if (isDo3 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDone = false;
                }

                string isDoXp3 = saveDec3[41];
                if (isDoXp3 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isDoneXp = false;
                }
                string isAli3 = saveDec3[42];
                if (isAli3 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isAlive = false;
                }

                this.gameObject.GetComponent<TimeCountingSystem>().currentXpBoost = float.Parse(saveDec3[43]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec3[44]);
                this.gameObject.GetComponent<TimeCountingSystem>().transform.position = new Vector3(float.Parse(saveDec3[45]), float.Parse(saveDec3[46]), float.Parse(saveDec3[47]));

                eq.GetComponent<EqSystem>().UpdateItems();
                this.gameObject.GetComponent<MissionSystem>().currentMissionId = int.Parse(saveDec3[48]);
                this.gameObject.GetComponent<TimeCountingSystem>().CurrentWorld = int.Parse(saveDec3[49]);

                GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = new Vector3(float.Parse(saveDec3[50]), float.Parse(saveDec3[51]), float.Parse(saveDec3[52])); ;
                TotalTimePlayed = int.Parse(saveDec3[53]);

                this.gameObject.GetComponent<TimeCountingSystem>().DP_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec3[54]));
                this.gameObject.GetComponent<TimeCountingSystem>().DK_energyRest = DateTime.FromBinary(Convert.ToInt64(saveDec3[55]));
                string isEnCooldown3 = saveDec3[56];
                if (isEnCooldown3 == "1")
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = true;
                }
                else
                {
                    this.gameObject.GetComponent<TimeCountingSystem>().isEnergyRestDone = false;
                }
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().FirstSkill = int.Parse(saveDec3[57]);
                //------------------------------------------------------------

                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().Streak = int.Parse(saveDec3[58]);
                GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().TodaysMissionId = int.Parse(saveDec3[59]);
                string isMissionClimedL3 = saveDec3[60];
                if (isMissionClimedL3 == "1")
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = true;
                }
                else
                {
                    this.gameObject.GetComponent<MissionSystem>().isMissionClimed = false;
                }


                string[] missProg34 = saveDec3[61].Split('#');
                for (int i = 0; i < missProg34.Length; i++)
                {
                    float x;
                    if (Single.TryParse(missProg34[i], out x))
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = x;
                    }
                    else
                    {
                        this.gameObject.GetComponent<MissionSystem>().missionDayliProgress[i] = 0f;
                    }
                }

                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionClimed = DateTime.FromBinary(Convert.ToInt64(saveDec3[62]));
                this.gameObject.GetComponent<TimeCountingSystem>().lastMissionGenerated = DateTime.FromBinary(Convert.ToInt64(saveDec3[63]));

                string[] knownPlaces3 = saveDec3[64].Split('#');
                for (int i = 0; i < knownPlaces3.Length; i++)
                {
                    //MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = int.Parse(knownPlaces3[i]);

                    int x;
                    if (int.TryParse(knownPlaces3[i], out x))
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = x;
                    }
                    else
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = 0;
                    }
                }

                string[] KnownItems3 = saveDec3[65].Split('#');
                for (int i = 0; i < KnownItems3.Length; i++)
                {
                    //MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = int.Parse(KnownItems3[i]);


                    int x;
                    if (int.TryParse(KnownItems3[i], out x))
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = x;
                    }
                    else
                    {
                        MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = 0;
                    }
                }

                break;

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
                //------------------------------------------------------------

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
                    //MapSystem.GetComponent<MapSizeSystem>().knownPlaces[i] = int.Parse(knownPlaces3[i]);

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
                    //MapSystem.GetComponent<MapSizeSystem>().knownItems[i] = int.Parse(KnownItems3[i]);


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
}
