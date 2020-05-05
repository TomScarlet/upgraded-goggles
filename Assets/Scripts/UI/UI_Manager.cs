using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    


    private static UI_Manager instance;
    public static UI_Manager Instance { get { return instance; } }

    [SerializeField] private GameObject modeSelectScreen;
    [SerializeField] private GameObject lobbyScreen;
    [SerializeField] private GameObject headlessServerScreen;

    [SerializeField] private Button headlessServerButton;

    //public GameObject ModeSelectScreen { get { return modeSelectScreen; } }


    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetScreen(E_Screens.ModeSelect);

        //        headlessServerButton.onClick.AddListener(delegate { SetScreen(E_Screens.HeadlessServer); });
        headlessServerButton.onClick.AddListener(() => SetScreen(E_Screens.HeadlessServer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScreen(E_Screens scr) {
        switch (scr) {
            case E_Screens.Lobby:
                modeSelectScreen.SetActive(false);
                headlessServerScreen.SetActive(false);
                lobbyScreen.SetActive(true);
                break;
            case E_Screens.ModeSelect:
                lobbyScreen.SetActive(false);
                headlessServerScreen.SetActive(false);
                modeSelectScreen.SetActive(true);
                break;
            case E_Screens.HeadlessServer:
                headlessServerScreen.SetActive(true);
                modeSelectScreen.SetActive(false);
                lobbyScreen.SetActive(false);
                break;
        }
    }

}
