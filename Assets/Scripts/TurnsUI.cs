using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurnsUI : MonoBehaviour
{
    public Image [] playersImages;
    public int turn = 0;
    void Start() {
        Color disabledColor = new Vector4(0.69f, 0.69f, 0.69f, 1f);
        Color activeColor = new Vector4(1f, 1f, 1f, 1f);

        for (int i = 0; i < playersImages.Length; i++) {
            if (i != turn) {
                playersImages[i].color = disabledColor;
            } else {
                playersImages[i].color = activeColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
