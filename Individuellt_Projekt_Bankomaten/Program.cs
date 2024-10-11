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
            if (currentUserIndex != -1) // if login is successful show menu
            {
                ShowMenu(accounts, accountBalances, currentUserIndex, usernames, passwords);
            }
            else
            {
                Console.WriteLine("\nDu har anget fel information 3 gånger. Programmet avslutas av säkerhetsskäl!");
                exit = true; // if login is not successful after 3 attempts, exit the program
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
            string password = ReadPassword(); // calls my method to read the password without showing it on the console

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
            Console.WriteLine($"Välkommen {usernames[currentUserIndex]}!");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Saldo & konton");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Insättning & uttag");
            Console.WriteLine("4. Logga ut");
            Console.WriteLine("5. Avsluta programmet");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice)) // reads the user input and checks if it's an integer
            {
                if (choice >= 1 && choice <= 5) // checks if the user input is between 1 and 4
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
                        case 5:
                            Console.WriteLine("Är du säker på att du vill avsluta programmet? Ange \"ja\" för att avsluta annars återgår du till huvudmenyn.");
                            string answer = Console.ReadLine().ToLower();
                            if (answer == "ja")
                            {
                                Console.WriteLine("Avslutar programet");
                                Environment.Exit(0); // exits the program
                            }
                            else
                            {
                                Console.WriteLine("Återgår till huvudmenyn");
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Felaktigt val, försök igen!");
                }
            }
            else
            {
                Console.WriteLine("Felaktigt val, försök igen!");
            }
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
        }
        
    }
    static void ShowAccounts(string[][] accounts, decimal[][] accountBalances, int userIndex, string[] usernames) // method to show the accounts and balances
    {
        Console.WriteLine($"Konton och saldo för {usernames[userIndex]}:");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loops through and shows the accounts and balances
        {
            Console.WriteLine($"{accounts[userIndex][i]}: {accountBalances[userIndex][i].ToString("C")}");
        }

        Console.WriteLine("\nTryck på enter för att återgå till menyval");
        Console.ReadKey();
    }

    static void TransferMoney(string[][] accounts, decimal[][] accountBalances, int userIndex) // method to transfer money between accounts
    {
        int fromAccount; 
        int toAccount;
        decimal amount;
        
        Console.WriteLine("Vilket konto vill du överföra pengar från?");
        for (int i = 0; i < accounts[userIndex].Length; i++) // loop through the accounts and show them
        {
            Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
        }
        
        string input = Console.ReadLine();
        while (!int.TryParse(input, out fromAccount) || fromAccount < 1 || fromAccount > accounts[userIndex].Length) // loop until the user enters a valid account number
        {
            Console.WriteLine("Ogiltigt val. Försök igen.");
            input = Console.ReadLine(); 
        }
        fromAccount--; // subtracts 1 from the input to get the correct index

        Console.WriteLine("Vilket konto vill du överföra pengar till?"); 
        for (int i = 0; i < accounts[userIndex].Length; i++) // loop through the accounts and show them
        {
            if (i != fromAccount)
            {
                Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
            }
        }
        
        input = Console.ReadLine();
        while (!int.TryParse(input, out toAccount) || toAccount < 1 || toAccount > accounts[userIndex].Length || toAccount == fromAccount + 1) // loop until the user enters a valid account number
        {
            Console.WriteLine("Ogiltigt val. Du kan inte välja samma konto att överföra pengar till. Försök igen.");
            input = Console.ReadLine();
        }
        toAccount--; // subtracts 1 from the input to get the correct index
        
        Console.WriteLine($"Hur mycket pengar vill du överföra från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}?");
        input = Console.ReadLine();
        
        while (!decimal.TryParse(input, out amount) || amount <= 0) // loop until the user enters a valid amount
        {
            Console.WriteLine("Ogiltigt belopp. Försök igen.");
            input = Console.ReadLine();
        }
        
        if (accountBalances[userIndex][fromAccount] < amount) // Check if the user has enough balance for the transfer
        {
            Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra överföringen.");
            Console.WriteLine("\nTryck på enter för att återgå till menyval");
            Console.ReadKey();
            return;
        }
        
        accountBalances[userIndex][fromAccount] -= amount;
        accountBalances[userIndex][toAccount] += amount;

        Console.WriteLine($"Överföringen lyckades! {amount:C} har överförts från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}.");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][fromAccount]}: {accountBalances[userIndex][fromAccount]:C}");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][toAccount]}: {accountBalances[userIndex][toAccount]:C}");
        Console.WriteLine("\nTryck på enter för att återgå till menyval");
        Console.ReadKey();
    }

static void DepositWithdrawMoney(string[][] accounts, decimal[][] accountBalances, int userIndex, string[] passwords) // method to deposit and withdraw money
{
    int account;
    
    Console.WriteLine("Vilket konto vill du sätta in eller ta ut pengar från?");
    for (int i = 0; i < accounts[userIndex].Length; i++)
    {
        Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
    }
    
    string input = Console.ReadLine();
    while (!int.TryParse(input, out account) || account < 1 || account > accounts[userIndex].Length) // loop until the user enters a valid account number
    {
        Console.WriteLine("Ogiltigt val. Försök igen.");
        input = Console.ReadLine();
    }
    account--; // subtracts 1 from the input to get the correct index
    
    Console.WriteLine("Vill du sätta in eller ta ut pengar?");
    Console.WriteLine("1. Sätta in pengar");
    Console.WriteLine("2. Ta ut pengar");
    int choice;
    
    input = Console.ReadLine();
    while (!int.TryParse(input, out choice) || choice < 1 || choice > 2) // loop until the user enters a valid choice
    {
        Console.WriteLine("Ogiltigt val. Försök igen.");
        input = Console.ReadLine();
    }

    if (choice == 1)
    {
        decimal amount;
        Console.WriteLine("Hur mycket pengar vill du sätta in?");
        
        input = Console.ReadLine();
        while (!decimal.TryParse(input, out amount) || amount <= 0) // loop until the user enters a valid amount
        {
            Console.WriteLine("Ogiltigt belopp. Försök igen.");
            input = Console.ReadLine();
        }
        
        accountBalances[userIndex][account] += amount; // add amount to account
        Console.WriteLine($"{amount:C} har satts in på {accounts[userIndex][account]}.");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
        Console.WriteLine("\nTryck på enter för att återgå till menyval");
        Console.ReadKey();
    }
    else if (choice == 2) 
    {
        decimal amount;
        Console.WriteLine("Hur mycket pengar vill du ta ut?");
        
        input = Console.ReadLine();
        while (!decimal.TryParse(input, out amount) || amount <= 0) // loop until the user enters a valid amount
        {
            Console.WriteLine("Ogiltigt belopp. Försök igen.");
            input = Console.ReadLine();
        }
        
        if (accountBalances[userIndex][account] < amount) // Check if the user has enough balance for the withdrawal
        {
            Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra uttaget.");
            Console.WriteLine("\nTryck på enter för att återgå till menyval");
            Console.ReadKey();
            return;
        }
        
        if (!VerifyPassword(passwords, userIndex)) // Verify the password
        {
            return;
        }
        
        accountBalances[userIndex][account] -= amount; // Withdraw money
        Console.WriteLine($"{amount:C} har tagits ut från {accounts[userIndex][account]}.");
        Console.WriteLine($"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
        Console.WriteLine("\nTryck på enter för att återgå till menyval");
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
            string password = ReadPassword(); // calls my method to read the password without showing it on the console
            if (password == passwords[userIndex]) // checks if the password is correct
            {
                isPinCorrect = true; // if the password is correct, the user can withdraw money
            }
            else
            {
                attempts++; // if the password is incorrect increase the attempts by 1
                if (attempts < 3) 
                {
                    Console.WriteLine("Felaktig pinkod, försök igen.");
                }
            }
        }

        if (!isPinCorrect) // if the user has tried 3 times and the password is still incorrect
        {
            Console.WriteLine("För många felaktiga försök. Åtgärden har avbrutits.");
            Console.WriteLine("\nTryck på enter för att återgå till menyval");
            Console.ReadKey();
        }
        return isPinCorrect; // returns if the password is correct or not
    }
    static string ReadPassword() // method to read the password with * instead of showing the password
    {
        string password = "";
        ConsoleKeyInfo key; 

        do
        {
            key = Console.ReadKey(intercept: true); // intercepts the key to not show it on the console
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)  // if the key is not backspace or enter
            {
                password += key.KeyChar;  // add the key to the password
                Console.Write("*"); // show * instead of the key
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0) // if the key is backspace and the password length is greater than 0
            {
                password = password.Substring(0, password.Length - 1); // remove the last character from the password
                Console.Write("\b \b");  // remove the * from the console
            }
        }
        while (key.Key != ConsoleKey.Enter);  

        Console.WriteLine();
        return password; // return the password
    }
}