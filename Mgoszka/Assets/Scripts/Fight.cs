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
    [Header("Game info")]
    public GameObject FightUi;
    public Vector3 previousLoc;
    public GameObject enymieKilled;
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
    public bool isInFight = false;
    [Header("animations")]
    public GameObject[] enyHitAni;
    public GameObject[] playerHitAni;
    private bool playerHit = false;
    private bool enyHit = false;
    [Space(15)]
    public AudioClip[] hitSounds;
    public AudioSource SFXsource;
    public GameObject[] blockAnimationEny;
    public GameObject[] blockAnimationPlayer;
    public AudioClip blockSound;

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

        if (Random.value > 0.5) //Dix start
        {
            
            Debug.Log("Dix starts");
            AttackBtn.interactable = false;
            EscapeBtn.interactable = false;
            StartCoroutine(waitForAttack(2));
        }
        else //Enymie start
        {
            Debug.Log("Enymie starts");
            AttackBtn.interactable = false;
            EscapeBtn.interactable = false;
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

    public void Attack()
    {
        AttackBtn.interactable = false;
        EscapeBtn.interactable = false;
        dixHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP;
        if (Random.value > enyDodge / 100) //not dodged
        {
            enyHP = enyHP - Mathf.Max(0, (dixMD - enyBarier)) - Mathf.Max(0, (dixAD - enyArmor));
            
            if (Random.value > dixCritChance / 100) //not crited
            {
                damageHistory.text = "Przeciwnik uderzony za: " + Mathf.Max(0, (dixMD - enyBarier)) + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor)) + " fizycznych" + "\n" + "\n" + damageHistory.text;
            }
            else//crited
            {
                enyHP = enyHP - Mathf.Max(0, (dixMD - enyBarier)) - Mathf.Max(0, (dixAD - enyArmor));
                damageHistory.text = "Przeciwnik uderzony <color=red>krytycznie</color> za: " + Mathf.Max(0, (dixMD - enyBarier))*2 + " magicznych obrażeń, i: " + Mathf.Max(0, (dixAD - enyArmor))*2 + " fizycznych" + "\n" + "\n" + damageHistory.text;
            }
            enyHP = Mathf.Round(enyHP * 100) / 100;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
            SFXsource.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            SFXsource.Play();
            if (dixHP <= 0)
            {
                EndFight(false);
            }
            if (enyHP <= 0)
            {
                EndFight(true);
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
                dixHP = dixHP - Mathf.Max(0, (enyMD - dixBarier)) - Mathf.Max(0, (enyAD - dixArmor));
                damageHistory.text = "Dix uderzony <color=red>krytycznie</color> za: " + Mathf.Max(0, (enyMD - dixBarier))*2 + " magicznych obrażeń, i: " + Mathf.Max(0, (enyAD - dixArmor))*2 + " fizycznych" + "\n" + "\n" + damageHistory.text;
            }
            dixHP = Mathf.Round(dixHP * 100) / 100;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = dixHP;
            SFXsource.clip = hitSounds[Random.Range(0, hitSounds.Length)];
            SFXsource.Play();
            if (enyHP <= 0)
            {
                EndFight(true);
            }
            
            if (dixHP <= 0)
            {
                EndFight(false);
            }
            else
            {
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
        foreach (GameObject p in enyHitAni)
        {
            p.SetActive(false);
            p.SetActive(true);
        }

    }


    void EndFight(bool alive)
    {
        GameObject.FindGameObjectWithTag("controller").GetComponent<UiController>().Ekwipunek.GetComponent<EqSystem>().isLimitedEq = false;
        isInFight = false;
        if (alive == true)
        {
            rewardList.text = "";
            GameObject.FindGameObjectWithTag("Player").transform.position = previousLoc;
            enymieKilled.GetComponent<enymieStats>().Reward();
            rewards.SetActive(true);
            FightUi.SetActive(false);

        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dead();
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
                break;
        }

    }
}
