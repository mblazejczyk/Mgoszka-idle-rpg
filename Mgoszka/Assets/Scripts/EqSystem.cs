using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqSystem : MonoBehaviour
{
    [Header("Itemy")]
    public Image[] eqItems;
    public GameObject[] items;

    // 0, 1, 2, 3, 4, 5, 6 - itemy zalozone!
    // 7-46 - itemy w eq
    // eqIds = [polozenie] + | + [itemId] + |
    public string[] eqIds;
    public Sprite emptyImg;
    public string[] outputId;
    [Header("Detale")]
    public GameObject details;
    public Image detailImage;
    public Text detailItem;
    public Text detailItemName;
    private string positionDetail;

    public Image[] equiped;
    public Sprite[] equipedEmpty;
    public GameObject itemDropped;
    public GameObject slotUsed;
    [Header("Limiting Eq")]
    public bool isLimitedEq;
    public GameObject[] ButtonsToLimit;
    [Space(20)]
    public Text useBtnText;
    [Space(5)]
    public GameObject AlfaTesterBadge;
    [Space(20)]
    public Image[] rareStarObject;
    public Color commonColor;
    public Color FineColor;
    public Color RareColor;
    public Color EpicColor;
    public Color LegendaryColor;
    public Color UltimateColor;
    public Color MythicColor;
    [Space(10)]
    public GameObject mapScirpt;
    [Space(10)]
    public bool isMoving = false;
    public GameObject WhileMovingPanel;
    public Button MoveButton;

    
    public void UpdateItems()
    {
        for (int j = 0; j < rareStarObject.Length; j++)
        {
            rareStarObject[j].color = commonColor;
        }
        for (int g = 0; g < 7; g++)
        {
            equiped[g].sprite = equipedEmpty[g];
            equiped[g].color = new Color(1, 1, 1, 0.5f);
        }

        for (int i = 7; i < 47; i++)
        {
            eqItems[i].sprite = emptyImg;
        }

        for (int i = 0; i < 47; i++)
        {
            

            if (eqIds[i] != "" && eqIds != null)
            {
                outputId = eqIds[i].Split('|');

                if (int.Parse(outputId[0]) <= 6)
                {
                    equiped[int.Parse(outputId[0])].color = new Color(1, 1, 1, 1f);
                }


                foreach (GameObject ob in items)
                {
                    if(ob.GetComponent<itemParameters>().Id.ToString() == outputId[1])
                    {
                        eqItems[int.Parse(outputId[0])].sprite = ob.GetComponent<Image>().sprite;
                        switch (ob.GetComponent<itemParameters>().RareId)
                        {
                            case 0:
                                rareStarObject[int.Parse(outputId[0])].color = commonColor;
                                break;
                            case 1:
                                rareStarObject[int.Parse(outputId[0])].color = FineColor;
                                break;
                            case 2:
                                rareStarObject[int.Parse(outputId[0])].color = RareColor;
                                break;
                            case 3:
                                rareStarObject[int.Parse(outputId[0])].color = EpicColor;
                                break;
                            case 4:
                                rareStarObject[int.Parse(outputId[0])].color = LegendaryColor;
                                break;
                            case 5:
                                rareStarObject[int.Parse(outputId[0])].color = UltimateColor;
                                break;
                            case 6:
                                rareStarObject[int.Parse(outputId[0])].color = MythicColor;
                                break;
                        }
                    }
                }


                
                
                outputId = null;
            }
            
            
            
        }
        
        Debug.Log("Item list updated");
    }

    public void MissionChecker()
    {
        for (int i = 0; i < eqIds.Length; i++)
        {
            string temp = eqIds[i];
            if(temp != "")
            {
                string[] s = temp.Split('|');
                Debug.Log(temp);
                foreach (GameObject obj in items)
                {
                    if (obj.GetComponent<itemParameters>().Id == int.Parse(s[1]) && obj.GetComponent<itemParameters>().isGivingProgress == true)
                    {
                        GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[obj.GetComponent<itemParameters>().ProgressId]++;
                    }
                }
            }
            

        }
    }

    public void AddItem(int itemId)
    {
        for (int i = 7; i < 48; i++)
        {
            mapScirpt.GetComponent<MapSizeSystem>().knownItems[itemId] = 1; //dodaje znajomość przedmiotu do systemu mapy

            if (i <= 46)
            {
                if (eqItems[i].sprite == emptyImg)
                {
                    for (int x = 0; x < eqIds.Length; x++)
                    {
                        if (eqIds[x] == "")
                        {
                            eqIds[x] = i + "|" + itemId.ToString("D1");
                            break;
                        }

                    }
                    foreach(GameObject ob in items)
                    {
                        if(ob.GetComponent<itemParameters>().Id == itemId && ob.GetComponent<itemParameters>().isGivingProgress == true)
                        {
                            GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[ob.GetComponent<itemParameters>().ProgressId]++;
                        }
                    }
                    UpdateItems();
                    break;
                }
                
            }
            else
            {
                itemDropped.SetActive(true);
            }
        }
    }

    public bool IsEqFull()
    {
        for (int i = 7; i < 48; i++)
        {

            if (i <= 46)
            {
                if (eqItems[i].sprite == emptyImg)
                {
                    break;
                }

            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void ShowDetails(int place)
    {
        if (isMoving == true)
        {
            MoveItem(place);
        }
        else
        {
            for (int i = 0; i < eqIds.Length; i++)
            {
                outputId = eqIds[i].Split('|');

                if (outputId[0] == place.ToString())
                {
                    details.SetActive(true);

                    foreach (GameObject ob in items)
                    {
                        if (ob.GetComponent<itemParameters>().Id.ToString() == outputId[1])
                        {
                            detailImage.sprite = ob.GetComponent<Image>().sprite;
                            detailItemName.text = ob.GetComponent<itemParameters>().Name;
                            detailItem.text = "";
                            int c = 1;
                            if (ob.GetComponent<itemParameters>().add_HP != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowe życie: " + ob.GetComponent<itemParameters>().add_HP + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_total_HP != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa pojemność życia: " + ob.GetComponent<itemParameters>().add_total_HP + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_energy != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa energia: " + ob.GetComponent<itemParameters>().add_energy + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_total_energy != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa poejmność energii: " + ob.GetComponent<itemParameters>().add_total_energy + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_ad != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowe obrażenia fizyczne: " + ob.GetComponent<itemParameters>().add_ad + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_md != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowe obrażenia magiczne: " + ob.GetComponent<itemParameters>().add_md + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_armor != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowy pancerz: " + ob.GetComponent<itemParameters>().add_armor + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_barier != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa bariera: " + ob.GetComponent<itemParameters>().add_barier + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_speed != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa prędkość: " + ob.GetComponent<itemParameters>().add_speed + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_dodge != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowy dodge: " + ob.GetComponent<itemParameters>().add_dodge + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_charyzma != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa charyzma: " + ob.GetComponent<itemParameters>().add_charyzma + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().add_escameChance != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa sznasa ucieczki: " + ob.GetComponent<itemParameters>().add_escameChance + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().CritChanse != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowa sznasa atak krytyczny: " + ob.GetComponent<itemParameters>().CritChanse + "\n";
                                c++;
                            }
                            if (ob.GetComponent<itemParameters>().xpBoostPercent != 0)
                            {
                                detailItem.text += c + ". " + "Dodatkowy % XP: " + ob.GetComponent<itemParameters>().xpBoostPercent + " na: " + ob.GetComponent<itemParameters>().xpBoostTimeInSec + "s";
                            }
                            if (ob.GetComponent<itemParameters>().isCommented == true)
                            {
                                detailItem.text += "<i><color=#d4d4d4>" + ob.GetComponent<itemParameters>().commentStr + "</color></i>";
                            }


                            if (isLimitedEq == true && ob.GetComponent<itemParameters>().isLimited == true)
                            {
                                foreach (GameObject obj in ButtonsToLimit)
                                {
                                    obj.GetComponent<Button>().interactable = false;
                                }
                            }
                            else
                            {
                                foreach (GameObject obj in ButtonsToLimit)
                                {
                                    obj.GetComponent<Button>().interactable = true;
                                }
                            }

                        }
                    }

                    if (int.Parse(outputId[0]) <= 6 && outputId[0] != "")
                    {
                        MoveButton.interactable = false;
                        useBtnText.text = "zdejmij";
                    }
                    else
                    {
                        MoveButton.interactable = true;
                        useBtnText.text = "użyj";
                    }

                    positionDetail = outputId[0];
                    outputId = null;
                }
            }
        }
    }

    public void TrashItem()
    {

        for (int i = 0; i < eqIds.Length; i++)
        {


            string[] str;

            str = eqIds[i].Split('|');


            if (str[0] == positionDetail)
            {
                //przedmiot jest w eq i bazie
                foreach (GameObject ob in items)
                {
                    if (ob.GetComponent<itemParameters>().Id.ToString() == str[1])
                    {
                        //znaleziono dokladny przedmiot w bazie przedmiotow

                        if (ob.GetComponent<itemParameters>().isEquickable == true && int.Parse(str[0]) <= 6)
                        {
                            
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP -= ob.GetComponent<itemParameters>().add_total_HP;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP -= ob.GetComponent<itemParameters>().add_HP;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy -= ob.GetComponent<itemParameters>().add_energy;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy -= ob.GetComponent<itemParameters>().add_total_energy;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD -= ob.GetComponent<itemParameters>().add_ad;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD -= ob.GetComponent<itemParameters>().add_md;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor -= ob.GetComponent<itemParameters>().add_armor;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().bariera -= ob.GetComponent<itemParameters>().add_barier;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed -= ob.GetComponent<itemParameters>().add_speed;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge -= ob.GetComponent<itemParameters>().add_dodge;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma -= ob.GetComponent<itemParameters>().add_charyzma;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape -= ob.GetComponent<itemParameters>().add_escameChance;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance -= ob.GetComponent<itemParameters>().CritChanse;
                        }
                    }
                }


                eqIds[i] = "";
                Debug.Log("wyrzucono");
                break;
            }
        }
        UpdateItems();
    }

    public void MoveItem(int newPlace)
    {

        if (isMoving == false)
        {
            isMoving = true;
            WhileMovingPanel.SetActive(true);
            details.SetActive(false);
        }
        else
        {
            int conv = newPlace;
            //conv += 6;

            for (int i = 0; i < eqIds.Length; i++)
            {
                string f;
                string[] g;
                g = eqIds[i].Split('|');


                if (g[0] == positionDetail)
                {
                    for (int x = 0; x < eqIds.Length; x++)
                    {
                        string[] v;
                        v = eqIds[x].Split('|');
                        if (v[0] == conv.ToString())
                        {
                            eqIds[x] = positionDetail + "|" + v[1];
                        }
                    }
                    f = conv + "|" + g[1];
                    eqIds[i] = f;
                    isMoving = false;
                    WhileMovingPanel.SetActive(false);
                    break;
                }
            }
        }
        UpdateItems();

    }

    public void UseItem()
    {
        for (int i = 0; i < eqIds.Length; i++)
        {
            string[] str;
            str = eqIds[i].Split('|');

            

            if(str[0] == positionDetail)
            {
                //przedmiot jest w eq i bazie
                foreach (GameObject ob in items)
                {
                    if (ob.GetComponent<itemParameters>().Id.ToString() == str[1])
                    {
                        //znaleziono dokladny przedmiot w bazie przedmiotow
                        if(ob.GetComponent<itemParameters>().Id.ToString() == "46")
                        {
                            AlfaTesterBadge.SetActive(true);
                            gameObject.SetActive(false);
                            break;
                        }
                        if(ob.GetComponent<itemParameters>().Id == 79)
                        {
                            break;
                        }
                        if(ob.GetComponent<itemParameters>().isEquickable == false)
                        {
                            eqIds[i] = "";
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP += ob.GetComponent<itemParameters>().add_total_HP;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP += ob.GetComponent<itemParameters>().add_HP;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy += ob.GetComponent<itemParameters>().add_energy;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy += ob.GetComponent<itemParameters>().add_total_energy;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD += ob.GetComponent<itemParameters>().add_ad;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD += ob.GetComponent<itemParameters>().add_md;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor += ob.GetComponent<itemParameters>().add_armor;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().bariera += ob.GetComponent<itemParameters>().add_barier;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed += ob.GetComponent<itemParameters>().add_speed;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge += ob.GetComponent<itemParameters>().add_dodge;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma += ob.GetComponent<itemParameters>().add_charyzma;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape += ob.GetComponent<itemParameters>().add_escameChance;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance += ob.GetComponent<itemParameters>().CritChanse;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().activeRune += ob.GetComponent<itemParameters>().runeId;

                            if (ob.GetComponent<itemParameters>().xpBoostPercent != 0)
                            {
                                if(ob.GetComponent<itemParameters>().xpBoostTimeInSec != 0)
                                {
                                    GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().AddXpBoost(ob.GetComponent<itemParameters>().xpBoostTimeInSec, ob.GetComponent<itemParameters>().xpBoostPercent + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost);
                                }
                                else
                                {
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost += ob.GetComponent<itemParameters>().xpBoostPercent;
                                }


                                
                            }
                            UpdateItems();
                            break;
                        }
                        else
                        {
                            if (int.Parse(str[0]) <= 6 )
                            {
                                
                                equiped[ob.GetComponent<itemParameters>().eqPlace].color = new Color(1, 1, 1, 0.5f);
                                eqIds[i] = "";
                                AddItem(int.Parse(str[1]));
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP -= ob.GetComponent<itemParameters>().add_total_HP;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP -= ob.GetComponent<itemParameters>().add_HP;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy -= ob.GetComponent<itemParameters>().add_energy;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy -= ob.GetComponent<itemParameters>().add_total_energy;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD -= ob.GetComponent<itemParameters>().add_ad;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD -= ob.GetComponent<itemParameters>().add_md;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor -= ob.GetComponent<itemParameters>().add_armor;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().bariera -= ob.GetComponent<itemParameters>().add_barier;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed -= ob.GetComponent<itemParameters>().add_speed;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge -= ob.GetComponent<itemParameters>().add_dodge;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma -= ob.GetComponent<itemParameters>().add_charyzma;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape -= ob.GetComponent<itemParameters>().add_escameChance;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance -= ob.GetComponent<itemParameters>().CritChanse;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().activeRune -= ob.GetComponent<itemParameters>().runeId;
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost -= ob.GetComponent<itemParameters>().xpBoostPercent;
                            }
                            else
                            {
                                if(equiped[ob.GetComponent<itemParameters>().eqPlace].sprite == equipedEmpty[ob.GetComponent<itemParameters>().eqPlace])
                                {
                                    eqIds[i] = ob.GetComponent<itemParameters>().eqPlace + "|" + str[1];
                                    equiped[ob.GetComponent<itemParameters>().eqPlace].color = new Color(1, 1, 1, 1);
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP += ob.GetComponent<itemParameters>().add_total_HP;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP += ob.GetComponent<itemParameters>().add_HP;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy += ob.GetComponent<itemParameters>().add_energy;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy += ob.GetComponent<itemParameters>().add_total_energy;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().AD += ob.GetComponent<itemParameters>().add_ad;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MD += ob.GetComponent<itemParameters>().add_md;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Armor += ob.GetComponent<itemParameters>().add_armor;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().bariera += ob.GetComponent<itemParameters>().add_barier;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Speed += ob.GetComponent<itemParameters>().add_speed;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Dodge += ob.GetComponent<itemParameters>().add_dodge;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma += ob.GetComponent<itemParameters>().add_charyzma;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().escape += ob.GetComponent<itemParameters>().add_escameChance;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().critChance += ob.GetComponent<itemParameters>().CritChanse;
                                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().activeRune += ob.GetComponent<itemParameters>().runeId;




                                    if (ob.GetComponent<itemParameters>().xpBoostPercent != 0)
                                    {
                                        if (ob.GetComponent<itemParameters>().xpBoostTimeInSec != 0)
                                        {
                                            GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().AddXpBoost(ob.GetComponent<itemParameters>().xpBoostTimeInSec, ob.GetComponent<itemParameters>().xpBoostPercent + GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost);
                                        }
                                        else
                                        {
                                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost += ob.GetComponent<itemParameters>().xpBoostPercent;
                                        }
                                    }
                                    UpdateItems();
                                }
                                else
                                {
                                    slotUsed.SetActive(true);

                                }

                                
                            }

                        }
                    }
                }

                break;
            }

                
            
        }
        UpdateItems();
    }
}
