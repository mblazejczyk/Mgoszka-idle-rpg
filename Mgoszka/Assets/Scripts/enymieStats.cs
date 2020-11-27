using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enymieStats : MonoBehaviour
{
    public int enyId;
    [HideInInspector] public float AD;
    [HideInInspector] public float MD;
    [HideInInspector] public float Armor;
    [HideInInspector] public float MagicBarier;
    [HideInInspector] public float DodgeChance;
    [HideInInspector] public float HP;
    [HideInInspector] public float CritChance;
    [Header("enymieStats")]
    public float AdMin;
    public float AdMax;
    [Space(5)]
    public float MdMin;
    public float MdMax;
    [Space(5)]
    public float ArmorMin;
    public float ArmorMax;
    [Space(5)]
    public float MagicBarierMin;
    public float MagicBarierMax;
    [Space(5)]
    public float DodgeChanceMin;
    public float DodgeChanceMax;
    [Space(5)]
    public float HpMin;
    public float HpMax;
    [Space(5)]
    public float minCritChance;
    public float maxCritChance;

    [Header("rewards")]
    public float xpRewardMax;
    public float xpRewardMin;
    [Space(5)]
    public float coinRewardMax;
    public float coinRewardMin;
    [Space(5)]
    public GameObject[] itemsDrop;
    public float[] itemDromRate;

    public GameObject[] droppedItems;
    [Space(10)]
    public bool isGivingProgress = false;
    public int progressId = 0;
    public int dailyProgressId = 0;
    [Space(10)]
    public int FightPlaceId = 0;
    // Start is called before the first frame update
    void Start()
    {
        AD = Random.Range(AdMin, AdMax);
        MD = Random.Range(MdMin, MdMax);
        Armor = Random.Range(ArmorMin, ArmorMax);
        MagicBarier = Random.Range(MagicBarierMin, MagicBarierMax);
        DodgeChance = Random.Range(DodgeChanceMin, DodgeChanceMax);
        HP = Random.Range(HpMin, HpMax);
        CritChance = Random.Range(minCritChance, maxCritChance);

        AD = Mathf.Round(AD * 100) / 100;
        MD = Mathf.Round(MD * 100) / 100;
        Armor = Mathf.Round(Armor * 100) / 100;
        MagicBarier = Mathf.Round(MagicBarier * 100) / 100;
        DodgeChance = Mathf.Round(DodgeChance * 100) / 100;
        HP = Mathf.Round(HP * 100) / 100;
        CritChance = Mathf.Round(CritChance * 100) / 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reward()
    {
        
        if(isGivingProgress == true)
        {
            GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[progressId]++;
        }
        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().missionDayliProgress[dailyProgressId]++;
        int index = 0;
        for (int i = 0; i < itemsDrop.Length; i++)
        {
            if (Random.value > itemDromRate[i] / 100) //not dropped
            {


            }
            else //dropped
            {
                droppedItems[index] = itemsDrop[i];
                index++;
            }
        }
        
        int Listing = 1;
        if (droppedItems != null)
        {
            for (int i = 0; i < droppedItems.Length; i++)
            {
                if (droppedItems[i] != null && droppedItems[i].ToString() != "")
                {
                    GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().rewardList.text += Listing + ". " + droppedItems[i].GetComponent<itemParameters>().Name + "\n";
                    GameObject.FindGameObjectWithTag("controller").GetComponent<UiController>().Ekwipunek.GetComponent<EqSystem>().AddItem(droppedItems[i].GetComponent<itemParameters>().Id);
                }
            }

        }

        float xpDropped = 0;
        float coinsDropped = 0;
        xpDropped = Random.Range(xpRewardMin, xpRewardMax);
        coinsDropped = Random.Range(coinRewardMin, coinRewardMax);

        xpDropped = Mathf.Round(xpDropped * 100) / 100;
        coinsDropped = Mathf.Round(coinsDropped * 100) / 100;
        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().missionDayliProgress[1] += coinsDropped;
        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().missionDayliProgress[0] += xpDropped;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().currentXP += xpDropped;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins += coinsDropped;

        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().rewardList.text += Listing + ". " + "Zdobyty EXP: " + xpDropped + "\n";
        Listing++;
        GameObject.FindGameObjectWithTag("enymieBattle").GetComponent<Fight>().rewardList.text += Listing + ". " + "Zdobyte złoto: " + coinsDropped;

        Destroy(this.gameObject);
    }
}
