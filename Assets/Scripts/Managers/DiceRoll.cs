using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{

    public int RollDice()

    {
        int total;
        int dice1;
        int dice2;

        dice1 = Random.Range(1, 6);
        dice2 = Random.Range(1, 6);

        total = dice1 + dice2;  

        return total;
    }


   
}
