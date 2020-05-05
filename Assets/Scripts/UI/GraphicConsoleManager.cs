using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicConsoleManager : MonoBehaviour
{
    [SerializeField] private int maxConsoleLines = 80;

    [SerializeField] private RectTransform contentPane;
    [SerializeField] private ScrollRect scrollPane;
    [SerializeField] private Text graphicConsoleText;


    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        Application.logMessageReceived += LogMessageReceived;
    }

    private void OnDisable() {
        Application.logMessageReceived -= LogMessageReceived;
    }
    private void OnDestroy() {
        Application.logMessageReceived -= LogMessageReceived;
    }

    // Update is called once per frame
    void Update()
    {
        //contentPane.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, graphicConsoleText.preferredHeight);


        if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            Debug.Log("THINGEEEY");
        }
    }

    private void UpdateConsoleText(string newText) {
        string text = graphicConsoleText.text;

        bool doScrollDown = scrollPane.normalizedPosition.y < 0.01f;


        int numLines = text.Length - text.Replace("\n", "").Length+1;


        if (numLines > maxConsoleLines) {
            int firstIndex = text.IndexOf("\n")+1;
            graphicConsoleText.text = text.Substring(firstIndex, text.Length - firstIndex);
        }


        DateTime now = DateTime.Now;
        string nowtime = now.ToString("HH:mm:ss");
        //nowtime = nowtime.Substring(0, nowtime.Length - 1);
        graphicConsoleText.text += nowtime + "    " +newText + "\n";

        contentPane.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, graphicConsoleText.preferredHeight);

        if (doScrollDown) {
            scrollPane.normalizedPosition = new Vector2(0, 0);
        }
    }

    private void LogMessageReceived(string condition, string stackTrace, LogType type) {
        //Debug.Log("Message recieved:  " + condition);

        UpdateConsoleText(condition);

    }
}
