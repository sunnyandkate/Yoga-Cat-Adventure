using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    Player player;
    GameManager gameManager;
    SavingPointManager savingPointManager;
    TreasureChest treasureChest;

    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        savingPointManager = FindObjectOfType<SavingPointManager>();
        treasureChest = FindObjectOfType<TreasureChest>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
     public int boolToInt(bool value){
        if(value){
            return 1;
        }else{
            return 0;
        }
        
    }
    public bool intToBool(int value){
        if(value != 0){
            return true;
        }else{
            return false;
        }
        
    }
   
    public void LoadData(){
      
       gameManager.hasGreenCrystal = intToBool(PlayerPrefs.GetInt("HasGreenCrystal", 0));
       gameManager.hasBlueCrystal = intToBool(PlayerPrefs.GetInt("HasBlueCrystal", 0));
       gameManager.hasOrangeCrystal = intToBool(PlayerPrefs.GetInt("HasOrangeCrystal", 0));
       gameManager.hasYellowCrystal = intToBool(PlayerPrefs.GetInt("HasYellowCrystal", 0));
       gameManager.hasPinkCrystal = intToBool(PlayerPrefs.GetInt("HasPinkCrystal", 0));
       gameManager.hasPurpleCrystal = intToBool(PlayerPrefs.GetInt("HasPurpleCrystal", 0));
      
       player.swimSuit = intToBool(PlayerPrefs.GetInt("HasSwimsuit", 0));

       
         Debug.Log("load from levelmanager");

    }
    public void LoadPlayer(){
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("playerX"),PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));
       
    }
     public void LoadBoat(){
        player.boat.transform.position = new Vector3(PlayerPrefs.GetFloat("boatX"),PlayerPrefs.GetFloat("boatY"), PlayerPrefs.GetFloat("boatZ"));
    }
    public void LoadTreasureChest(){
        treasureChest.boxIsOpened = intToBool(PlayerPrefs.GetInt("BoxIsOpened", 0));
    }
    public void LoadSavePlayerPosition(){
        savingPointManager.savingCurrentPosition = new Vector3(PlayerPrefs.GetFloat("playerSaveX"), PlayerPrefs.GetFloat("playerSaveY"), PlayerPrefs.GetFloat("playerSaveZ"));
    }
    public void SaveAll(){
        SaveData();
        SaveBoat();
    }

    public void SaveData(){
        PlayerPrefs.SetString("currentScene", SceneManager.GetActiveScene().name);     
       
        PlayerPrefs.SetInt("HasGreenCrystal", boolToInt(gameManager.hasGreenCrystal));
        PlayerPrefs.SetInt("HasBlueCrystal", boolToInt(gameManager.hasBlueCrystal));
        PlayerPrefs.SetInt("HasOrangeCrystal", boolToInt(gameManager.hasOrangeCrystal));
        PlayerPrefs.SetInt("HasYellowCrystal", boolToInt(gameManager.hasYellowCrystal));
        PlayerPrefs.SetInt("HasPinkCrystal", boolToInt(gameManager.hasPinkCrystal));
        PlayerPrefs.SetInt("HasPurpleCrystal", boolToInt(gameManager.hasPurpleCrystal));
       
        PlayerPrefs.SetInt("HasSwimsuit", boolToInt(player.swimSuit));

        PlayerPrefs.SetFloat("playerX", player.transform.position.x);
        PlayerPrefs.SetFloat("playerY", player.transform.position.y);
        PlayerPrefs.SetFloat("playerZ", player.transform.position.z);

       

        PlayerPrefs.SetFloat("playerSaveX", savingPointManager.savingCurrentPosition.x);
        PlayerPrefs.SetFloat("playerSaveY", savingPointManager.savingCurrentPosition.y);
        PlayerPrefs.SetFloat("playerSaveZ", savingPointManager.savingCurrentPosition.z);

        

        Debug.Log("save from levelmanager");      
    }  
    public void SaveTreasureChest(){
        PlayerPrefs.SetInt("BoxIsOpened", boolToInt(treasureChest.boxIsOpened));
    }
    public void SaveBoat(){
         //if there is a boat in the scene
        if(player.boat != null){
            PlayerPrefs.SetFloat("boatX", player.boat.transform.position.x);
            PlayerPrefs.SetFloat("boatY", player.boat.transform.position.y);
            PlayerPrefs.SetFloat("boatZ", player.boat.transform.position.z);
        }
    }
}
