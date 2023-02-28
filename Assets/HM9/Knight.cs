using System;

public class Knight : Unit
{
  public int resistance { get; set; } = 100;

  public Knight(int power, int speed, int resistance) : base(power, speed)
  {
    this.resistance = resistance;
  }

  public override void Attack()
  {
    Console.WriteLine("Knight Attack: " + power + ", Speed: " + speed + ", Resistance: " + resistance);
  }
}
