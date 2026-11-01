using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YogaMat : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Color tmp;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tmp = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other){
        //when player moves on the mat, the empty mat will get transparent
        if(other.gameObject.tag == "Player"){
            tmp.a = 0f;
            spriteRenderer.color = tmp;
        }
    }
     public void OnTriggerExit2D(Collider2D other){
        //when player leaves the mat, the empty mat will be visible again
        if(other.gameObject.tag == "Player"){
            tmp.a = 1f;
            spriteRenderer.color = tmp;
        }
    }
}
