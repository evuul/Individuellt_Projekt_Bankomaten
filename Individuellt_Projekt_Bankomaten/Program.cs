namespace Individuellt_Projekt_Bankomaten;

class Program
{
    static void Main(string[] args)
    {
        // array to store usernames and passwords
        string[] usernames = { "Alexander", "Karin", "Johan", "Fredrik", "Lisa" };
        string[] passwords = { "1234", "1111", "2222", "3333", "4444" };
        
        // Jagged array to store different accounts 
        string[][] accounts =
        {
            new string[] { "Sparkonto", "Lönekonto", "Kreditkort", "Resekonto" }, // Alex
            new string[] { "Sparkonto", "Matkonto", "Aktiekonto", }, // Karin
            new string[] { "Gemensamt konto" }, // Johan
            new string[] { "Godiskonto", "Autogirokonto", "Avanza" }, // Fredrik
            new string[] { "Hundkonto", "Nöjes konto" } // Lisa
        };
        // Jagged array to store different account balances
        decimal[][] accountBalances =
        {
            new decimal[] { 100000m, 500000.69m, -1000m, 2500m }, // Alexander
            new decimal[] { 200000m, 10000m, 650m }, // Karin
            new decimal[] { 1000000m }, // Johan
            new decimal[] { 4500m, 90000m, 5000.85m}, // Fredrik
            new decimal[] { 3000m, 2000m } // Lisa
        };
        // program loop runs until user shuts down the program
        bool exit = false;
        while (!exit)
        {
            Console.Clear(); // clears the console before a new login
            int currentUserIndex = Login(usernames, passwords); // calls my method to login
            if (currentUserIndex != -1) // if login is sucessful show menu
            {
                ShowMenu(accounts, accountBalances, currentUserIndex, usernames, passwords);
            }
            else
            {
                Console.WriteLine("\nDu har anget fel login information 3 gånger, programmet avslutas av säkerhetsskäl!");
                exit = true; // if login is not sucessful after 3 attempts, exit the program
            }
        }
    }
    // methods
    
    static int Login(string[] usernames, string[] passwords) // method to handle login
    {
        int attempts = 0;
        while (attempts < 3) // users has 3 attempts to login
        {
            Console.WriteLine("Välkommen till bankomaten!");
            Console.Write("Användarnamn:");
            string username = Console.ReadLine();
            Console.Write("Lösenord:");
            string password = Console.ReadLine();

            for (int i = 0; i < usernames.Length; i++) // loop through the usernames and passwords to verify the login
            {
                if (username == usernames[i] && password == passwords[i])
                {
                    return i; // if the login is correct, return the index of the user
                }
            }
            attempts++;
            Console.WriteLine("Felaktigt användarnamn eller lösenord, försök igen!");
        }
        return -1; // if the user has tried to login 3 times, return -1
    }
    // method to show the menu
    static void ShowMenu(string[][] accounts, decimal[][] accountBalances, int currentUserIndex, string[] usernames, string[] passwords)
    {
        bool loggedIn = true;
        while (loggedIn) // loops until the user logs out
        {
            Console.Clear();
            Console.WriteLine($"Inloggningen lyckades! \nVälkommen {usernames[currentUserIndex]}!");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Saldo & konton");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Insättning & uttag");
            Console.WriteLine("4. Logga ut");
            if (int.TryParse(Console.ReadLine(), out int choice)) // reads the user input and checks if it's an integer
            {
                switch (choice)
                {
                    case 1:
                        ShowAccounts(accounts, accountBalances, currentUserIndex, usernames);
                        break;
                    case 2:
                        TransferMoney(accounts, accountBalances, currentUserIndex);
                        break;
                    case 3:
                        DepositWithdrawMoney(accounts, accountBalances, currentUserIndex, passwords);
                        break;
                    case 4:
                        Console.WriteLine("Tack för att du använde bankomaten!");
                        loggedIn = false; // logs out the user
                        break;
                    default:
                        Console.WriteLine("Felaktigt val, försök igen!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Felaktigt val, försök igen!");
            }
        }
    }
    // method to show the accounts and balances
    static void ShowAccounts(string[][] accounts, decimal[][] accountBalances, int userIndex, string[] usernames)
    {
        Console.WriteLine($"Konton och saldo för {usernames[userIndex]}:");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loops through and shows the accounts and balances
        {
            Console.WriteLine($"{accounts[userIndex][i]}: {accountBalances[userIndex][i].ToString("C")}");
        }

        Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
        Console.ReadKey();
    }

    static void TransferMoney(string[][] accounts, decimal[][] accountBalances, int userIndex) // method to transfer money between accounts
    {
        Console.WriteLine("Vilket konto vill du överföra pengar från?");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loops through and shows the accounts
        {
            Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
        }
        
        if (!int.TryParse(Console.ReadLine(), out int fromAccount) || fromAccount < 1 || fromAccount > accounts[userIndex].Length) // reads input and checks if it's an integer
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        fromAccount--; // subtracts 1 from the input to get the correct index

        Console.WriteLine("Vilket konto vill du överföra pengar till?");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loops through and shows the accounts
        {
            if (i != fromAccount) // if the account is not the same as the from account
            {
                Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}"); 
            }
        }

        if (!int.TryParse(Console.ReadLine(), out int toAccount) || toAccount < 1 || toAccount > accounts[userIndex].Length || toAccount == fromAccount + 1) // reads the input and checks if it's an integer
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return; // if the input is invalid, return
        }

        toAccount--; // subtracts 1 from the input to get the correct index

        Console.WriteLine($"Hur mycket pengar vill du överföra från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}?"); // asks how much money they want to transfer
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0) 
        {
            Console.WriteLine("Ogiltigt belopp, försök igen.");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
            return;
        }

        if (accountBalances[userIndex][fromAccount] < amount) // checks if the user has enough money to transfer
        {
            Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra överföringen.");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
            return;
        }

        accountBalances[userIndex][fromAccount] -= amount; // subtracts the amount from the from account
        accountBalances[userIndex][toAccount] += amount; // adds the amount to the to account
        
        // shows the result after the transfer
        Console.WriteLine($"Överföringen lyckades! {amount:C} har överförts från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}.");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][fromAccount]}: {accountBalances[userIndex][fromAccount]:C}");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][toAccount]}: {accountBalances[userIndex][toAccount]:C}");
        Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
        Console.ReadKey();
    }

    static void DepositWithdrawMoney(string[][] accounts, decimal[][] accountBalances, int userIndex, string[] passwords) // method to deposit and withdraw money
    {
        Console.WriteLine("Vilket konto vill du sätta in eller ta ut pengar från?");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loops through and shows the accounts
        {
            Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
        }

        if (!int.TryParse(Console.ReadLine(), out int account) || account < 1 || account > accounts[userIndex].Length) // check input from user
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        account--; // subtracts 1 from the input to get the correct index

        Console.WriteLine("Vill du sätta in eller ta ut pengar?");
        Console.WriteLine("1. Sätta in pengar");
        Console.WriteLine("2. Ta ut pengar");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 2) // reads the input and checks if it's an integer
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        if (choice == 1)
        {
            Console.WriteLine("Hur mycket pengar vill du sätta in?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)  // checks if it is a valid amount
            {
                Console.WriteLine("Ogiltigt belopp, försök igen.");
                return;
            }

            accountBalances[userIndex][account] += amount; // adds the amount to the account
            Console.WriteLine($"{amount:C} har satts in på {accounts[userIndex][account]}.");
            Console.WriteLine($"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Hur mycket pengar vill du ta ut?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)  // checks if it is a valid amount
            {
                Console.WriteLine("Ogiltigt belopp, försök igen.");
                return;
            }

            if (accountBalances[userIndex][account] < amount) // checks so that the user have enough money on the account
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra uttaget.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
                Console.ReadKey();
                return;
            }

            if (!VerifyPassword(passwords, userIndex)) // calls my method to verify the password
            {
                return; // if the password is incorrect, the user can't withdraw money
            }
            accountBalances[userIndex][account] -= amount; // subtracts the amount from the account
            Console.WriteLine($"{amount:C} har tagits ut från {accounts[userIndex][account]}.");
            Console.WriteLine($"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
        }
    }

    static bool VerifyPassword(string[] passwords, int userIndex) // method to verify the password
    {
        Console.WriteLine("Verifiera ditt uttag med din pinkod:"); 
        int attempts = 0; // user has 3 attempts to enter the correct password
        bool isPinCorrect = false; // boolean to check if the password is correct

        while (attempts < 3 && !isPinCorrect) // loops until the user has entered the correct password or has tried 3 times
        {
            string password = Console.ReadLine();
            if (password == passwords[userIndex]) // checks if the password is correct
            {
                isPinCorrect = true; // if the password is correct, the user can withdraw money
            }
            else
            {
                attempts++;
                if (attempts < 3) 
                {
                    Console.WriteLine("Felaktig pinkod, försök igen.");
                }
            }
        }

        if (!isPinCorrect) // if the user has tried 3 times and the password is still incorrect
        {
            Console.WriteLine("För många felaktiga försök. Åtgärden har avbrutits.");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
        }
        return isPinCorrect; // returns if the password is correct or not
    }
}