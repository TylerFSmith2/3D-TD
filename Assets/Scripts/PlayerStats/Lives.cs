using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public VarInt livesSO; 
    public Text livesText; //Text field to change
    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponentInParent<Text>(); //Text field of the parent
        livesText.text = "Lives: " + livesSO.value; //Start it off with the right value
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + livesSO.value; //Update the value - TODO: call UpdateLives (with an event?) instead of update
    }
}
