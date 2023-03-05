using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        var money1 = new Money(100, 99);
        var money2 = new Money(1, 1);
        var money3 = new Money(3, 1);
        var money4 = new Money(98, 99);

        // money1.Increase(money2);
        // money1.Decrease(money3);
        // var res1 = money1.Equal(money3);
        // print(res1);
        // var res2 =money1.Equal(money4);
        // print(res2);
        // money1.Multiplication(0.31f);
        // money1.Division(0.31f);
    }
}
