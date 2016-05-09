using System;

class Program
{
    public static string GetCreditCardVendor(string CardNumber)
    {
        if (!IsCreditCardNumberValid(CardNumber))
        {
            return "Unknown";
        }

        CardNumber = CardNumber.Replace(" ", "");

        if (CardNumber[0] == '4')
        {
            if (CardNumber.Length == 13 || CardNumber.Length == 16 || CardNumber.Length == 19)
                return "Visa";
            return "Unknown";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) >= 51 && Int32.Parse(CardNumber.Substring(0, 2)) <= 55)
        {
            if (CardNumber.Length == 16)
                return "MasterCard";
            return "Unknown";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) == 34 || Int32.Parse(CardNumber.Substring(0, 2)) == 37)
        {
            if (CardNumber.Length == 15)
                return "AmericanExpress";
            return "Unknown.";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) == 50 || (Int32.Parse(CardNumber.Substring(0, 2)) >= 56 && Int32.Parse(CardNumber.Substring(0, 2)) <= 69))
        {
            if (CardNumber.Length >= 12 && CardNumber.Length <= 19)
                return "Maestro";
            return "Unknown";
        }
        if (Int32.Parse(CardNumber.Substring(0, 4)) >= 3528 && Int32.Parse(CardNumber.Substring(0, 4)) <= 3589)
        {
            if (CardNumber.Length == 16)
                return "JCB";
        }
        return "Unknown";
    }
    public static bool IsCreditCardNumberValid(string CardNumber)
    {
        CardNumber = CardNumber.Replace(" ", "");

        if (CardNumber.Length < 4 || CardNumber.Length > 19) // Minimal number of digits for number to be recognized/maximal number of digits allowed for vendors available. 
        {
            return false;
        }
        if (CardNumber[0] == '4')
        {
            if (CardNumber.Length != 13 && CardNumber.Length != 16 && CardNumber.Length != 19) return false;
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) >= 51 && Int32.Parse(CardNumber.Substring(0, 2)) <= 55)
        {
            if (CardNumber.Length != 16) return false;
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 34 || Int32.Parse(CardNumber.Substring(0, 2)) == 37)
        {
            if (CardNumber.Length != 15) return false;
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 50 || (Int32.Parse(CardNumber.Substring(0, 2)) >= 56 && Int32.Parse(CardNumber.Substring(0, 2)) <= 69))
        {
            if (CardNumber.Length < 12 && CardNumber.Length > 19) return false;
        }
        else if (Int32.Parse(CardNumber.Substring(0, 4)) >= 3528 && Int32.Parse(CardNumber.Substring(0, 4)) <= 3589)
        {
            if (CardNumber.Length != 16) return false;
        }
        else //Number can be valid but vendor is unknown. 
        {
            return false;
        }

        int iSum = 0;
        for (int i = CardNumber.Length - 1; i >= 0; i--)
        {
            iSum += Convert.ToInt16(CardNumber[i].ToString());
            i--;
            if (i >= 0)
            {
                if ((Convert.ToInt16(CardNumber[i].ToString()) * 2) > 9)
                    iSum += (Convert.ToInt16(CardNumber[i].ToString()) * 2) - 9;
                else
                    iSum += Convert.ToInt16(CardNumber[i].ToString()) * 2;
            }
        }
        return (iSum % 10) == 0;
    }
    public static string GenerateNextCreditCardNumber(string CardNumber)
    {
        CardNumber = CardNumber.Replace(" ", "");

        if (CardNumber.Length < 12)   
        {
            return "Unknown vendor. Cannot generate next valid card number.";
        }

        long iCardNumber = long.Parse(CardNumber);

        string CurrentVendor;
        if (CardNumber[0] == '4') //13, 16, 19 digits.
        {
            CurrentVendor = "Visa";
            if (CardNumber.Length != 19 && CardNumber.Length != 16 && CardNumber.Length != 13) return "Unknown vendor. Cannot generate next valid card number.";
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) >= 51 && Int32.Parse(CardNumber.Substring(0, 2)) <= 55) //16 digits.
        {
            CurrentVendor = "MasterCard";
            if (CardNumber.Length != 16) return "Unknown vendor. Cannot generate next valid card number.";
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 34 || Int32.Parse(CardNumber.Substring(0, 2)) == 37) //15 digits.
        {
            CurrentVendor = "AmericanExpress";
            if (CardNumber.Length != 15) return "Unknown vendor. Cannot generate next valid card number.";
        }
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 50 || (Int32.Parse(CardNumber.Substring(0, 2)) >= 56 && Int32.Parse(CardNumber.Substring(0, 2)) <= 69))
        {
            CurrentVendor = "Maestro";
            if (CardNumber.Length < 12 && CardNumber.Length > 19) return "Unknown vendor. Cannot generate next valid card number."; 
        }
        else if (Int32.Parse(CardNumber.Substring(0, 4)) >= 3528 && Int32.Parse(CardNumber.Substring(0, 4)) <= 3589 && CardNumber.Length <= 16) //16 digits.
        {
            CurrentVendor = "JCB";
            if (CardNumber.Length != 16)  return "Unknown vendor. Cannot generate next valid card number.";
        }
        else return "Unknown vendor. Cannot generate next valid card number.";

        do
        {
            iCardNumber++;
            if (CurrentVendor == "AmericanExpress") //A big gap between values allowed to be recognized (34, 37).
            {
                if (iCardNumber.ToString().Substring(0, 2) == "35") //Gap's beginning.
                {
                    iCardNumber = 370000000000000;
                }
            }
            else if (CurrentVendor == "Maestro") //A big gap between values allowed to be recognized (50, 56+).
            {
                if (iCardNumber.ToString().Substring(0, 2) == "51") //Gap's beginning.
                {
                    string Tmp = "56" + iCardNumber.ToString().Substring(2);
                    iCardNumber = long.Parse(Tmp);
                }
                else if (iCardNumber.ToString().Substring(0, 2) == "70" && iCardNumber.ToString().Length != 19) //Add 1 digit.
                {
                    string Tmp = "50" + iCardNumber.ToString().Substring(2) + "0";
                    iCardNumber = long.Parse(Tmp);
                }
            }
            else if (CurrentVendor == "Visa" && iCardNumber.ToString().Length == 13 && iCardNumber.ToString()[0] == '5')
            {
                iCardNumber = 4000000000000000;
            }
            else if (CurrentVendor == "Visa" && iCardNumber.ToString().Length == 16 && iCardNumber.ToString()[0] == '5')
            {
                iCardNumber = 4000000000000000000;
            }
        }
        while (!IsCreditCardNumberValid(iCardNumber.ToString()));

        if (GetCreditCardVendor(iCardNumber.ToString()) != CurrentVendor)
        {
            return "No more card numbers available for this vendor.";
        }
        return iCardNumber.ToString();
    }
    public static void Main()
    {
        //Зчитує три рази номер картки і визначає відповідну властивість.
        //Console.WriteLine(GetCreditCardVendor(Console.ReadLine()));
        Console.WriteLine(IsCreditCardNumberValid(Console.ReadLine()));
        //Console.WriteLine(GenerateNextCreditCardNumber(Console.ReadLine()));
        Console.ReadKey();
    }
}
