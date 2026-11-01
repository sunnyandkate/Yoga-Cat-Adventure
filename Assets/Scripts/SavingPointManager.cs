using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingPointManager : MonoBehaviour
{

    public Vector3 savingPosition;
    public Vector3 savingCurrentPosition;
    LevelManager levelManager;

    public string savingPointName;
    // Start is called before the first frame update
    void Start()
    {
        savingPosition = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            levelManager.SaveData();
            levelManager.SaveBoat();
           
            savingCurrentPosition = transform.position;
        }
    }
}
