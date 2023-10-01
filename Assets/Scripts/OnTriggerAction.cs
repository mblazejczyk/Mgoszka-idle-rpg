using System.Collections;
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
    public GameObject travelPreparation;
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
    [Space(15)]
    public GameObject actionButtonImage;
    public Sprite talkSprite;
    public Sprite fightSprite;
    public Sprite travelSprite;
    public Sprite otherActionSprite;
    private void Awake()
    {
        actionButton = GameObject.FindGameObjectWithTag("actionbtn");
        fightPos = GameObject.FindGameObjectWithTag("fightPos");
        enymieBattle = GameObject.FindGameObjectWithTag("enymieBattle");
        actionButtonImage = GameObject.FindGameObjectWithTag("actionChild");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(actionButtonImage == null)
        {
            actionButtonImage = GameObject.FindGameObjectWithTag("actionChild");
        }

        actionButton.GetComponent<Animator>().ResetTrigger("down");
        actionButton.GetComponent<Animator>().SetTrigger("up");
        switch (whatTrigger)
        {
            case 1:
                actionButtonImage.GetComponent<Image>().sprite = talkSprite;
                break;
            case 2:
                actionButtonImage.GetComponent<Image>().sprite = travelSprite;
                break;
            case 3:
                actionButtonImage.GetComponent<Image>().sprite = talkSprite;
                break;
            case 4:
                actionButtonImage.GetComponent<Image>().sprite = otherActionSprite;
                break;
            case 0:
                actionButtonImage.GetComponent<Image>().sprite = fightSprite;
                break;
        }
    }

    IEnumerator energyFlash()
    {
        GameObject tempObj = GameObject.FindGameObjectWithTag("miniEn");

        tempObj.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        tempObj.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.2f);
        tempObj.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        tempObj.GetComponent<Image>().color = Color.black;
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
                        GameObject tempPlayerObj = GameObject.FindGameObjectWithTag("Player");

                        tempPlayerObj.GetComponent<PlayerStats>().Energy -= 5;
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
                        enymieBattle.GetComponent<Fight>().EnyId = this.gameObject.GetComponent<enymieStats>().enyId;

                        enymieBattle.GetComponent<Fight>().dixAD = tempPlayerObj.GetComponent<PlayerStats>().AD;
                        enymieBattle.GetComponent<Fight>().dixArmor = tempPlayerObj.GetComponent<PlayerStats>().Armor;
                        enymieBattle.GetComponent<Fight>().dixBarier = tempPlayerObj.GetComponent<PlayerStats>().MagicBarier;
                        enymieBattle.GetComponent<Fight>().dixDodge = tempPlayerObj.GetComponent<PlayerStats>().Dodge;
                        enymieBattle.GetComponent<Fight>().dixEscape = tempPlayerObj.GetComponent<PlayerStats>().escape;
                        enymieBattle.GetComponent<Fight>().dixHP = tempPlayerObj.GetComponent<PlayerStats>().HP;
                        enymieBattle.GetComponent<Fight>().dixMD = tempPlayerObj.GetComponent<PlayerStats>().MD;
                        enymieBattle.GetComponent<Fight>().dixCritChance = tempPlayerObj.GetComponent<PlayerStats>().critChance;
                        enymieBattle.GetComponent<Fight>().RuneUsed = tempPlayerObj.GetComponent<PlayerStats>().activeRune;

                        enymieBattle.GetComponent<Fight>().enymieKilled = this.gameObject;

                        if (tempPlayerObj.GetComponent<Transform>().position != fightPos.transform.position)
                        {
                            enymieBattle.GetComponent<Fight>().previousLoc = tempPlayerObj.GetComponent<Transform>().position;
                        }

                        enymieBattle.GetComponent<Fight>().StartFight();
                        
                        started = true;
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
                    travelPreparation.SetActive(true);
                    travelPreparation.GetComponent<TravelPreparations>().PrepareTravel((int)tim, NewWorldId, travelDest);
                    break;
                case 3:
                    actionButton.GetComponent<Animator>().ResetTrigger("up");
                    shop.SetActive(true);
                    shop.GetComponent<ShopSystem>().GetEqInfo();
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
        if(whatTrigger == 1)
        {
            GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().HideMissionsPanel();
        }
    }
}
