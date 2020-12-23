using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class TimeCountingSystem : MonoBehaviour
{
    
    public DateTime DP_podroz;
    public DateTime DK_podroz;
    public Transform dest;
    public GameObject inTravel;
    public Text timeLeft;
    public bool isDone;
    [Space(10)]
    public DateTime DP_xpBoost;
    public DateTime DK_xpBoost;
    public GameObject Xp_boost_info_obj;
    public Text XP_boost_Text;
    public float currentXpBoost;
    public bool isDoneXp;
    [Space(10)]
    public int CurrentWorld;
    public GameObject[] TravelPanels;
    [Space(20)]
    public DateTime DP_dead;
    public DateTime DK_dead;
    public GameObject DeadPanel;
    public Text timeToRessurect;
    public bool isAlive;
    [Space(20)]
    public DateTime DP_energyRest;
    public DateTime DK_energyRest;
    public GameObject EnergyRestGOInfo;
    public Text EnergyRestText;
    public bool isEnergyRestDone;
    [Space(20)]
    //daily missions
    public DateTime lastMissionClimed;
    public DateTime lastMissionGenerated;
    public GameObject rainFilter;
    [Space(10)]
    public GameObject mapSystem;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWorldInfo();
        mapSystem.GetComponent<MapSizeSystem>().knownPlaces[CurrentWorld] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnergyRestText();
        UpdateXpText();
        UpdateDeadText();
        UpdateTravelText();

        if(CurrentWorld == 2)
        {
            rainFilter.SetActive(true);
        }
        else
        {
            rainFilter.SetActive(false);
        }

        int hPassed = (int)(DateTime.Now - lastMissionGenerated).TotalHours;
        
        if(hPassed >= 24)
        {
            lastMissionGenerated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            this.gameObject.GetComponent<MissionSystem>().StartDayMission();
        }
        int hCPassed = (int)(DateTime.Now - lastMissionClimed).TotalHours;
        if (hCPassed >= 24)
        {
            this.gameObject.GetComponent<MissionSystem>().Streak = 1;
        }

        if (isDone == false && DateTime.Now > DK_podroz)
        {
            
            inTravel.SetActive(false);
            isDone = true;
            mapSystem.GetComponent<MapSizeSystem>().knownPlaces[CurrentWorld] = 1;
        }
        if(DateTime.Now < DK_podroz)
        {
            inTravel.SetActive(true);
        }

        if (isDoneXp == false && DateTime.Now > DK_xpBoost)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost -= currentXpBoost;
            isDoneXp = true;
            Xp_boost_info_obj.SetActive(false);
        }

        if(isDoneXp == false)
        {
            Xp_boost_info_obj.SetActive(true);
        }

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy == GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy)
        {
            EnergyRestGOInfo.SetActive(false);
            isEnergyRestDone = true;
        }

        if (isEnergyRestDone == false && DateTime.Now > DK_energyRest)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy;
            isEnergyRestDone = true;
            EnergyRestGOInfo.SetActive(false);
        }

        if (isEnergyRestDone == false)
        {
            EnergyRestGOInfo.SetActive(true);
        }

        if (DateTime.Now < DK_xpBoost)
        {
            Xp_boost_info_obj.SetActive(true);
        }


        if (isAlive == false && DateTime.Now > DK_dead)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxHP;
            isAlive = true;
            DeadPanel.SetActive(false);
        }

        if(DateTime.Now < DK_dead)
        {
            DeadPanel.SetActive(true);
        }
        
    }

    public void MissionClimed()
    {
        lastMissionClimed = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
    }
    public void Travel(GameObject place, int TimeInSec)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = place.transform.position;
        DP_podroz = DateTime.Now;
        DK_podroz = DP_podroz.AddSeconds(TimeInSec);
        inTravel.SetActive(true);
        isDone = false;
        mapSystem.GetComponent<MapSizeSystem>().knownPlaces[CurrentWorld] = 1;

        var channel = new AndroidNotificationChannel()
        {
            Id = "travel_id",
            Name = "Travel",
            Importance = Importance.High,
            Description = "Notifications for traveling",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Jestes na miejscu";
        notification.Text = "Dotarles do docelowej lokalizacji";
        notification.LargeIcon = "mgoszka_large_icon";
        notification.SmallIcon = "mgoszka_small_icon";
        notification.FireTime = DK_podroz;

        AndroidNotificationCenter.SendNotification(notification, "travel_id");
    }

    public void Killed(int TimeInSec)
    {
        DP_dead = DateTime.Now;
        DK_dead = DP_dead.AddSeconds(TimeInSec);
        DeadPanel.SetActive(true);
        isAlive = false;

        var channel = new AndroidNotificationChannel()
        {
            Id = "death_id",
            Name = "Death",
            Importance = Importance.High,
            Description = "Notifications for resurecting",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Znow stoisz na nogach!";
        notification.Text = "Skonczyl sie twoj czas odrodzenia. Mozesz wrocic do gry!";
        notification.LargeIcon = "mgoszka_large_icon";
        notification.SmallIcon = "mgoszka_small_icon";
        notification.FireTime = DK_dead;

        AndroidNotificationCenter.SendNotification(notification, "death_id");
    }

    public void MoveToWorld(int WorldID)
    {
        CurrentWorld = WorldID;
        UpdateWorldInfo();
    }

    public void UpdateWorldInfo()
    {
        for (int i = 0; i < TravelPanels.Length; i++)
        {
            if(TravelPanels[i] != null)
            {
                TravelPanels[i].SetActive(false);
            }
            
        }
        TravelPanels[CurrentWorld].SetActive(true);
    }


    public void AddXpBoost(int TimeInSec, float xpBoost)
    {

        if (isDoneXp == false && DateTime.Now < DK_xpBoost)
        {
            Xp_boost_info_obj.SetActive(true);
            DP_xpBoost = DateTime.Now;
            DK_xpBoost = DP_xpBoost.AddSeconds(TimeInSec);
            currentXpBoost = xpBoost;
            isDoneXp = false;
        }
        else
        {
            Xp_boost_info_obj.SetActive(true);
            DP_xpBoost = DateTime.Now;
            DK_xpBoost = DP_xpBoost.AddSeconds(TimeInSec);
            currentXpBoost = xpBoost;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().xpBoost += currentXpBoost;
            isDoneXp = false;
        }

    }


    public void RestoreEnergy()
    {
        if(isEnergyRestDone == false && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Energy != GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().maxEnergy)
        {
            return;
        }
        EnergyRestGOInfo.SetActive(true);
        DP_energyRest = DateTime.Now;
        DK_energyRest = DP_energyRest.AddSeconds(1800);

        var channel = new AndroidNotificationChannel()
        {
            Id = "energy_id",
            Name = "Energy",
            Importance = Importance.High,
            Description = "Notifications for energy",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var notification = new AndroidNotification();
        notification.Title = "Znow masz sile";
        notification.Text = "Twoja energia sie odnowila!";
        notification.LargeIcon = "mgoszka_large_icon";
        notification.SmallIcon = "mgoszka_small_icon";
        notification.FireTime = DK_energyRest;

        AndroidNotificationCenter.SendNotification(notification, "energy_id");

        isEnergyRestDone = false;
    }

    void UpdateTravelText()
    {
        int a = (int)(DK_podroz - DateTime.Now).TotalSeconds;
        timeLeft.text = "Pozostalo: ";
        TimeSpan result = TimeSpan.FromSeconds(a);
        timeLeft.text += result.ToString("hh':'mm':'ss");
        
    }

    void UpdateXpText()
    {
        int a = (int)(DK_xpBoost - DateTime.Now).TotalSeconds;
        XP_boost_Text.text = "Pozostalo: ";
        TimeSpan result = TimeSpan.FromSeconds(a);
        XP_boost_Text.text += result.ToString("hh':'mm':'ss");
    }

    void UpdateEnergyRestText()
    {
        int a = (int)(DK_energyRest - DateTime.Now).TotalSeconds;
        
        TimeSpan result = TimeSpan.FromSeconds(a);
        EnergyRestText.text = result.ToString("hh':'mm':'ss");
    }

    void UpdateDeadText()
    {
        int a = (int)(DK_dead - DateTime.Now).TotalSeconds;
        timeToRessurect.text = "Pozostalo: ";
        TimeSpan result = TimeSpan.FromSeconds(a);
        timeToRessurect.text += result.ToString("hh':'mm':'ss");

        
    }

    
}
