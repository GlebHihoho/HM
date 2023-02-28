using System;

class Program {
  static void Main(string[] args) {
    Unit unit = new Unit(20, 20);

    unit.Attack();

    Knight knight = new Knight(500, 50, 200);

    knight.Attack();
    knight.resistance = 500;
    knight.Attack();

    Mage mage = new Mage(100, 70, 2000);

    mage.Attack();

    Archer archer = new Archer(200, 200, 70);

    archer.Attack();
  }
}
