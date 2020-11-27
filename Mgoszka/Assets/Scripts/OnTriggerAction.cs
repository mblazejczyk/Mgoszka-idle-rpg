using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTriggerAction : MonoBehaviour
{
    private GameObject actionButton;
    [Header("0 - fight")]
    [Header("1 - talk")]
    [Header("2 - travel")]
    [Header("3 - shop")]
    [Header("4 - add mission progress")]
    public int whatTrigger = 0;
    [Space(15)]
    public GameObject fightPos;
    public GameObject enymieBattle;
    [Space(15)]
    public GameObject travelDest;
    public float TimeInSec;
    public int NewWorldId;
    [Space(15)]
    public int Person;
    [Space(15)]
    public GameObject shop;
    [Space(15)]
    public int whitchProgress;
    public bool isToDestroy;
    private bool started = false;
    private void Awake()
    {
        actionButton = GameObject.FindGameObjectWithTag("actionbtn");
        fightPos = GameObject.FindGameObjectWithTag("fightPos");
        enymieBattle = GameObject.FindGameObjectWithTag("enymieBattle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        actionButton.GetComponent<Animator>().ResetTrigger("down");
        actionButton.GetComponent<Animator>().SetTrigger("up");
    }

    

    IEnumerator energyFlash()
    {
        GameObject.FindGameObjectWithTag("miniEn").GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("miniEn").GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("miniEn").GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("miniEn").GetComponent<Image>().color = Color.black;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        

        actionButton.GetComponent<Animator>().SetTrigger("up");
        actionButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (whatTrigger)
            {
                case 0:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    Debug.Log("0");
                    if(started == true)
                    {
                        return;
                    }
                    if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy < 5)
                    {
                        StartCoroutine(energyFlash());
                    }
                    else
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy -= 5;
                        GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().RestoreEnergy();
                        enymieBattle.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
                        enymieBattle.GetComponent<Fight>().FightPlaceId = this.gameObject.GetComponent<enymieStats>().FightPlaceId;
                        enymieBattle.GetComponent<Fight>().enyHP = this.gameObject.GetComponent<enymieStats>().HP;
                        enymieBattle.GetComponent<Fight>().enyAD = this.gameObject.GetComponent<enymieStats>().AD;
                        enymieBattle.GetComponent<Fight>().enyMD = this.gameObject.GetComponent<enymieStats>().MD;
                        enymieBattle.GetComponent<Fight>().enyArmor = this.gameObject.GetComponent<enymieStats>().Armor;
                        enymieBattle.GetComponent<Fight>().enyBarier = this.gameObject.GetComponent<enymieStats>().MagicBarier;
                        enymieBattle.GetComponent<Fight>().enyDodge = this.gameObject.GetComponent<enymieStats>().DodgeChance;
                        enymieBattle.GetComponent<Fight>().enyCritChance = this.gameObject.GetComponent<enymieStats>().CritChance;

                        enymieBattle.GetComponent<Fight>().dixAD = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD;
                        enymieBattle.GetComponent<Fight>().dixArmor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor;
                        enymieBattle.GetComponent<Fight>().dixBarier = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MagicBarier;
                        enymieBattle.GetComponent<Fight>().dixDodge = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge;
                        enymieBattle.GetComponent<Fight>().dixEscape = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape;
                        enymieBattle.GetComponent<Fight>().dixHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP;
                        enymieBattle.GetComponent<Fight>().dixMD = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD;
                        enymieBattle.GetComponent<Fight>().dixCritChance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance;

                        enymieBattle.GetComponent<Fight>().enymieKilled = this.gameObject;

                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position != fightPos.transform.position)
                        {
                            enymieBattle.GetComponent<Fight>().previousLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
                        }



                        enymieBattle.GetComponent<Fight>().StartFight();
                        
                        started = true;
                        Debug.Log("All dont - fight started!");
                        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    }
                    

                    break;
                case 1:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().ShowMissionPanel(Person);
                    break;
                case 2:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    
                    float tim = (TimeInSec * ((100 - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed) / 100));
                    Debug.Log(tim);
                    Debug.Log(TimeInSec);
                    GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().Travel(travelDest, (int)tim);
                    GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().MoveToWorld(NewWorldId);
                    break;
                case 3:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    shop.SetActive(true);
                    if (started == false)
                    {
                        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[7]++;
                        started = true;
                    }
                    break;
                case 4:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    if (started == false)
                    {


                        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[whitchProgress]++;
                        if (isToDestroy == true)
                        {
                            Destroy(this.gameObject);
                        }
                        started = true;
                    }
                    if (whitchProgress != 14)
                    {
                        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        actionButton.GetComponent<Animator>().SetTrigger("down");
                    }
                    break;
            }
        });
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        started = false;
        actionButton.GetComponent<Animator>().ResetTrigger("up");
        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        actionButton.GetComponent<Animator>().SetTrigger("down");

    }
}
