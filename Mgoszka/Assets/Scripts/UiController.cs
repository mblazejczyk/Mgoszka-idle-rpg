using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject Profil;
    public GameObject Ekwipunek;
    public GameObject Podroz;
    public GameObject Online;
    public GameObject Ustawienia;

    public void OpenClose(int loc)
    {
        
        switch (loc)
        {
            case 0:
                if(Profil.activeSelf == true)
                {
                    Profil.SetActive(false);
                }
                else
                {
                    Profil.SetActive(true);
                    Ekwipunek.SetActive(false);
                    Podroz.SetActive(false);
                    Online.SetActive(false);
                    Ustawienia.SetActive(false);
                }
                break;
            case 1:
                if (Ekwipunek.activeSelf == true)
                {
                    Ekwipunek.SetActive(false);
                }
                else
                {
                    Ekwipunek.SetActive(true);
                    Profil.SetActive(false);
                    Podroz.SetActive(false);
                    Online.SetActive(false);
                    Ustawienia.SetActive(false);
                }
                break;
            case 2:
                if (Podroz.activeSelf == true)
                {
                    Podroz.SetActive(false);
                }
                else
                {
                    Podroz.SetActive(true);
                    Ekwipunek.SetActive(false);
                    Profil.SetActive(false);
                    Online.SetActive(false);
                    Ustawienia.SetActive(false);
                }
                break;
            case 3:
                if (Online.activeSelf == true)
                {
                    Online.SetActive(false);
                }
                else
                {
                    Online.SetActive(true);
                    Ekwipunek.SetActive(false);
                    Podroz.SetActive(false);
                    Profil.SetActive(false);
                    Ustawienia.SetActive(false);
                }
                break;
            case 4:
                if (Ustawienia.activeSelf == true)
                {
                    Ustawienia.SetActive(false);
                }
                else
                {
                    Ustawienia.SetActive(true);
                    Ekwipunek.SetActive(false);
                    Podroz.SetActive(false);
                    Online.SetActive(false);
                    Profil.SetActive(false);
                }
                break;
        }
    }


}
