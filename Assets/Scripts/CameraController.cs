using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player player;
    Vector3 camPos;
    Vector3 playerPos;
    public float smoothRate;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        camPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>();
    }
     private void FixedUpdate(){
        playerPos = player.transform.position;

        camPos = transform.position;
        camPos.x = Mathf.Lerp(camPos.x, playerPos.x, smoothRate);
        camPos.y = Mathf.Lerp(camPos.y, playerPos.y, smoothRate);
        transform.position = camPos;
    }
   
}
