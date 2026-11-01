using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    NPC npc;

    GameStart gameStart;
    LevelManager levelManager;

 

    //gems
    [Header("Gems")]
    public GameObject BlueCrystal;
    public GameObject GreenCrystal;
    public GameObject OrangeCrystal;
    public GameObject YellowCrystal;
    public GameObject PurpleCrystal;
    public GameObject PinkCrystal;

    public GameObject blueCrystalItem;
    public GameObject greenCrystalItem;
    public GameObject orangeCrystalItem;
    public GameObject yellowCrystalItem;
    public GameObject purpleCrystalItem;
    public GameObject pinkCrystalItem;

    public bool hasBlueCrystal;
    public bool hasGreenCrystal;
    public bool hasOrangeCrystal;
    public bool hasYellowCrystal;
    public bool hasPurpleCrystal;
    public bool hasPinkCrystal;

    public bool allGemsFound = false;

    //screens
    [Header("Screens")]
    public GameObject StartScreen;
    public GameObject ItemCollectBox;
    public GameObject ObjectCollectBox;
    public GameObject MainMenu;
    public GameObject MenuButton;
    public GameObject MapImage;

    //dialog
    [Header("Dialog")]
    public GameObject DialogBox;

    public Image dialogImage;
    public TextMeshProUGUI dialogText;  
    public bool dialogBoxIsOpenend = false;


    //saving collected items
    public CollectingItemsValue collectingItemsValue;

    //treasurebox
    TreasureChest treasureChest;
    public GameObject treasureBox;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        npc = FindObjectOfType<NPC>();
        gameStart = FindObjectOfType<GameStart>();
        levelManager = FindObjectOfType<LevelManager>();
        treasureChest = FindObjectOfType<TreasureChest>();
        

        GreenCrystal = GameObject.Find("Gems/GreenCrystal");
        BlueCrystal = GameObject.Find("Gems/BlueCrystal");
        OrangeCrystal = GameObject.Find("Gems/OrangeCrystal");
        YellowCrystal = GameObject.Find("Gems/YellowCrystal");
        PurpleCrystal = GameObject.Find("Gems/PurpleCrystal");
        PinkCrystal = GameObject.Find("Gems/PinkCrystal");
        greenCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/GreenCrystalItem");
        blueCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/BlueCrystalItem");
        orangeCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/OrangeCrystalItem");
        yellowCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/YellowCrystalItem");
        purpleCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/PurpleCrystalItem");
        pinkCrystalItem = GameObject.Find("GameEssentials/Canvas/ItemCollectBox/ItemContainer/PinkCrystalItem");

        MenuButton = GameObject.Find("GameEssentials/Canvas/MainMenuBox");
        MapImage = GameObject.Find("GameEssentials/Canvas/MapImage");
        ObjectCollectBox = GameObject.Find("GameEssentials/Canvas/ObjectCollectBox");

        CheckIfGemsCollected();   
        if(PinkCrystal != null){
            PinkCrystal.SetActive(false);
        }
        if(treasureBox != null){
            if(hasPinkCrystal){
                treasureBox.GetComponent<SpriteRenderer>().sprite = treasureChest.chestEmpty;
            }
        }
        
    }
    public void CheckIfGemsCollected(){
        //if continue game, check if gems are already collected
        hasPinkCrystal = collectingItemsValue.pinkCrystalValue;
        hasPurpleCrystal = collectingItemsValue.purpleCrystalValue;
        hasBlueCrystal = collectingItemsValue.blueCrystalValue;
        hasGreenCrystal = collectingItemsValue.greenCrystalValue;
        hasOrangeCrystal = collectingItemsValue.orangeCrystalValue;
        hasYellowCrystal = collectingItemsValue.yellowCrystalValue;
    }

    // Update is called once per frame
    void Update()
    {
      
       UpdateItems();
        
        
    }
    public void UpdateItems(){
       
        //if the diamond has been collected
        //show the diamond in the item collector box
        //and remove from area
      
        //if there is a diamond in the scene
        

        
        UpdateGems(hasBlueCrystal, BlueCrystal, blueCrystalItem);
        UpdateGems(hasGreenCrystal, GreenCrystal, greenCrystalItem);
        UpdateGems(hasPinkCrystal, PinkCrystal, pinkCrystalItem);
        UpdateGems(hasOrangeCrystal, OrangeCrystal, orangeCrystalItem);
        UpdateGems(hasYellowCrystal, YellowCrystal, yellowCrystalItem);
        UpdateGems(hasPurpleCrystal, PurpleCrystal, purpleCrystalItem);
        CheckIfAllGemsAreFound();        
    }
  
    public void UpdateGems(bool hasGem, GameObject gem, GameObject gemItem){
        if(hasGem){
            gemItem.SetActive(true);
            
            //if there is a gem in the scene
            if(gem != null){
                gem.SetActive(false);
            }
        }else{
            gemItem.SetActive(false);
        }
    }
    public void CheckIfAllGemsAreFound(){
        if(hasBlueCrystal && hasGreenCrystal && hasPinkCrystal && hasYellowCrystal && hasOrangeCrystal && hasPurpleCrystal){
            allGemsFound = true;
        }
    }
     public void collectItem(GameObject item){
        
        CollectGem("GreenCrystal", greenCrystalItem, hasGreenCrystal, item);
        CollectGem("BlueCrystal", blueCrystalItem, hasBlueCrystal, item);
        CollectGem("OrangeCrystal", orangeCrystalItem, hasOrangeCrystal, item);
        CollectGem("YellowCrystal", yellowCrystalItem, hasYellowCrystal, item);
        CollectGem("PinkCrystal", pinkCrystalItem, hasPinkCrystal, item);
        CollectGem("PurpleCrystal", purpleCrystalItem, hasPurpleCrystal, item);
    }
    public void CollectGem(string gemName, GameObject gemItem, bool hasGemItem, GameObject item){
         if(item.name == gemName){
           gemItem.SetActive(true);

           switch(gemName){
            case "BlueCrystal": hasBlueCrystal = true; break;
            case "GreenCrystal": hasGreenCrystal = true; break;
            case "OrangeCrystal": hasOrangeCrystal = true; break;
            case "YellowCrystal": hasYellowCrystal = true; break;
            case "PinkCrystal": hasPinkCrystal = true; break;
            case "PurpleCrystal": hasPurpleCrystal = true; break;
           }
         
           item.SetActive(false);          
        }       
    }
   
    public void OpenMainMenu(){
        MainMenu.SetActive(true);
        ItemCollectBox.SetActive(false);
        ObjectCollectBox.SetActive(false);
        MenuButton.SetActive(false);
    }
    public void CloseMainMenu(){
        MainMenu.SetActive(false);
        ItemCollectBox.SetActive(true);
        ObjectCollectBox.SetActive(true);
        MenuButton.SetActive(true);
    }
    public void ExitGame(){
        SceneManager.LoadScene("YogaCatAdventure");
    }
       
    public void ActivateDialogBox(){
        DialogBox.SetActive(true);
        dialogBoxIsOpenend = true;
       
    }
    public void DeactivateDialogBox(){
        DialogBox.SetActive(false);
        dialogBoxIsOpenend = false;
    }
   
    public void ShowMap(){       
        MapImage.SetActive(true);      
    }
    public void CloseMap(){
        MapImage.SetActive(false);
    }
   
   
}
