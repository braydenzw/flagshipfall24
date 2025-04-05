using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button continueButton;
    public GameObject saveListScene;
    public Button[] loadSaveButtons;
    public GameObject saveCreator;
    public TMP_InputField saveName;

    private Dictionary<SaveFile, string> saves;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveListScene.SetActive(false);
        saveCreator.SetActive(false);
        saveName.text = "";

        saves = GameSaveSystem.loadAllSaves();
        continueButton.interactable = PlayerPrefs.HasKey("LastSave") && File.Exists(PlayerPrefs.GetString("LastSave"));
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        var keyList = saves.Keys.ToList();
        for(int i = 0; i < loadSaveButtons.Length; i++){
            if(keyList.Count > i){
                var s = keyList[i];
                loadSaveButtons[i].gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "<b>" + s.name + "</b>\nDay " + s.day;
                loadSaveButtons[i].onClick.AddListener(() => loadFromSave(saves[s]));
            } else {
                loadSaveButtons[i].gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = "<b>---</b>\nDay -";
                loadSaveButtons[i].onClick.AddListener(() => toggleSaveCreator());
            }
        }
    }

    public void loadFromSave(string save){
        GameManager.gameData = GameSaveSystem.loadSave(save);
        gameManager.beginDay();
    }
    public void loadFromLastSave(){
        loadFromSave(PlayerPrefs.GetString("LastSave"));
    }

    // view all saves
    public void toggleViewSaves() {
        saveListScene.SetActive(!saveListScene.activeSelf);
    }
    // toggle save creator thing
    public void toggleSaveCreator(){
        if(!saveCreator.activeSelf){
            saveName.text = "";
        }
        saveCreator.SetActive(!saveCreator.activeSelf);
    }

    // create new save
    public void createSave(){
        string name = saveName.text;
        GameManager.gameData = GameSaveSystem.createNewSave(name);
        gameManager.beginDay();
    }

    // delete save
    public void deleteSave(string file){

    }
}
