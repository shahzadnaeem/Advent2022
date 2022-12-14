namespace Advent2022;

public class Day11Data
{
    public const string SAMPLE = @"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";
    public const string INPUT = @"Monkey 0:
  Starting items: 89, 73, 66, 57, 64, 80
  Operation: new = old * 3
  Test: divisible by 13
    If true: throw to monkey 6
    If false: throw to monkey 2

Monkey 1:
  Starting items: 83, 78, 81, 55, 81, 59, 69
  Operation: new = old + 1
  Test: divisible by 3
    If true: throw to monkey 7
    If false: throw to monkey 4

Monkey 2:
  Starting items: 76, 91, 58, 85
  Operation: new = old * 13
  Test: divisible by 7
    If true: throw to monkey 1
    If false: throw to monkey 4

Monkey 3:
  Starting items: 71, 72, 74, 76, 68
  Operation: new = old * old
  Test: divisible by 2
    If true: throw to monkey 6
    If false: throw to monkey 0

Monkey 4:
  Starting items: 98, 85, 84
  Operation: new = old + 7
  Test: divisible by 19
    If true: throw to monkey 5
    If false: throw to monkey 7

Monkey 5:
  Starting items: 78
  Operation: new = old + 8
  Test: divisible by 5
    If true: throw to monkey 3
    If false: throw to monkey 0

Monkey 6:
  Starting items: 86, 70, 60, 88, 88, 78, 74, 83
  Operation: new = old + 4
  Test: divisible by 11
    If true: throw to monkey 1
    If false: throw to monkey 2

Monkey 7:
  Starting items: 81, 58
  Operation: new = old + 5
  Test: divisible by 17
    If true: throw to monkey 3
    If false: throw to monkey 5";
}
