using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void RespawnPlayer() 
    {
     
      player.transform.position = transform.position;
        
    }
	
}