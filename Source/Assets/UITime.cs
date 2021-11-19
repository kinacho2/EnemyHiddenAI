using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    [SerializeField] Text Text;

    private void Update()
    {
        float timer = GameTimer.Timer;
        int sec = Mathf.FloorToInt(timer);
        float dec = timer - sec;
        dec *= 100;
        int r = Mathf.FloorToInt(dec);

        int min = sec / 60;
        sec = sec - min * 60;
        string smin = min.ToString("00");
        string ssec = sec.ToString("00");
        string sr = r.ToString("00");
        Text.text = ("Time:\n" + smin + ":" + ssec + ":" + sr);

    }

}
