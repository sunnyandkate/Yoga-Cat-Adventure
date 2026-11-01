using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string sceneToLoad;
    Player player;
   
    GameManager gameManager;
    LevelManager levelManager;

    public GameObject FadeInCanvas;

    public Vector2 playerPosition;
    public AreaTransitionValue playerStartPosition;

    public CollectingItemsValue collectingItemsValue;

   
    void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();          
    }
    void Start(){
     
    }
    void Update(){
     
    }
   
    public void LoadLevel(){       
        Invoke("SelectLevel", 1.5f);
        Invoke("FadeOut", 1.8f);      
    }
   
    public void SelectLevel(){    
        SceneManager.LoadScene(sceneToLoad);     
   }
    public void FadeOut(){
      FadeInCanvas.SetActive(false);
      gameManager.ObjectCollectBox.SetActive(true);
       
   }
   
   void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player"){
            
          //when we change the scene-> save collected items and set player on area entrance position
          playerStartPosition.initialValue = playerPosition;
          SaveGems();
         
          collectingItemsValue.swimsuitValue = levelManager.intToBool(PlayerPrefs.GetInt("HasSwimsuit", 0));
          gameManager.ObjectCollectBox.SetActive(false);
          FadeInCanvas.SetActive(true);
        
          LoadLevel();  
        
        }
     }
     public void SaveGems(){
      //save gems in scriptable object
      collectingItemsValue.greenCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasGreenCrystal", 0));
      collectingItemsValue.blueCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasBlueCrystal", 0));
      collectingItemsValue.orangeCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasOrangeCrystal", 0));
      collectingItemsValue.yellowCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasYellowCrystal", 0));
      collectingItemsValue.pinkCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasPinkCrystal", 0));
      collectingItemsValue.purpleCrystalValue = levelManager.intToBool(PlayerPrefs.GetInt("HasPurpleCrystal", 0));
     }
    
}
