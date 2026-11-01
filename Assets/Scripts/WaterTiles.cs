using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTiles : MonoBehaviour
{

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            player.waterCollision = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            player.waterCollision = false;
        }
    }
}
