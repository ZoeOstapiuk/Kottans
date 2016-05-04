using System;

class Program
{
    public static string GetCreditCardVendor(string CardNumber)
    {
        if (CardNumber[0] == '4')
        {
            return "Visa";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) >= 51 && Int32.Parse(CardNumber.Substring(0, 2)) <= 55)
        {
            return "MasterCard";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) == 34 || Int32.Parse(CardNumber.Substring(0, 2)) == 37)
        {
            return "American Express";
        }
        if (Int32.Parse(CardNumber.Substring(0, 2)) == 50 || (Int32.Parse(CardNumber.Substring(0, 2)) >= 56 && Int32.Parse(CardNumber.Substring(0, 2)) <= 69))
        {
            return "Maestro";
        }
        if (Int32.Parse(CardNumber.Substring(0, 4)) >= 3528 && Int32.Parse(CardNumber.Substring(0, 4)) <= 3589)
        {
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
        int iSum = 0;
        for (int i = CardNumber.Length - 1; i >= 0; i--)
        {
            if ((Convert.ToInt16(CardNumber[i]) * 2) > 9)
                iSum += (Convert.ToInt16(CardNumber[i]) * 2) - 9;
            else
                iSum += Convert.ToInt16(CardNumber[i]) * 2;
            i--;
            if (i >= 0)
                iSum += Convert.ToInt16(CardNumber[i]) * 2;
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
        long iCardNumber = long.Parse(CardNumber);
        do
        {
            iCardNumber++;
        }
        while (!IsCreditCardNumberValid(iCardNumber.ToString()));
        return iCardNumber.ToString();
    }
    public static void Main()
    {
        //Приклад роботи з функціями. Зчитує три рази номер картки і визначає відповідну властивість.
        Console.WriteLine(GetCreditCardVendor(Console.ReadLine()));
        Console.WriteLine(IsCreditCardNumberValid(Console.ReadLine()));
        Console.WriteLine(GenerateNextCreditCardNumber(Console.ReadLine()));
        Console.ReadKey();
    }
}
