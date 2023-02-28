using System;

public class Unit
{
  public int power { get; set; } = 10;
  public int speed { get; set; } = 10;

  public Unit(int power, int speed)
  {
    this.power = power;
    this.speed = speed;
  }

  public virtual void Attack()
  {
    Console.WriteLine("Unit Attack: " + power + ", Speed: " + speed);
  }
}
