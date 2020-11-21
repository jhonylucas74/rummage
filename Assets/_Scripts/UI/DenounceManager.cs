using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DenounceManager : Singleton<DenounceManager>
{
    int [] denounce = new int [3];
    public int [] receivedDenounce = new int [3];

    public GameObject culpritMenu;
    public GameObject WeaponMenu;
    public GameObject DenounceBox;
    public GameObject RevealMenu;

    public TMP_Text [] revealItems;
    public GameObject PassMenu;

    public TMP_Text denounceTitle;
    void Start()
    {
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        DenounceBox.SetActive(false);
        RevealMenu.SetActive(false);
        PassMenu.SetActive(false);
        Events.OnDeclareDenounce += OnDeclareDenounce;
        Events.OnReceiveDenounce += OnReceiveDenounce;
        Events.OnStopDenounce += OnStopDenounce;
        Events.OnCheckHand += OnCheckHand;
        Events.OnPlayerTurn += OnPlayerTurn;
    }

    void OnDestroy ()
    {
        Events.OnDeclareDenounce -= OnDeclareDenounce;
        Events.OnReceiveDenounce -= OnReceiveDenounce;
        Events.OnStopDenounce -= OnStopDenounce;
        Events.OnCheckHand -= OnCheckHand;
        Events.OnPlayerTurn -= OnPlayerTurn;
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

    public void PassDenuncie () {
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
        DenounceBox.SetActive(true);
        receivedDenounce = d;
        denounceTitle.text = GetLocalName(d[0]) + " - " + GetCulpritName(d[1]) + " - " + GetWeaponName(d[2]);
        Events.OnCheckHand?.Invoke();
    }

    void OnStopDenounce (string name) {
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        RevealMenu.SetActive(false);
        PassMenu.SetActive(false);
    }

    void OnCheckHand () {
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        RevealMenu.SetActive(false);
        PassMenu.SetActive(false);
    }

    void OnPlayerTurn (string id) {
        culpritMenu.SetActive(false);
        WeaponMenu.SetActive(false);
        DenounceBox.SetActive(false);
        RevealMenu.SetActive(false);
        PassMenu.SetActive(false);
    }

    public void ShowReveledCard(string name) {
        DenounceBox.SetActive(true);
        denounceTitle.text = name;
        StartCoroutine(EraseText());
    }

    IEnumerator EraseText() {
        yield return new WaitForSeconds(5);
        denounceTitle.text = "";
        DenounceBox.SetActive(false);

        if (ConnectionManager.Instance.IsHost) {
            Events.OnNextPlayerTurn?.Invoke();
        }
    }

    void OnReceiveDenounce(string pId) {
        if (ConnectionManager.Instance.IsLocalUser(pId)) {
            int item = 0;

            revealItems[0].text = "";
            revealItems[1].text = "";
            revealItems[2].text = "";

            for (int i = 0; i < GameManager.Instance.Player.Cards.Count; i++) {
                CardData card = GameManager.Instance.Player.Cards[i];

                if (card.Type == CardType.Location) {
                    if (card.Name == GetLocalName(receivedDenounce[0])) {
                        revealItems[item].text = card.Name;
                        item++;
                    }
                }

                if (card.Type == CardType.Culprit) {
                    if (card.Name == GetCulpritName(receivedDenounce[1])) {
                        revealItems[item].text = card.Name;
                        item++;
                    }
                }

                if (card.Type == CardType.Weapon) {
                    if (card.Name == GetWeaponName(receivedDenounce[2])) {
                        revealItems[item].text = card.Name;
                        item++;
                    }
                }
            }

            if (item == 0) {
                PassMenu.SetActive(true);
            } else {
                RevealMenu.SetActive(true);
            }
        }
    }
}
