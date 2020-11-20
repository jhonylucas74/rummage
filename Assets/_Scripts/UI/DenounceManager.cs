using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DenounceManager : Singleton<DenounceManager>
{
    int [] denounce = new int [3];

    public GameObject culpritMenu;
    public GameObject WeaponMenu;

    public TMP_Text denounceTitle;
    void Start()
    {
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        Events.OnDeclareDenounce += OnDeclareDenounce;
    }

    void OnDestroy ()
    {
        Events.OnDeclareDenounce -= OnDeclareDenounce;
    }

    public void StartDenounce() {
        culpritMenu.SetActive(true);
        WeaponMenu.SetActive(false);
    }

    public void SetLocal (int input) {
        denounce[0] = input;
    }

    public void SetCulprit (int input) {
        denounce[1] = input;
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(true);
    }

    public void SetWeapon (int input) {
        denounce[2] = input;
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        ConnectionManager.Instance.DispatchDeclareDenuncie(denounce);
    }

    string GetLocalName (int index) {
        return "Apart. " + index;
    }

    string GetWeaponName (int index) {
        switch (index) {
            case 0:
                return "Frying Pan";
            case 1:
                return "Hammer";
            case 2:
                return "Ice Pick";
            case 3:
                return "Kitchen Board";
            case 4:
                return "Knife";
            case 5:
                return "Olive Oil";
            case 6:
                return "Rolling Pin";
            case 7:
                return "Scissor";
            default:
                return "";
        }
    }

    string GetCulpritName (int index) {
        switch (index) {
            case 0:
                return "Comunist";
            case 1:
                return "E-girl";
            case 2:
                return "Raged";
            case 3:
                return "Pet Dad";
            case 4:
                return "Homeless";
            case 5:
                return "Emo";
            case 6:
                return "Bearded";
            case 7:
                return "Competitive";
            default:
                return "";
        }
    }

    void OnDeclareDenounce (int [] d) {
        denounceTitle.text = GetLocalName(d[0]) + " - " + GetCulpritName(d[1]) + " - " + GetWeaponName(d[2]);
    }
}
