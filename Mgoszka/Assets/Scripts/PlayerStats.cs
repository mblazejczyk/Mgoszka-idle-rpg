using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("podstawowe stat")]
    public float HP;
    public float maxHP;
    public float Energy;
    public float maxEnergy;
    public float AD;
    public float MD;
    public float Armor;
    public float MagicBarier;
    public float Speed;
    public float Dodge;
    public int charyzma;
    public float escape;

    [Header("Ulepszenia i exp")]
    public float maxXP;
    public float currentXP;
    public float siła;
    public float intelekt;
    public float wytrzymalosc;
    public float bariera;
    public float kondycja;
    public float spryt;
    public float zwinnosc;
    public float charyzmaUpgrade;
    public float predkosc;
    public float critChance;
    public float xpBoost;
    public int activeRune = 0;

    [Header("UI")]
    public Image HPbar;
    public Text HpPoints;
    public Image miniHPbar;
    public Text miniHpPoints;
    public Image energybar;
    public Text energyPoints;
    public Image miniEnergybar;
    public Text miniEnergyPoints;
    public Image miniEXPbar;
    public Image expBar;
    public Text expPoints;
    public Text level_t;
    public Text lp_t;
    public Text coins_text;
    public Text playerLevelMini;

    [Space(20)]
    public int Level;
    public int LevelPoints;
    public int FirstSkill = 0;
    public Image[] upgradeProgress;

    [Space(20)]
    public float coins;
    public int MainSkill = 0;

    [Space(20)]
    public Button[] UpgradeBtns;
    public int[] MaxLevel;
    public int[] CurrentLevel;
    public int[] maxLevel;
    public int[] RequirePoints;
    public GameObject[] LevelUpObjects;
    public Text[] upgradeLevel;

    [Space(20)]
    public GameObject eq;

    [Space(20)]
    public Text[] playerStatsDetail;

    [Space(20)]
    public AudioClip DeathSound;
    public AudioClip LevelUpSound;
    public AudioSource SFXsource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (FirstSkill != 0)
        {
        upgradeProgress[FirstSkill - 1].fillAmount = 1;
        }

        playerLevelMini.text = Level.ToString();
        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[16] = (int)coins;
        coins_text.text = coins.ToString();
        level_t.text = Level.ToString();
        lp_t.text = LevelPoints.ToString();
        HpPoints.text = HP + "/" + maxHP;
        miniHpPoints.text = HP + "/" + maxHP;
        energyPoints.text = Energy + "/" + maxEnergy;
        miniEnergyPoints.text = Energy + "/" + maxEnergy;
        energybar.fillAmount = (Energy / maxEnergy);
        miniEnergybar.fillAmount = (Energy / maxEnergy);
        miniEXPbar.fillAmount = (currentXP / maxXP);
        HPbar.fillAmount = (HP / maxHP);
        miniHPbar.fillAmount = (HP / maxHP);
        expBar.fillAmount = (currentXP / maxXP);
        expPoints.text = currentXP + "/" + maxXP;

        if(currentXP >= maxXP)
        {
            SFXsource.clip = LevelUpSound;
            SFXsource.Play();
            currentXP = 0;
            Level++;
            LevelPoints++;
            maxXP *= 1.2f;
            HP = maxHP;
            Energy += 50;
            foreach(GameObject ob in LevelUpObjects)
            {
                ob.SetActive(true);
            }
        }
        
        if(Energy >= maxEnergy)
        {
            Energy = maxEnergy;
        }

        if(FirstSkill != 0)
        {
            for (int i = 0; i < UpgradeBtns.Length; i++)
            {
                if (RequirePoints[i] <= LevelPoints)
                {
                    UpgradeBtns[i].interactable = true;
                }
                else
                {
                    UpgradeBtns[i].interactable = false;
                }
                
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (RequirePoints[i] <= LevelPoints)
                {
                    UpgradeBtns[i].interactable = true;
                }
                else
                {
                    UpgradeBtns[i].interactable = false;
                }
                
            }
        }

        for (int y = 0; y < UpgradeBtns.Length; y++)
        {
            if(CurrentLevel[y] >= maxLevel[y])
            {
                UpgradeBtns[y].interactable = false;
            }
            if (y >= 5)
            {
                upgradeProgress[y].fillAmount = (float)CurrentLevel[y] / (float)maxLevel[y];
            }
        }

        

        if(critChance >= 40)
        {
            critChance = 40;
        }
        if(HP >= maxHP)
        {
            HP = maxHP;
        }
        HP = Mathf.Round(HP * 100) / 100;
        Energy = Mathf.Round(Energy * 100) / 100;
        coins = Mathf.Round(coins * 100) / 100;
        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[9] = Level;

        playerStatsDetail[0].text = AD.ToString();
        playerStatsDetail[1].text = MD.ToString();
        playerStatsDetail[2].text = Armor.ToString();
        playerStatsDetail[3].text = bariera.ToString();
        playerStatsDetail[4].text = Speed.ToString();
        playerStatsDetail[5].text = Dodge.ToString() + "%";
        playerStatsDetail[6].text = charyzma.ToString();
        playerStatsDetail[7].text = escape.ToString() + "%";
        playerStatsDetail[8].text = critChance.ToString() + "%";

        AD = Mathf.Round(AD * 100) / 100;
        MD = Mathf.Round(MD * 100) / 100;
        Armor = Mathf.Round(Armor * 100) / 100;
        bariera = Mathf.Round(bariera * 100) / 100;
        Speed = Mathf.Round(Speed * 100) / 100;
        Dodge = Mathf.Round(Dodge * 100) / 100;
        //charyzma = Mathf.Round(charyzma * 100) / 100;
        escape = Mathf.Round(escape * 100) / 100;
        critChance = Mathf.Round(critChance * 100) / 100;

        for (int i = 0; i < CurrentLevel.Length; i++)
        {
            upgradeLevel[i].text = "poziom: " + CurrentLevel[i];
        }
    }

    public void Dead(bool isTimeShorter)
    {
        SFXsource.clip = DeathSound;
        SFXsource.Play();
        if(isTimeShorter == true)
        {
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().Killed(5400);
        }
        else
        {
            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().Killed(10800);
        }
    }

    public void Upgrade(int UpgradeId)
    {
        Debug.Log(HP);
        for (int i = 0; i < eq.GetComponent<EqSystem>().eqIds.Length; i++)
        {
            if (eq.GetComponent<EqSystem>().eqIds[i] != "")
            {



                string[] str = eq.GetComponent<EqSystem>().eqIds[i].Split('|');

                if (int.Parse(str[0]) <= 6)
                {
                    foreach (GameObject ob in eq.GetComponent<EqSystem>().items)
                    {

                        if (ob.GetComponent<itemParameters>().Id.ToString() == str[1])
                        {
                            maxHP -= ob.GetComponent<itemParameters>().add_total_HP;
                            HP -= ob.GetComponent<itemParameters>().add_HP;
                            Energy -= ob.GetComponent<itemParameters>().add_energy;
                            maxEnergy -= ob.GetComponent<itemParameters>().add_total_energy;
                            AD -= ob.GetComponent<itemParameters>().add_ad;
                            MD -= ob.GetComponent<itemParameters>().add_md;
                            Armor -= ob.GetComponent<itemParameters>().add_armor;
                            bariera -= ob.GetComponent<itemParameters>().add_barier;
                            Speed -= ob.GetComponent<itemParameters>().add_speed;
                            Dodge -= ob.GetComponent<itemParameters>().add_dodge;
                            charyzma -= ob.GetComponent<itemParameters>().add_charyzma;
                            escape -= ob.GetComponent<itemParameters>().add_escameChance;
                            critChance -= ob.GetComponent<itemParameters>().CritChanse;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("isNull");
            }
        }
        Debug.Log(HP);
        if(FirstSkill == 0)
        {
            FirstSkill = UpgradeId + 1;
        }
        else
        {
            switch (UpgradeId)
            {
                case 0:
                    AD /= 1.5f;
                    break;
                case 1:
                    MD /= 1.5f;
                    break;
                case 2:
                    Armor /= 1.5f;
                    break;
                case 3:
                    bariera /= 1.5f;
                    break;
                case 4:
                    maxEnergy /= 1.5f;
                    break;
                case 5:
                    escape /= 1.5f;
                    break;
                case 6:
                    Dodge /= 1.5f;
                    break;
                case 7:
                    charyzma -= 1;
                    break;
                case 8:
                    Speed /= 1.5f;
                    break;
                case 9:
                    critChance /= 1.5f;
                    break;
            }
        }
        switch (UpgradeId)
        {
            case 0:
                AD *= 1.2f;
                CurrentLevel[0]++;
                break;
            case 1:
                MD *= 1.2f;
                CurrentLevel[1]++;
                break;
            case 2:
                Armor *= 1.2f;
                CurrentLevel[2]++;
                break;
            case 3:
                bariera *= 1.2f;
                CurrentLevel[3]++;
                break;
            case 4:
                maxEnergy *= 1.2f;
                CurrentLevel[4]++;
                break;
            case 5:
                escape *= 1.2f;
                CurrentLevel[5]++;
                break;
            case 6:
                Dodge *= 1.2f;
                CurrentLevel[6]++;
                break;
            case 7:
                charyzma += 1;
                CurrentLevel[7]++;
                break;
            case 8:
                Speed *= 1.2f;
                CurrentLevel[8]++;
                break;
            case 9:
                critChance *= 1.2f;
                CurrentLevel[9]++;
                break;
        }
        switch (UpgradeId)
        {
            case 0:
                AD *= 1.5f;
                break;
            case 1:
                MD *= 1.5f;
                break;
            case 2:
                Armor *= 1.5f;
                break;
            case 3:
                bariera *= 1.5f;
                break;
            case 4:
                maxEnergy *= 1.5f;
                break;
            case 5:
                escape *= 1.5f;
                break;
            case 6:
                Dodge *= 1.5f;
                break;
            case 7:
                charyzma += 1;
                break;
            case 8:
                Speed *= 1.5f;
                break;
            case 9:
                critChance *= 1.5f;
                break;
        }
        for (int i = 0; i < eq.GetComponent<EqSystem>().eqIds.Length; i++)
        {
            if (eq.GetComponent<EqSystem>().eqIds[i] != "")
            {
                string[] str = eq.GetComponent<EqSystem>().eqIds[i].Split('|');

                if (int.Parse(str[0]) <= 6)
                {
                    foreach (GameObject ob in eq.GetComponent<EqSystem>().items)
                    {
                        if (ob.GetComponent<itemParameters>().Id.ToString() == str[1])
                        {
                            maxHP += ob.GetComponent<itemParameters>().add_total_HP;
                            HP += ob.GetComponent<itemParameters>().add_HP;
                            Energy += ob.GetComponent<itemParameters>().add_energy;
                            maxEnergy += ob.GetComponent<itemParameters>().add_total_energy;
                            AD += ob.GetComponent<itemParameters>().add_ad;
                            MD += ob.GetComponent<itemParameters>().add_md;
                            Armor += ob.GetComponent<itemParameters>().add_armor;
                            bariera += ob.GetComponent<itemParameters>().add_barier;
                            Speed += ob.GetComponent<itemParameters>().add_speed;
                            Dodge += ob.GetComponent<itemParameters>().add_dodge;
                            charyzma += ob.GetComponent<itemParameters>().add_charyzma;
                            escape += ob.GetComponent<itemParameters>().add_escameChance;
                            critChance += ob.GetComponent<itemParameters>().CritChanse;
                        }
                    }
                }
            }
        }
        Debug.Log(HP);
        LevelPoints -= RequirePoints[UpgradeId];
        
        GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
    }
}
