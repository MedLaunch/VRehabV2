using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public GameObject Panel;
    
    public void openPanel(){
        if(Panel != null){
            Panel.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
          Panel.SetActive(true);
        }
    }

}

