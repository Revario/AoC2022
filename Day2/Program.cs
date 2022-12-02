
using System.Runtime.CompilerServices;

var input = File.ReadLines(new Utils.Input("2").GetInputFilePath());


Console.WriteLine(input.Sum(l => CalcPlayPoints(ConvertToPlay(l, false))));
Console.WriteLine(input.Sum(l => CalcPlayPoints(ConvertToPlay(l, true))));


Play ConvertToPlay(string l, bool isPart2)
{
    Hand opp = l.First() switch
    {
        'A' => Hand.Rock,
        'B' => Hand.Paper,
        'C' => Hand.Scissor,
        _ => throw new ArgumentException()
    };

    Hand me = l.Last() switch
        {
            'X' => Hand.Rock,
            'Y' => Hand.Paper,
            'Z' => Hand.Scissor,
            _ => throw new ArgumentException()
        };
    if(isPart2)
    {
        me = CalculateMyHandForPart2(opp, CalcResultForHandPart2(me));
    }
    return new Play(opp, me);

}

int CalcPlayPoints(Play p) => CalcResultPoints(CalcResult(p)) + CalculateMyHandPoint(p.me);
int CalcResultPoints(Result r) => r switch
{
   Result.Win => 6,
   Result.Loss => 0,
   Result.Even => 3
};

int CalculateMyHandPoint(Hand h) => h switch
{
    Hand.Rock => 1,
    Hand.Paper => 2,
    Hand.Scissor => 3
};

Hand CalculateMyHandForPart2(Hand opponentHand, Result wishedResult) => (opponentHand, wishedResult) switch
{

    { opponentHand: Hand.Rock, wishedResult: Result.Win } => Hand.Paper,
    { opponentHand: Hand.Rock, wishedResult: Result.Loss } => Hand.Scissor,
    { opponentHand: Hand.Paper, wishedResult: Result.Win } => Hand.Scissor,
    { opponentHand: Hand.Paper, wishedResult: Result.Loss } => Hand.Rock,
    { opponentHand: Hand.Scissor, wishedResult: Result.Win } => Hand.Rock,
    { opponentHand: Hand.Scissor, wishedResult: Result.Loss } => Hand.Paper,
    _ => opponentHand
};

Result CalcResult(Play h) => h switch
{
    { opponent: Hand.Paper, me: Hand.Rock } => Result.Loss,
    { opponent: Hand.Scissor, me: Hand.Paper } => Result.Loss,
    { opponent: Hand.Rock, me: Hand.Scissor } => Result.Loss,
    { opponent: Hand.Paper, me: Hand.Scissor } => Result.Win,
    { opponent: Hand.Scissor, me: Hand.Rock } => Result.Win,
    { opponent: Hand.Rock, me: Hand.Paper } => Result.Win,
    _ => Result.Even
};

//Result CalcResultPart2(Play p) => CalcResultForHandPart2(p.me);
Result CalcResultForHandPart2(Hand h) => h switch
{
    Hand.Rock  => Result.Loss,
    Hand.Paper  => Result.Even,
    Hand.Scissor  => Result.Win
};

record Play(Hand opponent, Hand me);

enum Result
{
    Win,
    Loss,
    Even
}

enum Hand
{
    Rock,
    Paper,
    Scissor
}
