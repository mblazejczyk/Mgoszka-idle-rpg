using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeSystem : MonoBehaviour
{

    public int currentSize = 1;
    public GameObject PlusButton;
    public GameObject MinusButton;

    public GameObject SmallMap;
    public GameObject NormalMap;
    public GameObject BigMap;
    [Space(20)]
    public int[] knownPlaces; //jak zna miejsce, odsłania NPC i nazwy przeciwników danej krainy. Miejsce w tabeli to ID krainy, 0 to nieznana, 1 to znana
    public int[] knownItems; //jak zna przedmiot to przypisuje go przeciwnikowi. Miejsce w tabeli to ID przedmiotu, 0 to nie poznany a 1 to poznany
    [Space(10)]
    public Text[] ItemsFromEny;

    public GameObject[] NPCs;
    public GameObject[] NPCsUnknown;

    public GameObject[] Enymies;
    public GameObject[] EnymiesUnknown;
    [Space(10)]
    public GameObject[] k1Markers;
    public GameObject[] k2Markers;
    public GameObject[] k3Markers;
    public GameObject[] k4Markers;
    public GameObject[] k5Markers;
    public GameObject[] k6Markers;

    private void FixedUpdate()
    {
        RefreshMapData();
    }
    public void RefreshMapData()
    {


        if (GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().areItemsKnown == 1)
        {
            for (int i = 0; i < knownItems.Length; i++)
            {
                knownItems[i] = 1;
            }
            for (int i = 0; i < knownPlaces.Length; i++)
            {
                knownPlaces[i] = 1;
            }
        }

        for (int i = 0; i < knownPlaces.Length; i++)
        {
            if(knownPlaces[i] == 1)
            {
                NPCs[i].SetActive(true);
                NPCsUnknown[i].SetActive(false);
                Enymies[i].SetActive(true);
                EnymiesUnknown[i].SetActive(false);
            }
            else
            {
                NPCs[i].SetActive(false);
                NPCsUnknown[i].SetActive(true);
                Enymies[i].SetActive(false);
                EnymiesUnknown[i].SetActive(true);
            }
        }

        switch (GameObject.FindGameObjectWithTag("controller").GetComponent<TimeCountingSystem>().CurrentWorld)
        {
            case 0:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(true);
                    k2Markers[i].SetActive(false);
                    k3Markers[i].SetActive(false);
                    k4Markers[i].SetActive(false);
                    k5Markers[i].SetActive(false);
                    k6Markers[i].SetActive(false);
                }
                break;
            case 1:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(false);
                    k2Markers[i].SetActive(true);
                    k3Markers[i].SetActive(false);
                    k4Markers[i].SetActive(false);
                    k5Markers[i].SetActive(false);
                    k6Markers[i].SetActive(false);
                }
                break;
            case 2:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(false);
                    k2Markers[i].SetActive(false);
                    k3Markers[i].SetActive(true);
                    k4Markers[i].SetActive(false);
                    k5Markers[i].SetActive(false);
                    k6Markers[i].SetActive(false);
                }
                break;
            case 3:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(false);
                    k2Markers[i].SetActive(false);
                    k3Markers[i].SetActive(false);
                    k4Markers[i].SetActive(true);
                    k5Markers[i].SetActive(false);
                    k6Markers[i].SetActive(false);
                }
                break;
            case 4:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(false);
                    k2Markers[i].SetActive(false);
                    k3Markers[i].SetActive(false);
                    k4Markers[i].SetActive(false);
                    k5Markers[i].SetActive(true);
                    k6Markers[i].SetActive(false);
                }
                break;
            case 5:
                for (int i = 0; i < k1Markers.Length; i++)
                {
                    k1Markers[i].SetActive(false);
                    k2Markers[i].SetActive(false);
                    k3Markers[i].SetActive(false);
                    k4Markers[i].SetActive(false);
                    k5Markers[i].SetActive(false);
                    k6Markers[i].SetActive(true);
                }
                break;
        }



        ItemsFromEny[0].text = "";

        foreach(GameObject gobj in GameObject.FindGameObjectWithTag("dzdzownica").GetComponent<enymieStats>().itemsDrop)
        {
            if(knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[0].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[0].text += "-???" + "\n";
            }
        }
        ItemsFromEny[0].text += "<color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("mrowka").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[0].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[0].text += "-???" + "\n";
            }
        }
        ItemsFromEny[0].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("zuk").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[0].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[0].text += "-???" + "\n";
            }
        }
        ItemsFromEny[0].text += "</color><color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("mucha").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[0].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[0].text += "-???" + "\n";
            }
        }
        ItemsFromEny[0].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("osa").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[0].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[0].text += "-???" + "\n";
            }
        }
        ItemsFromEny[0].text += "</color>";




        ItemsFromEny[1].text = "";

        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("wrobel").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[1].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[1].text += "-???" + "\n";
            }
        }
        ItemsFromEny[1].text += "<color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("jaszczurka").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[1].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[1].text += "-???" + "\n";
            }
        }
        ItemsFromEny[1].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("sroka").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[1].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[1].text += "-???" + "\n";
            }
        }
        ItemsFromEny[1].text += "</color><color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("orzel").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[1].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[1].text += "-???" + "\n";
            }
        }
        ItemsFromEny[1].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("wilk").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[1].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[1].text += "-???" + "\n";
            }
        }
        ItemsFromEny[1].text += "</color>";






        ItemsFromEny[4].text = "";

        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("ryba").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[4].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[4].text += "-???" + "\n";
            }
        }
        ItemsFromEny[4].text += "<color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("trujacazaba").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[4].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[4].text += "-???" + "\n";
            }
        }
        ItemsFromEny[4].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("delfin").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[4].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[4].text += "-???" + "\n";
            }
        }
        ItemsFromEny[4].text += "</color><color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("mewa").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[4].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[4].text += "-???" + "\n";
            }
        }
        ItemsFromEny[4].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("rekin").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[4].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[4].text += "-???" + "\n";
            }
        }
        ItemsFromEny[4].text += "</color>";






        ItemsFromEny[2].text = "";

        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("trujacyslimak").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[2].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[2].text += "-???" + "\n";
            }
        }
        ItemsFromEny[2].text += "<color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("sarna").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[2].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[2].text += "-???" + "\n";
            }
        }
        ItemsFromEny[2].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("mrowkojad").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[2].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[2].text += "-???" + "\n";
            }
        }
        ItemsFromEny[2].text += "</color><color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("zolw").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[2].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[2].text += "-???" + "\n";
            }
        }
        ItemsFromEny[2].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("waz").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[2].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[2].text += "-???" + "\n";
            }
        }
        ItemsFromEny[2].text += "</color>";






        ItemsFromEny[3].text = "";

        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("skorpion").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[3].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[3].text += "-???" + "\n";
            }
        }
        ItemsFromEny[3].text += "<color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("trujacypajak").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[3].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[3].text += "-???" + "\n";
            }
        }
        ItemsFromEny[3].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("grzechotnik").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[3].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[3].text += "-???" + "\n";
            }
        }
        ItemsFromEny[3].text += "</color><color=white>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("gepard").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[3].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[3].text += "-???" + "\n";
            }
        }
        ItemsFromEny[3].text += "</color><color=black>";
        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("sep").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[3].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[3].text += "-???" + "\n";
            }
        }
        ItemsFromEny[3].text += "</color>";



        ItemsFromEny[5].text = "";

        foreach (GameObject gobj in GameObject.FindGameObjectWithTag("deparn").GetComponent<enymieStats>().itemsDrop)
        {
            if (knownItems[gobj.GetComponent<itemParameters>().Id] == 1)
            {
                ItemsFromEny[5].text += "-" + gobj.GetComponent<itemParameters>().Name + "\n";
            }
            else
            {
                ItemsFromEny[5].text += "-???" + "\n";
            }
        }
        
    }


    public void Plus()
    {
        currentSize++;
    }
    public void Minus()
    {
        currentSize--;
    }
    public void SetToDefault()
    {
        currentSize = 1;
    }
    private void Update()
    {
        switch (currentSize)
        {
            case 0:
                SmallMap.SetActive(true);
                BigMap.SetActive(false);
                NormalMap.SetActive(false);
                MinusButton.GetComponent<Button>().interactable = false;
                break;
            case 1:
                MinusButton.GetComponent<Button>().interactable = true;
                PlusButton.GetComponent<Button>().interactable = true;
                NormalMap.SetActive(true);
                SmallMap.SetActive(false);
                BigMap.SetActive(false);
                break;
            case 2:
                BigMap.SetActive(true);
                SmallMap.SetActive(false);
                NormalMap.SetActive(false);
                PlusButton.GetComponent<Button>().interactable = false;
                break;
        }
    }
}
