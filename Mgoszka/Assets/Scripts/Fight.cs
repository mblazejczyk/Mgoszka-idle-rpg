using System.Collections;
using System.Collections.Generic;
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
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
    }
    IEnumerator EnyHitAnim()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position -= new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
        yield return new WaitForSeconds(0.01f);
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Transform>().position += new Vector3(0, 0.1f, 0);
    }
    public void StartFight()
    {
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
            
            Debug.Log("Dix starts");
            AttackBtn.interactable = false;
            EscapeBtn.interactable = false;
            RuneBtn.interactable = false;
            StartCoroutine(waitForAttack(2));
        }
        else //Enymie start
        {
            Debug.Log("Enymie starts");
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
                    damageHistory.text = "Przeciwnik uderzony za: " + Mathf.Max(0, (dixMD - enyBarier * 0.5f)) + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor * 0.5f)) + " fizycznych" + "\n" + "\n" + damageHistory.text;
                }
                else
                {
                    enyHP = enyHP - Mathf.Max(0, (dixMD - enyBarier)) - Mathf.Max(0, (dixAD - enyArmor));
                    damageHistory.text = "Przeciwnik uderzony za: " + Mathf.Max(0, (dixMD - enyBarier)) + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor)) + " fizycznych" + "\n" + "\n" + damageHistory.text;
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
                    damageHistory.text = "Przeciwnik uderzony <color=red>krytycznie</color> za: " + Mathf.Max(0, (dixMD - enyBarier * 0.5f)) * 2 + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor * 0.5f)) * 2 + " fizycznych" + "\n" + "\n" + damageHistory.text;
                }
                else
                {
                    enyHP = enyHP - (Mathf.Max(0, (dixMD - enyBarier))) * 2;
                    enyHP = enyHP - (Mathf.Max(0, (dixAD - enyArmor))) * 2;
                    damageHistory.text = "Przeciwnik uderzony <color=red>krytycznie</color> za: " + Mathf.Max(0, (dixMD - enyBarier)) * 2 + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor)) * 2 + " fizycznych" + "\n" + "\n" + damageHistory.text;
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

                Debug.Log("enymie got hit > waiting for attack");
                StartCoroutine(waitForAttack(1));
            }
            
        }
        else //dodged
        {
            Debug.Log("Dodged > waiting for attack");

            foreach(GameObject gobj in blockAnimationEny)
            {
                gobj.SetActive(false);
                gobj.SetActive(true);
            }
            SFXsource.clip = blockSound;
            SFXsource.Play();
            damageHistory.text = "Przeciwnik uniknął ataku" + "\n" + "\n" + damageHistory.text;
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
            Debug.Log("Escaped!");
        }
        else //escape failed
        {
            Debug.Log("Escape failed > waiting for attack");
            damageHistory.text = "Dix nie uciekł" + "\n" + "\n" + damageHistory.text;
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
                damageHistory.text = "Dix uderzony za: " + Mathf.Max(0, (enyMD - dixBarier)) + " magicznych obrażeń, i: " + Mathf.Max(0, (enyAD - dixArmor)) + " fizycznych" + "\n" + "\n" + damageHistory.text;
            }
            else //crited
            {
                foreach(ParticleSystem ps in playerCrit)
                {
                    ps.Play();
                }
                dixHP = dixHP - Mathf.Max(0, (enyMD - dixBarier)) - Mathf.Max(0, (enyAD - dixArmor));
                damageHistory.text = "Dix uderzony <color=red>krytycznie</color> za: " + Mathf.Max(0, (enyMD - dixBarier))*2 + " magicznych obrażeń, i: " + Mathf.Max(0, (enyAD - dixArmor))*2 + " fizycznych" + "\n" + "\n" + damageHistory.text;
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
                        Debug.Log("Healed: " + (Mathf.Max(0, (enyMD - dixBarier)) + Mathf.Max(0, (enyAD - dixArmor))) * 2);
                    }
                    break;
                case 6:
                    if (isRuneActive == true)
                    {

                        dixHP = dixHP + (Mathf.Max(0, (enyMD - dixBarier)) + Mathf.Max(0, (enyAD - dixArmor))) * 0.2f;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
                        Debug.Log("Healed: " + (Mathf.Max(0, (enyMD - dixBarier)) + Mathf.Max(0, (enyAD - dixArmor))) * 0.2f);
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
                Debug.Log("dix got hit > waiting for attack");
                StartCoroutine(waitForAttack(2));
            }
            
        }
        else //dodged
        {
            Debug.Log("Dodged > waiting for attack");
            foreach (GameObject gobj in blockAnimationPlayer)
            {
                gobj.SetActive(false);
                gobj.SetActive(true);
            }
            SFXsource.clip = blockSound;
            SFXsource.Play();
            damageHistory.text = "Dix uniknął ataku" + "\n" + "\n" + damageHistory.text;
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

        }
        else
        {
            isRuneActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dead(isTimeShorter);
        }
        
    }

    IEnumerator waitForAttack(int whatToDo)
    {
        UpdateUi();
        yield return new WaitForSeconds(1);

        switch (whatToDo)
        {
            case 1:
                Debug.Log("Enymie turn!");
                EnyTurn();
                break;
            case 2:
                Debug.Log("Waiting for decision");
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
