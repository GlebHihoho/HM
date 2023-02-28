using System;

public class Archer : Unit
{
  public int agility { get; set; } = 500;

  public Archer(int power, int speed, int agility) : base(power, speed)
  {
    this.agility = agility;
  }

  public override void Attack()
  {
    Console.WriteLine("Archer Attack: " + power + ", Speed: " + speed + ", Agility: " + agility);
  }
}
