using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleManager : MonoBehaviour
{

    GameManager gameManager;
     //temple
    [Header("Temple")]
    public GameObject TempleBlank;
    public GameObject TempleGems;
   
    public GameObject AreaTransitionTemple;

    //npc
    public GameObject Elephant;
    public GameObject ElephantGemsFound;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        TempleBlank = GameObject.Find("TempleArea/TempleBlank");
        TempleGems = GameObject.Find("TempleArea/TempleGems");
        Elephant = GameObject.Find("TempleArea/Elephant");
        ElephantGemsFound = GameObject.Find("TempleArea/ElephantGemsFound");
        AreaTransitionTemple = GameObject.Find("TempleArea/AreaTransitionTemple"); 

        TempleGems.SetActive(false);
        ElephantGemsFound.SetActive(false);
        AreaTransitionTemple.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.allGemsFound){
            TempleBlank.SetActive(false);
            TempleGems.SetActive(true);
            Elephant.SetActive(false);
            ElephantGemsFound.SetActive(true);
            AreaTransitionTemple.SetActive(true);
        }else{
            TempleBlank.SetActive(true);
            TempleGems.SetActive(false);
            Elephant.SetActive(true);
            ElephantGemsFound.SetActive(false);
            AreaTransitionTemple.SetActive(false); 
        }
    }
}
