using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public int FightPlaceId = 0;
    public Transform[] enyPlacesById;
    public Transform[] playerPlaceById;
    [Header("Fight info")]
    public float enyHP;
    public float dixHP;
    public float enyAD;
    public float dixAD;
    public float enyMD;
    public float dixMD;
    public float enyArmor;
    public float dixArmor;
    public float enyBarier;
    public float dixBarier;
    public float enyDodge;
    public float dixDodge;
    public float dixEscape;
    public float dixCritChance;
    public float enyCritChance;

    public bool isRuneActive = false;
    public bool isRuneUsed = false;
    public int RuneUsed;
    [Header("Game info")]
    public GameObject FightUi;
    public Vector3 previousLoc;
    public GameObject enymieKilled;
    public Image EnyHealthBar;
    [Header("UI info")]
    public Text enyHPText;
    public Text enyADText;
    public Text enyMDText;
    public Text enyArmorText;
    public Text enyBarierText;
    public Text enyDodgeText;
    public GameObject rewards;
    public Text rewardList;
    public Text damageHistory;
    public Button AttackBtn;
    public Button EscapeBtn;
    public Button RuneBtn;
    public bool isInFight = false;
    [Header("animations")]
    public GameObject[] enyHitAni;
    public GameObject[] playerHitAni;
    private bool playerHit = false;
    private bool enyHit = false;
    public ParticleSystem[] enyCrit;
    public ParticleSystem[] playerCrit;
    [Space(15)]
    public AudioClip[] hitSounds;
    public AudioSource SFXsource;
    public GameObject[] blockAnimationEny;
    public GameObject[] blockAnimationPlayer;
    public AudioClip blockSound;
    public int EnyId;
    [Space(10)]
    public GameObject TeleportPanel;
    public int placeToTp;
    public GameObject[] placesForEachLand;
    private float StartingMaxEnyHp;
    private TranslationSystem TranslationObject;

    private void Update()
    {
        if(playerHit == true)
        {
            StartCoroutine(PlayerHitAnim());
            playerHit = false;
        }
        if (enyHit == true)
        {
            StartCoroutine(EnyHitAnim());
            enyHit = false;
        }
        if(RuneUsed == 0)
        {
            RuneBtn.interactable = false;
        }
    }
    IEnumerator PlayerHitAnim()
    {
        GameObject tempPlayerObj = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempPlayerObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
    }
    IEnumerator EnyHitAnim()
    {
        GameObject tempEnyBtObj = GameObject.FindGameObjectWithTag("enymieBattle");

        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        tempEnyBtObj.GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
    }
    public void StartFight()
    {
        TranslationObject = GameObject.FindGameObjectWithTag("controller").GetComponent<TranslationSystem>();
        damageHistory.text = "";
        if(isInFight == true)
        {
            return;
        }
        isInFight = true;
        FightUi.SetActive(true);
        GameObject.FindGameObjectWithTag("controller").GetComponent<UiController>().Ekwipunek.GetComponent<EqSystem>().isLimitedEq = true;
        UpdateUi();
        StartingMaxEnyHp = enyHP;
        EnyHealthBar.fillAmount = enyHP / StartingMaxEnyHp;
        if (Random.value > 0.5) //Dix start
        {
            AttackBtn.interactable = false;
            EscapeBtn.interactable = false;
            RuneBtn.interactable = false;
            StartCoroutine(waitForAttack(2));
        }
        else //Enymie start
        {
            AttackBtn.interactable = false;
            EscapeBtn.interactable = false;
            RuneBtn.interactable = false;
            StartCoroutine(waitForAttack(1));
        }
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position = enyPlacesById[FightPlaceId].position;
        GameObject.FindGameObjectWithTag("fightPos").GetComponent<Transform>().position = playerPlaceById[FightPlaceId].position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("fightPos").GetComponent<Transform>().position;
    }

    public void UpdateUi()
    {
        enyHPText.text = "HP: " + enyHP;
        enyADText.text = "AD: " + enyAD;
        enyMDText.text = "MD: " + enyMD;
        enyArmorText.text = "Armor: " + enyArmor;
        enyBarierText.text = "Barier: " + enyBarier;
        enyDodgeText.text = "Dodge chance: " + enyDodge + "%";
    }

    public void UseRune()
    {
        AttackBtn.interactable = false;
        EscapeBtn.interactable = false;
        RuneBtn.interactable = false;
        dixHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP;

        if(RuneUsed >= 2 && RuneUsed <= 6)
        {
            isRuneActive = true;
            StartCoroutine(waitForAttack(1));
        }else if(RuneUsed == 1)
        {
            TeleportPanel.SetActive(true);
        }
    }

    public void Teleport(Dropdown place)
    {
        isRuneActive = true;
        TeleportPanel.SetActive(false);
        placeToTp = place.value;
        StartCoroutine(waitForAttack(1));
    }

    public void Attack()
    {
        AttackBtn.interactable = false;
        EscapeBtn.interactable = false;
        RuneBtn.interactable = false;
        dixHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP;
        if (Random.value > enyDodge / 100) //not dodged
        {
            if (Random.value < 5 / 100 && RuneUsed == 3 && isRuneActive == true && isRuneUsed == false && EnyId != 50)
            {
                isRuneUsed = true;
                EndFight(true, false);

            }
            else if (RuneUsed == 3 && isRuneActive == true)
            {
                isRuneUsed = true;
            }

            if (Random.value > dixCritChance / 100) //not crited
            {
                if(isRuneActive == true && RuneUsed == 4 && isRuneUsed == false)
                {
                    isRuneUsed = true;
                    enyHP = enyHP - Mathf.Max(0, (dixMD - enyBarier * 0.5f)) - Mathf.Max(0, (dixAD - enyArmor * 0.5f));
                    damageHistory.text = TranslationObject.GetText(14) + Mathf.Max(0, (dixMD - enyBarier * 0.5f)) + TranslationObject.GetText(15) + Mathf.Max(0, (dixAD - enyArmor * 0.5f)) + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
                }
                else
                {
                    enyHP = enyHP - Mathf.Max(0, (dixMD - enyBarier)) - Mathf.Max(0, (dixAD - enyArmor));
                    damageHistory.text = TranslationObject.GetText(14) + Mathf.Max(0, (dixMD - enyBarier)) + TranslationObject.GetText(15) + Mathf.Max(0, (dixAD - enyArmor)) + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
                }
            }
            else//crited
            {
                foreach(ParticleSystem ps in enyCrit)
                {
                    ps.Play();
                }

                if (isRuneActive == true && RuneUsed == 4 && isRuneUsed == false)
                {
                    isRuneUsed = true;
                    enyHP = enyHP - (Mathf.Max(0, (dixMD - enyBarier * 0.5f))) * 2;
                    enyHP = enyHP - (Mathf.Max(0, (dixAD - enyArmor * 0.5f))) * 2;
                    damageHistory.text = TranslationObject.GetText(17) + Mathf.Max(0, (dixMD - enyBarier * 0.5f)) * 2 + TranslationObject.GetText(15) + Mathf.Max(0, (dixAD - enyArmor * 0.5f)) * 2 + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
                }
                else
                {
                    enyHP = enyHP - (Mathf.Max(0, (dixMD - enyBarier))) * 2;
                    enyHP = enyHP - (Mathf.Max(0, (dixAD - enyArmor))) * 2;
                    damageHistory.text = TranslationObject.GetText(17) + Mathf.Max(0, (dixMD - enyBarier)) * 2 + TranslationObject.GetText(15) + Mathf.Max(0, (dixAD - enyArmor)) * 2 + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
                }

                
            }
            enyHP = Mathf.Round(enyHP * 100) / 100;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
            SFXsource.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            SFXsource.Play();
            if (dixHP <= 0)
            {
                if (RuneUsed == 5 && isRuneActive == true && isRuneUsed == false) //uzycie runy
                {
                    if (Random.value < 50f / 100) //skracanie czasu smierci
                    {
                        EndFight(false, true);
                    }
                    else //ucieczka
                    {
                        dixEscape = 100;
                        dixHP = 20;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = 20;
                        Escape();
                    }
                }
                else //nie posiada runy lub nie uzywa
                {
                    EndFight(false, false);
                }

            }
            if (enyHP <= 0)
            {
                EndFight(true, false);
            }
            else
            {
                StartCoroutine(waitForAttack(1));
            }
        }
        else //dodged
        {
            foreach(GameObject gobj in blockAnimationEny)
            {
                gobj.SetActive(false);
                gobj.SetActive(true);
            }
            SFXsource.clip = blockSound;
            SFXsource.Play();
            damageHistory.text = TranslationObject.GetText(18) + "\n" + "\n" + damageHistory.text;
            StartCoroutine(waitForAttack(1));
        }

        playerHit = true;
        EnyHealthBar.fillAmount = enyHP / StartingMaxEnyHp;
        foreach(GameObject p in playerHitAni)
        {
            p.SetActive(false);
            p.SetActive(true);
        }
    }

    public void Escape()
    {
        AttackBtn.interactable = false;
        EscapeBtn.interactable = false;
        RuneBtn.interactable = false;
        if (Random.value < dixEscape / 100) //escaped
        {
            rewardList.text = "";
            GameObject.FindGameObjectWithTag("Player").transform.position = previousLoc;
            FightUi.SetActive(false);
            isInFight = false;
            GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
        }
        else //escape failed
        {
            damageHistory.text = TranslationObject.GetText(19) + "\n" + "\n" + damageHistory.text;
            StartCoroutine(waitForAttack(1));
        }
    }

    public void EnyTurn()
    {
        dixHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP;
        if (Random.value > dixDodge / 100) //not dodged
        {
            dixHP = dixHP - Mathf.Max(0, (enyMD - dixBarier)) - Mathf.Max(0, (enyAD - dixArmor));

            if (Random.value > enyCritChance / 100) //not crited
            {
                damageHistory.text = TranslationObject.GetText(20) + Mathf.Max(0, (enyMD - dixBarier)) + TranslationObject.GetText(15) + Mathf.Max(0, (enyAD - dixArmor)) + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
            }
            else //crited
            {
                foreach(ParticleSystem ps in playerCrit)
                {
                    ps.Play();
                }
                dixHP = dixHP - Mathf.Max(0, (enyMD - dixBarier)) - Mathf.Max(0, (enyAD - dixArmor));
                damageHistory.text = TranslationObject.GetText(21) + Mathf.Max(0, (enyMD - dixBarier))*2 + TranslationObject.GetText(15) + Mathf.Max(0, (enyAD - dixArmor))*2 + TranslationObject.GetText(16) + "\n" + "\n" + damageHistory.text;
            }
            dixHP = Mathf.Round(dixHP * 100) / 100;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
            SFXsource.clip = hitSounds[Random.Range(0, hitSounds.Length)];

            switch (RuneUsed)
            {
                case 1:
                    break;
                case 2:
                    if (isRuneUsed == false && isRuneActive == true)
                    {
                        isRuneUsed = true;
                        dixHP = dixHP + (Mathf.Max(0, (enyMD - dixBarier)) + Mathf.Max(0, (enyAD - dixArmor))) * 2;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
                    }
                    break;
                case 6:
                    if (isRuneActive == true)
                    {

                        dixHP = dixHP + (Mathf.Max(0, (enyMD - dixBarier)) + Mathf.Max(0, (enyAD - dixArmor))) * 0.2f;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
                    }
                    break;
            }

            SFXsource.Play();
            if (enyHP <= 0)
            {
                EndFight(true, false);
            }
            
            if (dixHP <= 0)
            {

                if(RuneUsed == 5 && isRuneActive == true && isRuneUsed == false) //uzycie runy
                {
                    if (Random.value < 50f / 100) //skracanie czasu smierci
                    {
                        EndFight(false, true);
                    }
                    else //ucieczka
                    {
                        dixEscape = 100;
                        dixHP = 20;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = 20;
                        Escape();
                    }
                }
                else //nie posiada runy lub nie uzywa
                {
                    EndFight(false ,false);
                }
            }
            else
            {
                if (RuneUsed == 5 && isRuneActive == true)
                {
                    isRuneUsed = true;
                }
                StartCoroutine(waitForAttack(2));
            }
            
        }
        else //dodged
        {
            foreach (GameObject gobj in blockAnimationPlayer)
            {
                gobj.SetActive(false);
                gobj.SetActive(true);
            }
            SFXsource.clip = blockSound;
            SFXsource.Play();
            damageHistory.text = TranslationObject.GetText(22) + "\n" + "\n" + damageHistory.text;
            StartCoroutine(waitForAttack(2));
        }

        enyHit = true;
        EnyHealthBar.fillAmount = enyHP / StartingMaxEnyHp;
        foreach (GameObject p in enyHitAni)
        {
            p.SetActive(false);
            p.SetActive(true);
        }
    }


    void EndFight(bool alive, bool isTimeShorter)
    {
        GameObject.FindGameObjectWithTag("controller").GetComponent<UiController>().Ekwipunek.GetComponent<EqSystem>().isLimitedEq = false;
        isInFight = false;
        
        isRuneUsed = false;
        if (alive == true)
        {
            rewardList.text = "";
            GameObject.FindGameObjectWithTag("Player").transform.position = previousLoc;
            enymieKilled.GetComponent<enymieStats>().Reward();

            if (RuneUsed == 1 && isRuneActive == true)
            {
                GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().MoveToWorld(placeToTp);
                GameObject.FindGameObjectWithTag("Player").transform.position = placesForEachLand[placeToTp].transform.position;
            }
            isRuneActive = false;
            rewards.SetActive(true);
            FightUi.SetActive(false);
            GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
        }
        else
        {
            isRuneActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dead(isTimeShorter);
            GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
        }
    }

    IEnumerator waitForAttack(int whatToDo)
    {
        UpdateUi();
        yield return new WaitForSeconds(1);

        switch (whatToDo)
        {
            case 1:
                EnyTurn();
                break;
            case 2:
                AttackBtn.interactable = true;
                EscapeBtn.interactable = true;

                if(isRuneActive == false)
                {
                    RuneBtn.interactable = true;
                }
                else
                {
                    RuneBtn.interactable = false;
                }
                break;
        }
    }
}
