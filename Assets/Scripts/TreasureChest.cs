using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
   
    public Sprite chestClosed;
    public Sprite chestOpened;
    public Sprite chestEmpty;

    

    public bool boxIsOpened;

    GameManager gameManager;
    LevelManager levelManager;
    public CollectingItemsValue collectingItemsValue;

  
 
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();
        gameManager.PinkCrystal.SetActive(false);
       
        //if the player already found the pink crystal, the treasure box is opened
        if(gameManager.hasPinkCrystal){
            spriteRenderer.sprite = chestEmpty;
        }else{
             spriteRenderer.sprite = chestClosed;
        }
     
    }

  
    
    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            //if the player hasn't found the pink crystal yet, open the treasure box
            if(gameManager.hasPinkCrystal == false){
                spriteRenderer.sprite = chestOpened;
            }
           
            Invoke("MoveGem", 2f);
            
        }
    }
    public void MoveGem(){
        gameManager.PinkCrystal.SetActive(true);
        spriteRenderer.sprite = chestEmpty;
        if(gameManager.hasPinkCrystal){
            boxIsOpened = true;
        }
      
    }
}
