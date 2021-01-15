using UnityEngine;
using UnityEngine.UI;

public class MissionSystem : MonoBehaviour
{
    public bool isOnMission;
    public bool isMissionDone;
    public string[] thingToDo;
    public int[] progress;
    public int currentMissionId;

    public GameObject[] PersonalMissions;
    public GameObject[] MissionById;
    
    public int[] missionsProgress = {1, 1 };
    public Text progressText;
    public GameObject progressPanel;
    public Button[] claimReward;
    public GameObject[] thanking;
    public Button[] AcceptButtons;

    [Space(35)]
    [Header("Dzienne misje")]
    public int Streak = 1;
    public string[] missionNameToDo;
    public float[] missionDayliProgress;
    public float[] missionGoal;
    public int TodaysMissionId;
    public GameObject DmissionDone;
    public bool isMissionClimed = false;
    public Text DProgressText;
    public Text rewardText;
    public GameObject DMissionProgressPanel;

    [Space(20)]
    public Button BossPlaceButton;
    public GameObject Brama;
    public Button stickBuyButton;

    [Space(10)]
    public GameObject eqSys;

    [Space(10)]
    public GameObject MapScript;
    private TranslationSystem translationObject;

    private void Awake()
    {
        translationObject = GameObject.FindGameObjectWithTag("controller").GetComponent<TranslationSystem>();
    }
    public void CancelMission()
    {
        isOnMission = false;
        isMissionDone = false;
        for (int i = 0; i < progress.Length; i++)
        {
            progress[i] = 0;
        }
        currentMissionId = 99999;
        progressText.text = "";
        GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
    }

    void Update()
    {
        if(isOnMission == true)
        {
            foreach (Button b in AcceptButtons)
            {
                b.interactable = false;
            }
        }
        else
        {
            foreach (Button b in AcceptButtons)
            {
                b.interactable = true;
            }
            progressText.text = "";
        }

        if(missionDayliProgress[TodaysMissionId] >= missionGoal[TodaysMissionId] && isMissionClimed == false)
        {
            DmissionDone.SetActive(true);
            isMissionClimed = true;
            this.gameObject.GetComponent<TimeCountingSystem>().MissionClimed();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().currentXP += 50 * Streak;
            if (Streak < 5)
            {
                Streak++;
            }
        }

        progress[9] = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Level;

        int[] killingMission = { 0, 1, 2, 3, 4, 17, 18, 19, 20, 21, 31, 32, 33, 34, 35, 37, 38, 39, 36, 49, 50, 51, 52, 53, 61 };

        if (currentMissionId == 93)
        {
            for (int i = 0; i < progress.Length; i++)
            {
                foreach (int x in killingMission)
                {
                    if (i == x && progress[i] >= 1)
                    {
                        progress[62]++;
                    }
                }
            }
        }

        switch (currentMissionId)
        {
            case 1:
                if (progress[0] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 2:
                if (progress[2] >= 8)
                {
                    isMissionDone = true;
                }
                break;
            case 3:
                if (progress[0] >= 10 && progress[2] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 4:
                if (progress[7] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 5:

                isMissionDone = true;
                break;
            case 6:
                if (progress[5] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 7:
                if (progress[1] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 8:
                if (progress[8] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 9:
                if (progress[9] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 10:
                if (progress[4] >= 3)
                {
                    isMissionDone = true;
                }
                break;
            case 11:
                if (progress[6] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 12:
                isMissionDone = true;
                break;
            case 13:
                if (progress[10] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 14:
                if (progress[11] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 15:
                if (progress[12] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 16:
                if (progress[9] >= 15)
                {
                    isMissionDone = true;
                }
                break;
            case 17:
                if (progress[13] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 18:
                if (progress[14] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 19:
                if (progress[15] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 20:
                if (progress[16] >= 100)
                {
                    isMissionDone = true;
                }
                break;
            case 21:
                if (progress[17] >= 3)
                {
                    isMissionDone = true;
                }
                break;
            case 22:
                isMissionDone = true;
                break;
            case 23:
                if (progress[18] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 24:
                if (progress[19] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 25:
                if (progress[18] >= 15 && progress[19] >= 15)
                {
                    isMissionDone = true;
                }
                break;
            case 26:
                if (progress[20] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 27:
                if (progress[20] >= 15 && progress[18] >= 20 && progress[19] >= 20)
                {
                    isMissionDone = true;
                }
                break;
            case 28:
                if (progress[4] >= 20)
                {
                    isMissionDone = true;
                }
                break;
            case 29:
                if (progress[21] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 30:
                if (progress[17] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 31:
                if (progress[18] >= 1 && progress[19] >= 1 && progress[20] >= 1 && progress[21] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 32:
                isMissionDone = true;
                break;
            case 33:
                if (progress[22] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 34:
                if (progress[23] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 35:
                if (progress[24] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 36:
                if (progress[25] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 37:
                if (progress[26] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 38:
                if (progress[27] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 39:
                if (progress[28] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 40:
                if (progress[29] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 41:
                if (progress[30] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 42:
                isMissionDone = true;
                break;
            case 43:
                if (progress[31] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 44:
                if (progress[0] >= 25)
                {
                    isMissionDone = true;
                }
                break;
            case 45:
                if (progress[32] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 46:
                if (progress[31] >= 50)
                {
                    isMissionDone = true;
                }
                break;
            case 47:
                if (progress[33] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 48:
                if (progress[17] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 49:
                if (progress[34] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 50:
                if (progress[22] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 51:
                if (progress[35] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 52:
                isMissionDone = true;
                break;
            case 53:
                if (progress[36] >= 7)
                {
                    isMissionDone = true;
                }
                break;
            case 54:
                if (progress[37] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 55:
                if (progress[38] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 56:
                if (progress[36] >= 15 && progress[38] >= 15)
                {
                    isMissionDone = true;
                }
                break;
            case 57:
                if (progress[2] >= 25)
                {
                    isMissionDone = true;
                }
                break;
            case 58:
                if (progress[35] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 59:
                if (progress[39] >= 3)
                {
                    isMissionDone = true;
                }
                break;
            case 60:
                if (progress[36] >= 1 && progress[37] >= 1 && progress[38] >= 1 && progress[39] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 61:
                if (progress[61] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 62:
                isMissionDone = true;
                break;
            case 63:
                if (progress[40] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 64:
                if (progress[41] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 65:
                if (progress[42] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 66:
                if (progress[43] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 67:
                if (progress[44] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 68:
                if (progress[45] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 69:
                if (progress[46] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 70:
                if (progress[47] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 71:
                if (progress[48] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 72:
                isMissionDone = true;
                break;
            case 73:
                if (progress[49] >= 5)
                {
                    isMissionDone = true;
                }
                break;
            case 74:
                if (progress[49] >= 15)
                {
                    isMissionDone = true;
                }
                break;
            case 75:
                if (progress[50] >= 3)
                {
                    isMissionDone = true;
                }
                break;
            case 76:
                if (progress[9] >= 15)
                {
                    isMissionDone = true;
                }
                break;
            case 77:
                if (progress[51] >= 8)
                {
                    isMissionDone = true;
                }
                break;
            case 78:
                if (progress[52] >= 2)
                {
                    isMissionDone = true;
                }
                break;
            case 79:
                if (progress[51] >= 10 && progress[50] >= 10)
                {
                    isMissionDone = true;
                }
                break;
            case 80:
                if (progress[49] >= 1 && progress[50] >= 1 && progress[51] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 81:
                if (progress[53] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 82:
                isMissionDone = true;
                break;
            case 83:
                if (progress[54] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 84:
                if (progress[55] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 85:
                if (progress[56] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 86:
                if (progress[57] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 87:
                if (progress[58] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 88:
                if (progress[59] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 89:
                if (progress[60] >= 1)
                {
                    isMissionDone = true;
                }
                break;
            case 90:
                if (progress[24] >= 8)
                {
                    isMissionDone = true;
                }
                break;
            case 91:
                if (progress[9] >= 30)
                {
                    isMissionDone = true;
                }
                break;
            case 92:
                isMissionDone = true;
                break;
            case 93:
                if (progress[62] >= 25)
                {
                    isMissionDone = true;
                }
                break;

        }

        progress[62] = 0;

        if(isMissionDone == true)
        {
            claimReward[currentMissionId - 1].interactable = true;
        }
        else
        {
            foreach(Button b in claimReward)
            {
                b.interactable = false;
            }
        }


        if(missionsProgress[10] == 94)
        {
            Brama.SetActive(false);
            BossPlaceButton.interactable = true;
        }else if(missionsProgress[10] == 95)
        {
            Brama.SetActive(false);
            BossPlaceButton.interactable = false;
        }
        else
        {
            Brama.SetActive(true);
            BossPlaceButton.interactable = false;
        }
        if(currentMissionId == 8)
        {
            stickBuyButton.interactable = true;
        }
        else
        {
            stickBuyButton.interactable = false;
        }
    }

    public void ShowMissionProgress()
    {
        DMissionProgressPanel.SetActive(true);
        progressPanel.SetActive(true);

        int[] killingMission = { 0, 1, 2, 3, 4, 17, 18, 19, 20, 21, 31, 32, 33, 34, 35, 37, 38, 39, 36, 49, 50, 51, 52, 53, 61 };
        for (int i = 0; i < progress.Length; i++)
        {
            foreach (int x in killingMission)
            {
                if (i == x && progress[i] >= 1)
                {
                    progress[62]++;
                }
            }
        }

        switch (currentMissionId)
        {
            case 1:
                progressText.text = translationObject.GetMissionText(0) + ": " + progress[0] + "/5";

                break;
            case 2:
                progressText.text = translationObject.GetMissionText(2) + ": " + progress[2] + "/8";
                break;
            case 3:
                progressText.text = translationObject.GetMissionText(0) + ": " + progress[0] + "/10" + "\n" + translationObject.GetMissionText(2) + ": " + progress[2] + "/10";
                break;
            case 4:
                progressText.text = translationObject.GetMissionText(7) + ": " + progress[7] + "/1";
                break;
            case 5:
                progressText.text = translationObject.GetText(41);
                break;
            case 6:
                progressText.text = translationObject.GetMissionText(5) + ": " + progress[5] + "/5";
                break;
            case 7:
                progressText.text = translationObject.GetMissionText(1) + ": " + progress[1] + "/10";
                break;
            case 8:
                progressText.text = translationObject.GetMissionText(8) + ": " + progress[8] + "/1";
                break;
            case 9:
                progressText.text = translationObject.GetMissionText(9) + ": " + progress[9] + "/5";
                break;
            case 10:
                progressText.text = translationObject.GetMissionText(4) + ": " + progress[4] + "/3";
                break;
            case 11:
                progressText.text = translationObject.GetMissionText(6) + ": " + progress[6] + "/1";
                break;
            case 12:
                progressText.text = translationObject.GetText(41);
                break;
            case 13:
                progressText.text = translationObject.GetMissionText(10) + ": " + progress[10] + "/1";
                break;
            case 14:
                progressText.text = translationObject.GetMissionText(11) + ": " + progress[11] + "/1";
                break;
            case 15:
                progressText.text = translationObject.GetMissionText(12) + ": " + progress[12] + "/1";
                break;
            case 16:
                progressText.text = translationObject.GetMissionText(9) + ": " + progress[9] + "/15";
                break;
            case 17:
                progressText.text = translationObject.GetMissionText(13) + ": " + progress[13] + "/1";
                break;
            case 18:
                progressText.text = translationObject.GetMissionText(14) + ": " + progress[14] + "/1";
                break;
            case 19:
                progressText.text = translationObject.GetMissionText(15) + ": " + progress[15] + "/1";
                break;
            case 20:
                progressText.text = translationObject.GetMissionText(16) + ": " + progress[16] + "/100";
                break;
            case 21:
                progressText.text = translationObject.GetMissionText(17) + ": " + progress[17] + "/3";
                break;
            case 22:
                progressText.text = translationObject.GetText(41);
                break;
            case 23:
                progressText.text = translationObject.GetMissionText(18) + ": " + progress[18] + "/10";
                break;
            case 24:
                progressText.text = translationObject.GetMissionText(19) + ": " + progress[19] + "/10";
                break;
            case 25:
                progressText.text = translationObject.GetMissionText(18) + ": " + progress[18] + "/15" + "\n" + 
                    translationObject.GetMissionText(19) + ": " + progress[19] + "/15";
                break;
            case 26:
                progressText.text = translationObject.GetMissionText(20) + ": " + progress[20] + "/5";
                break;
            case 27:
                progressText.text = translationObject.GetMissionText(20) + ": " + progress[20] + "/15" + "\n" + 
                    translationObject.GetMissionText(18) + ": " + progress[18] + "/20" + "\n" + 
                    translationObject.GetMissionText(19) + ": " + progress[19] + "/20";
                break;
            case 28:
                progressText.text = translationObject.GetMissionText(4) + ": " + progress[4] + "/20";
                break;
            case 29:
                progressText.text = translationObject.GetMissionText(21) + ": " + progress[21] + "/1";
                break;
            case 30:
                progressText.text = translationObject.GetMissionText(17) + ": " + progress[17] + "/5";
                break;
            case 31:
                progressText.text = translationObject.GetMissionText(18) + ": " + progress[18] + "/1" + "\n" + 
                    translationObject.GetMissionText(19) + ": " + progress[19] + "/1" + "\n" + 
                    translationObject.GetMissionText(20) + ": " + progress[20] + "/1" + "\n" + 
                    translationObject.GetMissionText(21) + ": " + progress[21] + "/1";
                break;
            case 32:
                progressText.text = translationObject.GetText(41);
                break;
            case 33:
                progressText.text = translationObject.GetMissionText(22) + ": " + progress[22] + "/1";
                break;
            case 34:
                progressText.text = translationObject.GetMissionText(23) + ": " + progress[23] + "/1";
                break;
            case 35:
                progressText.text = translationObject.GetMissionText(24) + ": " + progress[24] + "/5";
                break;
            case 36:
                progressText.text = translationObject.GetMissionText(25) + ": " + progress[25] + "/1";
                break;
            case 37:
                progressText.text = translationObject.GetMissionText(26) + ": " + progress[26] + "/1";
                break;
            case 38:
                progressText.text = translationObject.GetMissionText(27) + ": " + progress[27] + "/1";
                break;
            case 39:
                progressText.text = translationObject.GetMissionText(28) + ": " + progress[28] + "/1";
                break;
            case 40:
                progressText.text = translationObject.GetMissionText(29) + ": " + progress[29] + "/1";
                break;
            case 41:
                progressText.text = translationObject.GetMissionText(30) + ": " + progress[30] + "/1";
                break;
            case 42:
                progressText.text = translationObject.GetText(41);
                break;
            case 43:
                progressText.text = translationObject.GetMissionText(31) + ": " + progress[31] + "/10";
                break;
            case 44:
                progressText.text = translationObject.GetMissionText(0) + ": " + progress[0] + "/25";
                break;
            case 45:
                progressText.text = translationObject.GetMissionText(32) + ": " + progress[32] + "/10";
                break;
            case 46:
                progressText.text = translationObject.GetMissionText(31) + ": " + progress[31] + "/50";
                break;
            case 47:
                progressText.text = translationObject.GetMissionText(33) + ": " + progress[33] + "/5";
                break;
            case 48:
                progressText.text = translationObject.GetMissionText(17) + ": " + progress[17] + "/10";
                break;
            case 49:
                progressText.text = translationObject.GetMissionText(34) + ": " + progress[34] + "/5";
                break;
            case 50:
                progressText.text = translationObject.GetMissionText(22) + ": " + progress[22] + "/1";
                break;
            case 51:
                progressText.text = translationObject.GetMissionText(35) + ": " + progress[35] + "/1";
                break;
            case 52:
                progressText.text = translationObject.GetText(41);
                break;
            case 53:
                progressText.text = translationObject.GetMissionText(36) + ": " + progress[36] + "/7";
                break;
            case 54:
                progressText.text = translationObject.GetMissionText(37) + ": " + progress[37] + "/5";
                break;
            case 55:
                progressText.text = translationObject.GetMissionText(38) + ": " + progress[38] + "/10";
                break;
            case 56:
                progressText.text = translationObject.GetMissionText(36) + ": " + progress[36] + "/15" + "\n" + translationObject.GetMissionText(38) + ": " + progress[38] + "/15";
                break;
            case 57:
                progressText.text = translationObject.GetMissionText(2) + ": " + progress[2] + "/25";
                break;
            case 58:
                progressText.text = translationObject.GetMissionText(35) + ": " + progress[35] + "/5";
                break;
            case 59:
                progressText.text = translationObject.GetMissionText(39) + ": " + progress[39] + "/3";
                break;
            case 60:
                progressText.text = translationObject.GetMissionText(36) + ": " + progress[36] + "/1" + "\n" +
                    translationObject.GetMissionText(37) + ": " + progress[37] + "/1" + "\n" +
                    translationObject.GetMissionText(38) + ": " + progress[38] + "/1" + "\n" +
                    translationObject.GetMissionText(39) + ": " + progress[39] + "/1" + "\n";
                break;
            case 61:
                progressText.text = translationObject.GetMissionText(61) + ": " + progress[61] + "/1";
                break;
            case 62:
                progressText.text = translationObject.GetText(41);
                break;
            case 63:
                progressText.text = translationObject.GetMissionText(40) + ": " + progress[40] + "/1";
                break;
            case 64:
                progressText.text = translationObject.GetMissionText(41) + ": " + progress[41] + "/1";
                break;
            case 65:
                progressText.text = translationObject.GetMissionText(42) + ": " + progress[42] + "/1";
                break;
            case 66:
                progressText.text = translationObject.GetMissionText(43) + ": " + progress[43] + "/1";
                break;
            case 67:
                progressText.text = translationObject.GetMissionText(44) + ": " + progress[44] + "/1";
                break;
            case 68:
                progressText.text = translationObject.GetMissionText(45) + ": " + progress[45] + "/1";
                break;
            case 69:
                progressText.text = translationObject.GetMissionText(46) + ": " + progress[46] + "/1";
                break;
            case 70:
                progressText.text = translationObject.GetMissionText(47) + ": " + progress[47] + "/1";
                break;
            case 71:
                progressText.text = translationObject.GetMissionText(48) + ": " + progress[48] + "/1";
                break;
            case 72:
                progressText.text = translationObject.GetText(41);
                break;
            case 73:
                progressText.text = translationObject.GetMissionText(49) + ": " + progress[49] + "/5";
                break;
            case 74:
                progressText.text = translationObject.GetMissionText(49) + ": " + progress[49] + "/15";
                break;
            case 75:
                progressText.text = translationObject.GetMissionText(50) + ": " + progress[50] + "/3";
                break;
            case 76:
                progressText.text = translationObject.GetMissionText(9) + ": " + progress[9] + "/15";
                break;
            case 77:
                progressText.text = translationObject.GetMissionText(51) + ": " + progress[51] + "/8";
                break;
            case 78:
                progressText.text = translationObject.GetMissionText(52) + ": " + progress[52] + "/2";
                break;
            case 79:
                progressText.text = translationObject.GetMissionText(51) + ": " + progress[51] + "/10" + "\n" +
                    translationObject.GetMissionText(50) + ": " + progress[50] + "/10";
                break;
            case 80:
                progressText.text = translationObject.GetMissionText(49) + ": " + progress[49] + "/1" + "\n" +
                    translationObject.GetMissionText(50) + ": " + progress[50] + "/1" + "\n" +
                    translationObject.GetMissionText(51) + ": " + progress[51] + "/1";
                break;
            case 81:
                progressText.text = translationObject.GetMissionText(53) + ": " + progress[53] + "/1";
                break;
            case 82:
                progressText.text = translationObject.GetText(41);
                break;
            case 83:
                progressText.text = translationObject.GetMissionText(54) + ": " + progress[54] + "/1";
                break;
            case 84:
                progressText.text = translationObject.GetMissionText(55) + ": " + progress[55] + "/1";
                break;
            case 85:
                progressText.text = translationObject.GetMissionText(56) + ": " + progress[56] + "/1";
                break;
            case 86:
                progressText.text = translationObject.GetMissionText(57) + ": " + progress[57] + "/1";
                break;
            case 87:
                progressText.text = translationObject.GetMissionText(58) + ": " + progress[58] + "/1";
                break;
            case 88:
                progressText.text = translationObject.GetMissionText(59) + ": " + progress[59] + "/1";
                break;
            case 89:
                progressText.text = translationObject.GetMissionText(60) + ": " + progress[60] + "/1";
                break;
            case 90:
                progressText.text = translationObject.GetMissionText(24) + ": " + progress[24] + "/8";
                break;
            case 91:
                progressText.text = translationObject.GetMissionText(9) + ": " + progress[9] + "/30";
                break;
            case 92:
                progressText.text = translationObject.GetText(41);
                break;
            case 93:
                progressText.text = translationObject.GetMissionText(62) + ": " + progress[62] + "/25";
                break;
        }

        progress[62] = 0;

        DProgressText.text = translationObject.GetDaMissionText(TodaysMissionId) + ": " + missionDayliProgress[TodaysMissionId] + "/" + missionGoal[TodaysMissionId];
        
        float rewardToText = 50 * Streak;

        rewardText.text = translationObject.GetText(42) + rewardToText + "XP";
    }

    public void ShowMissionPanel(int person)
    {
        int MissionId = missionsProgress[person];

        PersonalMissions[person].SetActive(true);

        int[] maxMissionId = { 6, 13, 23, 33, 43, 53, 63, 73, 83, 93, 94 };

        if(MissionId >= maxMissionId[person])
        {
            thanking[person].SetActive(true);
        }
        else
        {
            MissionById[MissionId - 1].SetActive(true);
        }
    }

    public void StartMIssion(int MissionId)
    {
        if(isOnMission == true)
        {
            return;
        }
        for (int i = 0; i < progress.Length; i++)
        {
            progress[i] = 0;
        }
        currentMissionId = MissionId;
        eqSys.GetComponent<EqSystem>().MissionChecker();
        isOnMission = true;
        GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
    }

    public void FinishMission(int MissionId)
    {
        if (MissionId <= 5)
        {
            missionsProgress[0]++;
        }
        else if (MissionId <= 12 && MissionId >= 6)
        {
            missionsProgress[1]++;
        }
        else if (MissionId <= 22 && MissionId >= 13)
        {
            missionsProgress[2]++;
        }
        else if (MissionId <= 32 && MissionId >= 23)
        {
            missionsProgress[3]++;
        }
        else if (MissionId <= 42 && MissionId >= 33)
        {
            missionsProgress[4]++;
        }
        else if (MissionId <= 52 && MissionId >= 43)
        {
            missionsProgress[5]++;
        }
        else if (MissionId <= 62 && MissionId >= 53)
        {
            missionsProgress[6]++;
        }
        else if (MissionId <= 72 && MissionId >= 63)
        {
            missionsProgress[7]++;
        }
        else if (MissionId <= 82 && MissionId >= 73)
        {
            missionsProgress[8]++;
        }
        else if (MissionId <= 92 && MissionId >= 83)
        {
            missionsProgress[9]++;
        }
        else if (MissionId <= 93 && MissionId >= 93)
        {
            missionsProgress[10]++;
        }
        isOnMission = false;
        isMissionDone = false;
        for (int i = 0; i < progress.Length; i++)
        {
            progress[i] = 0;
        }
        currentMissionId = 9999;
        progressText.text = "";
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().currentXP += 75;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins += 25;
        GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
    }

    public void StartDayMission()
    {
        int[] k0 = {0, 1 };
        int[] k1 = { 2, 3, 4, 5, 6 };
        int[] k2 = { 7, 8, 9, 10, 16 };
        int[] k3 = { 22, 23, 24, 25, 26 };
        int[] k4 = { 17, 18, 19, 20, 21 };
        int[] k5 = { 11, 12, 13, 14, 15 };

        int[] missionsAvailable = { 1, 0, 0, 0, 0, 0 };

        for (int i = 0; i < MapScript.GetComponent<MapSizeSystem>().knownPlaces.Length - 1; i++)
        {
            missionsAvailable[i + 1] = MapScript.GetComponent<MapSizeSystem>().knownPlaces[i];
        }

        int landIdMIssion = 0;
        for (int i = 0; i < 100; i++)
        {
            landIdMIssion = Random.Range(0, 6);

            if (missionsAvailable[landIdMIssion] == 1)
            {
                break;
            }
        }

        switch (landIdMIssion)
        {
            case 0:
                TodaysMissionId = k0[Random.Range(0, 2)];
                break;
            case 1:
                TodaysMissionId = k1[Random.Range(0, 6)];
                break;
            case 2:
                TodaysMissionId = k2[Random.Range(0, 6)];
                break;
            case 3:
                TodaysMissionId = k3[Random.Range(0, 6)];
                break;
            case 4:
                TodaysMissionId = k4[Random.Range(0, 6)];
                break;
            case 5:
                TodaysMissionId = k5[Random.Range(0, 6)];
                break;
        }

        for (int i = 0; i < missionDayliProgress.Length; i++)
        {
            missionDayliProgress[i] = 0;
        }
        GameObject.FindGameObjectWithTag("controller").GetComponent<SettingsSystem>().SaveGame();
    }
}
