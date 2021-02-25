using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public VarInt moneySO;
    public Text moneyText; //Text field to change
    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponentInParent<Text>(); //Text field of the parent
        moneyText.text = "Money: " + moneySO.value; //Start it off with the right value
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + moneySO.value; //Update the value - TODO: call UpdateLives (with an event?) instead of update
    }
}
