using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < priceToId.Length; i++)
        {
            priceAterSale[i] = priceToId[i];
            priceAterSale[i] -= priceAterSale[i] * (float.Parse(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().charyzma.ToString()) / 100);
            pricesText[i].text = priceAterSale[i].ToString();
        }
    }

    public void BuyItem(int ShopItemId)
    {
        if(priceToId[ShopItemId] <= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins)
        {
            for (int i = 0; i < whatIsMissionRelated.Length; i++)
            {
                if(whatIsMissionRelated[i] == ShopItemId)
                {
                    GameObject.FindGameObjectWithTag("controller").GetComponent<MissionSystem>().progress[itemId[ShopItemId]]++;
                }
                else
                {
                    eq.GetComponent<EqSystem>().AddItem(itemId[ShopItemId]);
                }
            }

            SFXsound.clip = audioClips[Random.Range(0, audioClips.Length)];
            SFXsound.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().coins -= priceToId[ShopItemId];
        }
    }
}
