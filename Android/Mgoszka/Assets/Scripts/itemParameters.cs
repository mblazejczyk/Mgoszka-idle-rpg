using UnityEngine;

public class itemParameters : MonoBehaviour
{
    public int ProgressId = 0;
    public bool isGivingProgress = false;
    [Space(10)]
    public int Id;
    public string[] Name;
    [Space(10)]
    public bool isEquickable;
    public int eqPlace;
    public bool isLimited;
    [Space(10)]
    public float add_total_HP;
    public float add_HP;
    public float add_energy;
    public float add_total_energy;
    public float add_ad;
    public float add_md;
    public float add_armor;
    public float add_barier;
    public float add_speed;
    public float add_dodge;
    public int add_charyzma;
    public float add_escameChance;
    public float xpBoostPercent;
    public int xpBoostTimeInSec;
    public float CritChanse;
    [Space(10)]
    public bool isCommented = false;
    public string[] commentStr;
    [Space(10)]
    public int runeId = 0;
    [Space(10)]
    [Tooltip("0 - common, 1 - Fine, 2 - rare, 3 - epic, 4 - legendary, 5 - ultimate, 6 - mythic")]
    public int RareId = 0;
}
