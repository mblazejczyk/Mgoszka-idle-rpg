using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public int[] itemId;
    public float[] priceToId;
    public float[] priceAterSale;
    public GameObject eq;
    public Text[] pricesText;
    [Space(10)]
    public int[] whatIsMissionRelated;
    [Space(10)]
    public AudioSource SFXsound;
    public AudioClip[] audioClips;
    [Space(10)]
    public GameObject eqIsFull;
    public Button buyButton;
    [Space(10)]
    public GameObject itemDetails;
    public Image itemImage;
    public Text itemParameters;
    public Text itemDetailName;
    public int ChoosenId = 0;
    // Start is called before the first frame update
    private void Start()
    {
        GetEqInfo();
    }
    public void GetEqInfo()
    {
        if(eq.GetComponent<EqSystem>().IsEqFull() == true)
        {
            eqIsFull.SetActive(true);
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    void Update()
    {
        for (int i = 0; i < priceToId.Length; i++)
        {
            priceAterSale[i] = priceToId[i];
            priceAterSale[i] -= priceAterSale[i] * (float.Parse(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma.ToString()) / 100);
            pricesText[i].text = priceAterSale[i].ToString();
        }
    }

    public void BuyItem()
    {
        if(priceToId[ChoosenId] <= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins)
        {
            for (int i = 0; i < whatIsMissionRelated.Length; i++)
            {
                if (whatIsMissionRelated[i] == ChoosenId)
                {
                    GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[itemId[ChoosenId]]++;
                }
                else
                {
                    eq.GetComponent<EqSystem>().AddItem(itemId[ChoosenId]);
                }
            }
            SFXsound.clip = audioClips[Random.Range(0, audioClips.Length)];
            SFXsound.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins -= priceToId[ChoosenId];
        }
    }

    public void ShowDetails(int Id)
    {
        ChoosenId = Id;
        itemDetails.SetActive(true);
        foreach (GameObject ob in eq.GetComponent<EqSystem>().items)
        {
            if (ob.GetComponent<itemParameters>().Id == itemId[ChoosenId])
            {
                itemImage.sprite = ob.GetComponent<Image>().sprite;
                itemDetailName.text = ob.GetComponent<itemParameters>().Name;
                itemParameters.text = "";
                int c = 1;
                if (ob.GetComponent<itemParameters>().add_HP != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowe życie: " + ob.GetComponent<itemParameters>().add_HP + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_total_HP != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa pojemność życia: " + ob.GetComponent<itemParameters>().add_total_HP + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_energy != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa energia: " + ob.GetComponent<itemParameters>().add_energy + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_total_energy != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa poejmność energii: " + ob.GetComponent<itemParameters>().add_total_energy + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_ad != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowe obrażenia fizyczne: " + ob.GetComponent<itemParameters>().add_ad + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_md != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowe obrażenia magiczne: " + ob.GetComponent<itemParameters>().add_md + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_armor != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowy pancerz: " + ob.GetComponent<itemParameters>().add_armor + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_barier != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa bariera: " + ob.GetComponent<itemParameters>().add_barier + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_speed != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa prędkość: " + ob.GetComponent<itemParameters>().add_speed + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_dodge != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowy dodge: " + ob.GetComponent<itemParameters>().add_dodge + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_charyzma != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa charyzma: " + ob.GetComponent<itemParameters>().add_charyzma + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().add_escameChance != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa sznasa ucieczki: " + ob.GetComponent<itemParameters>().add_escameChance + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().CritChanse != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowa sznasa atak krytyczny: " + ob.GetComponent<itemParameters>().CritChanse + "\n";
                    c++;
                }
                if (ob.GetComponent<itemParameters>().xpBoostPercent != 0)
                {
                    itemParameters.text += c + ". " + "Dodatkowy % XP: " + ob.GetComponent<itemParameters>().xpBoostPercent + " na: " + ob.GetComponent<itemParameters>().xpBoostTimeInSec + "s";
                }
                if (ob.GetComponent<itemParameters>().isCommented == true)
                {
                    itemParameters.text += "<i><color=#d4d4d4>" + ob.GetComponent<itemParameters>().commentStr + "</color></i>";
                }
            }
        }
    }
}
