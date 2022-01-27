int p1Wins = 0;
int p2Wins = 0;

foreach (string line in System.IO.File.ReadLines(@"C:\Users\nikil\Documents\Developer\ProjectEuler\Problem54-ProjectEuler\p054_poker.txt"))
{
    string[] hands = line.Split('|');
    string[] player1 = hands[0].Split(',');
    string[] player2 = hands[1].Split(',');
    double p1Score = Convert.ToDouble(HandReader(player1));
    double p2Score = Convert.ToDouble(HandReader(player2));
    if(p1Score > p2Score)
        p1Wins++;
    else if(p1Score < p2Score)
        p2Wins++;
}

Console.WriteLine("Player 1 wins {0} times", p1Wins);


string HandReader(string[] cards)
{
    string result = "";
    Dictionary<string, int> values = new();
    values.Add("2", 0);
    values.Add("3", 0);
    values.Add("4", 0);
    values.Add("5", 0);
    values.Add("6", 0);
    values.Add("7", 0);
    values.Add("8", 0);
    values.Add("9", 0);
    values.Add("T", 0);
    values.Add("J", 0);
    values.Add("Q", 0);
    values.Add("K", 0);
    values.Add("A", 0);
    values.Add("H", 0);
    values.Add("D", 0);
    values.Add("C", 0);
    values.Add("S", 0);

    Dictionary<string, string> valuesScores = new();
    valuesScores.Add("2", "02");
    valuesScores.Add("3", "03");
    valuesScores.Add("4", "04");
    valuesScores.Add("5", "05");
    valuesScores.Add("6", "06");
    valuesScores.Add("7", "07");
    valuesScores.Add("8", "08");
    valuesScores.Add("9", "09");
    valuesScores.Add("T", "10");
    valuesScores.Add("J", "11");
    valuesScores.Add("Q", "12");
    valuesScores.Add("K", "13");
    valuesScores.Add("A", "14");

    foreach (string card in cards)
    {
        foreach (char c in card)
        {
            values[c.ToString()]++;
        }
    }
    //Royal Flush
    if (values["T"] == 1 && values["J"] == 1 && values["Q"] == 1 && values["K"] == 1 && values["A"] == 1 &&
        (values["H"] == 5 || values["D"] == 5 || values["C"] == 5 || values["S"] == 5))
        return "10.";
    //Straight Flush or Flush
    if (values["H"] == 5 || values["D"] == 5 || values["C"] == 5 || values["S"] == 5)
    {
        int consecutive = 0;
        foreach (string key in values.Keys)
        {
            if (key == "A")
                break;
            if (values[key] == 0)
                consecutive = 0;
            if (values[key] > 0)
                consecutive++;
            if (consecutive == 5)
            {
                return "9.";
            }
        }
        return "6";
    }
    //Four of a Kind
    if (values["H"] < 3 && values["D"] < 3 && values["C"] < 3 && values["S"] < 3 &&
        values["H"] > 0 && values["D"] > 0 && values["C"] > 0 && values["S"] > 0)
    {
        bool fourOfAKind = false;
        foreach (string key in values.Keys)
        {
            if (key == "H")
                break;
            if (values[key] == 4 && result == "")
            {
                result = "8." + result;
                fourOfAKind = true;
            }
            if (values[key] == 1)
                result += valuesScores[key];
        }
        if(fourOfAKind)
            return result;
    }
    // Straight
    int consec = 0;
    foreach (string key in values.Keys)
    {
        if (key == "H")
            break;
        if (values[key] == 0)
            consec = 0;
        if (values[key] > 0)
            consec++;
        if (consec == 5)
        {
            return "5.";
        }
    }
    //Full House, 3 of a kind, 2 pairs, one Pair, High card
    int pairs = 0;
    int trios = 0;
    result = "";
    string otherCards = "";
    foreach (string key in values.Keys)
    {
        if (key == "H")
            break;
        if (values[key] == 2)
        {
            pairs++;
            if (trios == 0)
                result = valuesScores[key] + result;
        }
        if (values[key] == 3)
        {
            result = valuesScores[key];
            trios++;
        }
            
        if (values[key] == 1 && otherCards == "")
            otherCards = valuesScores[key];
        else if(values[key] == 1 && otherCards != "")
            otherCards = valuesScores[key] + otherCards;
    }
    if (trios == 1 && pairs == 1)
        return "7." + result;
    else if (trios == 1)
        return "4." + result + otherCards;
    else if(pairs == 2)
        return "3." + result + otherCards;
    else if(pairs ==1)
        return "2." + result + otherCards;
    else if(trios == 0 && pairs ==0)
        return "1." + otherCards;

    return "FAILED TO DETERMINE";
}