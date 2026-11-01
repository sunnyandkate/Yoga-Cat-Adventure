using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    Player player;
    GameManager gameManager;
    LevelManager levelManager;
    
    public string newGame;
    public string continueGame;

    public Vector2 playerPosition;
    public AreaTransitionValue playerStartPosition;
    //get data from scriptable object
    public CollectingItemsValue collectingItemsValue;
    public Vector2 boatPosition;
    public ObjectsPositions boatStartPosition;

   
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame(){
      //startposition of the player
       playerStartPosition.initialValue = playerPosition;
       collectingItemsValue.swimsuitValue = false;
       boatStartPosition.boatPositionValue = boatPosition;
       ResetGems();
       
       SceneManager.LoadScene(newGame);   
 
    }
    public void Continue(){
       
        SceneManager.LoadScene(PlayerPrefs.GetString("currentScene"));
        //load saved items form scriptable object
        LoadGems();
        //load swimsuit
        collectingItemsValue.swimsuitValue = levelManager.intToBool(PlayerPrefs.GetInt("HasSwimsuit", 0));       
        //load player position
        playerStartPosition.initialValue = new Vector3(PlayerPrefs.GetFloat("playerX"),PlayerPrefs.GetFloat("playerY"), PlayerPrefs.GetFloat("playerZ"));        
        //load boat
        boatStartPosition.boatPositionValue = new Vector3(PlayerPrefs.GetFloat("boatX"),PlayerPrefs.GetFloat("boatY"), PlayerPrefs.GetFloat("boatZ"));
       //load savingpoint

       
    }
    public void LoadGems(){
     
      collectingItemsValue.greenCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasGreenCrystal", 0));
      collectingItemsValue.blueCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasBlueCrystal", 0));
      collectingItemsValue.orangeCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasOrangeCrystal", 0));
      collectingItemsValue.yellowCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasYellowCrystal", 0));
      collectingItemsValue.pinkCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasPinkCrystal", 0));
      collectingItemsValue.purpleCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasPurpleCrystal", 0));
     }
    public void ResetGems(){
     
      collectingItemsValue.greenCrystalValue = false;
      collectingItemsValue.blueCrystalValue = false;
      collectingItemsValue.orangeCrystalValue = false;
      collectingItemsValue.yellowCrystalValue = false;
      collectingItemsValue.pinkCrystalValue = false;
      collectingItemsValue.purpleCrystalValue = false;
      collectingItemsValue.treasureChestValue = false;
     }
    public void Exit(){
       
        SceneManager.LoadScene("Credits");
    }
    public void PlayAgain(){
        SceneManager.LoadScene("YogaCatAdventure");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
