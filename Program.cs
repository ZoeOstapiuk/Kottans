using System;

class Program
{
    public static string GetCreditCardVendor(string CardNumber)
    {
        if (!IsCreditCardNumberValid(CardNumber))
        {
            return "Unknown";
        }

        for (int i = 0; i < CardNumber.Length; i++)
        {
            if (CardNumber[i] == ' ')
            {
                CardNumber = CardNumber.Remove(i, 1);
                i--;
            }
        }
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
                return "American Express";
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
        for (int i = 0; i < CardNumber.Length; i++)
        {
            if (CardNumber[i] == ' ')
            {
                CardNumber = CardNumber.Remove(i, 1);
                i--;
            }
        }
        if (CardNumber.Length < 12 || CardNumber.Length > 19)   //Minimal length allowed for vendors available (Maestro) and max (Maestro, Visa).
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
        for (int i = 0; i < CardNumber.Length; i++)
        {
            if (CardNumber[i] == ' ')
            {
                CardNumber = CardNumber.Remove(i, 1);
                i--;
            }
        }

        if (CardNumber.Length < 12 || CardNumber.Length > 19)   //Minimal length allowed for vendors available (Maestro) and max (Maestro, Visa).
        {
            return "Unknown vendor. Cannot generate next valid card number.";
        }

        string CurrentVendor;
        if (CardNumber[0] == '4') CurrentVendor = "Visa";
        else if (Int32.Parse(CardNumber.Substring(0, 2)) >= 51 && Int32.Parse(CardNumber.Substring(0, 2)) <= 55) CurrentVendor = "MasterCard";
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 34 || Int32.Parse(CardNumber.Substring(0, 2)) == 37) CurrentVendor = "American Express";
        else if (Int32.Parse(CardNumber.Substring(0, 2)) == 50 || (Int32.Parse(CardNumber.Substring(0, 2)) >= 56 && Int32.Parse(CardNumber.Substring(0, 2)) <= 69))
            CurrentVendor = "Maestro";
        else if (Int32.Parse(CardNumber.Substring(0, 4)) >= 3528 && Int32.Parse(CardNumber.Substring(0, 4)) <= 3589) CurrentVendor = "JCB";
        else return "Unknown vendor. Cannot generate next valid card number.";

        long iCardNumber = long.Parse(CardNumber);
        do
        {
            iCardNumber++;
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
        Console.WriteLine(GetCreditCardVendor(Console.ReadLine()));
        Console.WriteLine(IsCreditCardNumberValid(Console.ReadLine()));
        Console.WriteLine(GenerateNextCreditCardNumber(Console.ReadLine()));
        Console.ReadKey();
    }
}
