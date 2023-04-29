using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private long _integerPart = 0;
    private uint _partialPart = 0;

    public Money(long integer, uint partial)
    {
        if (partial >= 100) partial = 0;

        this._integerPart = integer;
        this._partialPart = partial;
    }

    public (long integer, uint partial) GetValue()
    {
        return (this._integerPart, this._partialPart);
    }

    public void Print(string label)
    {
        print(label + "---------" + this._integerPart + "." + (int)(this._partialPart));
    }

    public void Increase(Money money)
    {
        var (integer, partial) = money.GetValue();

        if (partial + this._partialPart > 100)
        {
            this._integerPart += 1;
            this._partialPart = this._partialPart + partial - 100;
        }
        else if (partial + this._partialPart == 100)
        {
            this._integerPart += 1;
            this._partialPart = 0;
        }

        this._integerPart += integer;
        Print("Increase");
    }

    public void Decrease(Money money)
    {
        var (integer, partial) = money.GetValue();

        var intPartialDecrease = (int)(this._partialPart) - (int)(partial);

        if (intPartialDecrease < 0)
        {
            this._integerPart -= 1;
            this._partialPart = this._partialPart - partial + 100;
        } 
        else if (intPartialDecrease == 0)
        {
            this._integerPart -= 1;
            this._partialPart = 0;
        }

        this._integerPart -= integer;
        Print("Decrease");
    }

    public bool Equal(Money money)
    {
        var (integer, partial) = money.GetValue();

        return this._integerPart == integer && this._partialPart == partial;
    }

    public void Multiplication(float value)
    {
        var n = float.Parse(this._integerPart + "," + this._partialPart);
        var res = n * value;

        this._integerPart = (int)res;
        this._partialPart = (uint)(Math.Round((double)(res - (int)res), 2) * 100);

        Print("Multiplication");
    }

    public void Division(float value)
    {
        var n = float.Parse(this._integerPart + "," + this._partialPart);
        var res = n / value;

        this._integerPart = (int)res;
        this._partialPart = (uint)(Math.Round((double)(res - (int)res), 2) * 100);

        Print("Division");
    }
}
