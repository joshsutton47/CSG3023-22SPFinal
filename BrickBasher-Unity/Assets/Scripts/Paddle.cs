/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        //set the input axis
        float xAxis = Input.GetAxis("Horizontal");
        Vector3 curPos = transform.position;
        float time = Time.deltaTime;
        curPos.x += (xAxis * speed * time);
        transform.position = curPos;
        //Time.deltaTime{ }

    }//end Update()
}
