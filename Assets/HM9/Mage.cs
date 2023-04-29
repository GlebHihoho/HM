using System;

public class Mage : Unit
{
  public int mana { get; set; } = 1000;

  public Mage(int power, int speed, int mana) : base(power, speed)
  {
    this.mana = mana;
  }

  public override void Attack()
  {
    Console.WriteLine("Mage Attack: " + power + ", Speed: " + speed + ", Mana: " + mana);
  }
}
