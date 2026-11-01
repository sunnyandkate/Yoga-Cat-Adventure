using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    //movement
    public float moveSpeed;
   
    float xInput;
    float yInput;
    private Vector2 moveDirection;

    private GameManager gameManager;
    private Animator animator;
    private SceneChanger sceneChanger;
    private LevelManager levelManager;
    private SavingPointManager savingPointManager;

    public static Player instance;
    private Vector3 respawnPosition;
    public Vector3 startPosition;

    //water collision
    [Header("Water")]
    public GameObject boat;
    public Vector3 boatPosition;
    public bool waterCollision; 
    public bool swimSuit;
    public bool insideBoat;
    public GameObject SwimsuitImage;

    //change sprite
    [Header("Sprites")]
    SpriteRenderer spriteRenderer;
    public Sprite waterSprite;
    public Sprite defaultSprite;
    public Sprite boatSprite;
    public Sprite fallWaterSprite;

   

    public AreaTransitionValue playerStartPosition;
    public CollectingItemsValue collectingItemsValue;
    public ObjectsPositions boatStartPosition;
   
   
    public string areaTransitionName;

   void Awake(){
   //check if the player already exists
    /*        if(instance == null){
            instance = this;
            
        }
        else if(instance != this){
            Destroy(gameObject);
        }  
         DontDestroyOnLoad(gameObject);  
     */
       
   }
    
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        sceneChanger = FindObjectOfType<SceneChanger>();
        levelManager = FindObjectOfType<LevelManager>();
        savingPointManager = FindObjectOfType<SavingPointManager>();
        boat = GameObject.Find("Boat");
        respawnPosition = transform.position;
      //  transform.position = startPosition; 
        transform.position = playerStartPosition.initialValue;
        if(boat != null){
            boatPosition = boat.transform.position;
            boat.transform.position = boatStartPosition.boatPositionValue;
        }
       
        swimSuit = collectingItemsValue.swimsuitValue;

        SwimsuitImage = GameObject.Find("GameEssentials/Canvas/ObjectCollectBox/ObjectContainer/Swimsuit");
         
    }

  
    void Update()
    {       
        PlayerSpriteAnimation();            
    }
    
    private void FixedUpdate(){
        //player movement   
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        transform.Translate(xInput * moveSpeed * Time.deltaTime, yInput * moveSpeed * Time.deltaTime, 0f);     
        MovePlayer();              
    }
   
    private void MovePlayer(){
        //idle animation
        animator.SetBool("Walk", false);
              
        if(xInput != 0 || yInput != 0){
            //walking animation
            animator.SetBool("Walk", true);
            animator.SetFloat("MoveX", xInput);
            animator.SetFloat("MoveY", yInput);
        } 
    }
    private void PlayerSpriteAnimation(){
        if(waterCollision && insideBoat == false){
            ChangeToWaterSprite();
           
        }else if(waterCollision && insideBoat){
            ChangeToBoatSprite();
            animator.SetTrigger("Boat");      
            //set boat position on player position   
            boat.transform.position = transform.position; 
            
        }
        else{
            ChangeToDefaultSprite();
            
            insideBoat = false;
        }    
        //leaving boat
        if(insideBoat == false && !waterCollision){
            animator.SetTrigger("LeaveBoat");
            if(boat != null){
                boat.SetActive(true);
            }          
        }
        //has swimsuit
        if(swimSuit){
            SwimsuitImage.SetActive(true);
        }
        if(!waterCollision){
            animator.SetTrigger("LeaveWater");
        }
    }
  
    public void RespawnPlayer(){      
        waterCollision = false;
         
        //respawn boat at last saving point
        levelManager.LoadBoat();
        
        //respawn player at last saving point
      //  transform.position = savingPointManager.savingPosition;
      
       // levelManager.LoadSavePlayerPosition();
      
        transform.position = savingPointManager.savingCurrentPosition;
       
    }
 
    public void ChangeToDefaultSprite(){
        spriteRenderer.sprite = defaultSprite;       
        animator.SetBool("Swim", false);
    }
    public void ChangeToWaterSprite(){
        if(swimSuit){
            spriteRenderer.sprite = waterSprite;
            animator.SetBool("Swim", true);           
            animator.SetFloat("SwimX", xInput);
            animator.SetFloat("SwimY", yInput);
        }else if(!swimSuit){
           //fall into water sprite
          FallInWater();                       
        }      
    }   
    public void ChangeToBoatSprite(){
        spriteRenderer.sprite = boatSprite;
    }
    public void FallInWater(){
        spriteRenderer.sprite = fallWaterSprite;
        animator.SetTrigger("FallWater");       
        Invoke("RespawnPlayer", 1f);  
    }
   
    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Object"){
            //collect object
            gameManager.collectItem(other.gameObject);     
            levelManager.SaveData();   
            //save gems in scriptable object
            sceneChanger.SaveGems();   
        }
        if(other.gameObject.tag == "DivingGear"){
            //collect object
            swimSuit = true;
            SwimsuitImage.SetActive(true);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Boat"){
            insideBoat = true;
            boat.SetActive(false);
        }
        if(other.gameObject.tag == "Yogamat"){
          //  animator.SetBool("Yoga", true);
             animator.Play("YogaAnimation");
        }
      
        if(other.gameObject.tag == "SavingPoint"){
          
            //player is crossing saving point
            //current player position is saved as saving point
            //savint point position is saved
            savingPointManager.savingPosition = transform.position;
            savingPointManager.savingCurrentPosition = savingPointManager.savingPosition;
        }
        //save point before cave
        if(other.gameObject.tag == "SaveArea"){
             savingPointManager.savingPosition = transform.position;
            savingPointManager.savingCurrentPosition = savingPointManager.savingPosition;
            levelManager.SaveData();
            
            
        }
        //load point when entering the cave/other scene
        if(other.gameObject.tag == "LoadArea"){
            levelManager.LoadData();
            if(boat != null){
                levelManager.LoadBoat();
            }     
            levelManager.LoadSavePlayerPosition();       
        }
    } 
    public void OnTriggerStay2D(Collider2D other){
          if(other.gameObject.tag == "Yogamat"){
            animator.SetBool("Yoga", true);
            animator.Play("YogaAnimation");
        }
    }   
    public void OnTriggerExit2D(Collider2D other){
          if(other.gameObject.tag == "Yogamat"){
            animator.SetBool("Yoga", false);
        }
    }
   
}
