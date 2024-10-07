namespace Individuellt_Projekt_Bankomaten;

class Program
{
    static void Main(string[] args)
    {
        // array för mina användarnamn och pinkoder
        string[] usernames = { "Alexander", "Karin", "Johan", "Fredrik", "Lisa" };
        string[] passwords = { "1234", "1111", "2222", "3333", "4444" };
        // Jagged array för konton och saldo
        string[][] accounts =
        {
            new string[] { "Sparkonto", "Lönekonto", "Kreditkort", "Resekonto" }, // Alex
            new string[] { "Sparkonto", "Matkonto", "Aktiekonto", }, // Karin
            new string[] { "Gemensamt konto"}, // Johan
            new string[] { "Godiskonto", "Autogirokonto", "Avanza" }, // Fredrik
            new string[] { "Hundkonto", "Nöjes konto" } // Lisa
        };
        decimal[][] accountBalances = 
        {
            new decimal[] { 100000m, 500000.69m, -1000m, 2500m },  // Alexander
            new decimal[] { 200000m, 10000m, 650m },              // Karin
            new decimal[] { 1000000m },                     // Johan
            new decimal[] { 4500m, 90000m, 5000.86m, 40000m },      // Fredrik
            new decimal[] { 3000m, 2000m }                  // Lisa
        };

        string username = "";
        string password = "";
        int currentUserIndex = -1; // för att se vilken användare som är inloggad 
        int attempts = 0;
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Välkommen till bankomaten!");
            Console.Write("Användarnamn:");
            username = Console.ReadLine();
            Console.Write("Lösenord:");
            password = Console.ReadLine();

            for (int i = 0; i < usernames.Length; i++)
            {
                if (username == usernames[i] && password == passwords[i])
                {
                    Console.WriteLine($"\nInloggning lyckades! Välkommen {username}!");
                    currentUserIndex = i; // sparar index för inloggad användare
                    isRunning = false;
                    break;
                }
            }

            if (currentUserIndex == -1)
            {
                attempts++;
                Console.WriteLine("Felaktigt användarnamn eller lösenord, försök igen!");
                if (attempts >= 3)
                {
                    Console.WriteLine("För många felaktiga inloggningsförsök, försök igen senare!");
                    return;
                }
            }
        }

        bool loggedIn = true;
        while (loggedIn && currentUserIndex != -1)
        {
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Saldo & konton");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Insättning & uttag");
            Console.WriteLine("4. Logga ut");
            if (int.TryParse(Console.ReadLine(), out int choice))
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
                        DepositWithdrawMoney(accounts, accountBalances, currentUserIndex);
                        break;
                    case 4:
                        Console.WriteLine("Tack för att du använde bankomaten!");
                        loggedIn = false;
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

    static void ShowAccounts(string[][] accounts, decimal[][] accountBalances, int userIndex, string[] usernames)
    {
        Console.WriteLine($"Konton och saldo för {usernames[userIndex]}:");
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            Console.WriteLine($"{accounts[userIndex][i]}: {accountBalances[userIndex][i].ToString("C")}");
        }

        Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
        Console.ReadKey();
    }

    static void TransferMoney(string[][] accounts, decimal[][] accountBalances, int userIndex)
    {
        Console.WriteLine("Vilket konto vill du överföra pengar från?");
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
        }


        if (!int.TryParse(Console.ReadLine(), out int fromAccount) || fromAccount < 1 ||
            fromAccount > accounts[userIndex].Length)
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        fromAccount--;

        Console.WriteLine("Vilket konto vill du överföra pengar till?");
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            if (i != fromAccount)
            {
                Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
            }
        }

        if (!int.TryParse(Console.ReadLine(), out int toAccount) || toAccount < 1 ||
            toAccount > accounts[userIndex].Length || toAccount == fromAccount + 1)
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        toAccount--;

        Console.WriteLine(
            $"Hur mycket pengar vill du överföra från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}?");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Ogiltigt belopp, försök igen.");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
            return;
        }

        if (accountBalances[userIndex][fromAccount] < amount)
        {
            Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra överföringen.");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
            return;
        }

        accountBalances[userIndex][fromAccount] -= amount;
        accountBalances[userIndex][toAccount] += amount;

        Console.WriteLine(
            $"Överföringen lyckades! {amount:C} har överförts från {accounts[userIndex][fromAccount]} till {accounts[userIndex][toAccount]}.");
        Console.WriteLine(
            $"Nytt saldo för {accounts[userIndex][fromAccount]}: {accountBalances[userIndex][fromAccount]:C}");
        Console.WriteLine(
            $"Nytt saldo för {accounts[userIndex][toAccount]}: {accountBalances[userIndex][toAccount]:C}");
        Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
        Console.ReadKey();
    }

    static void DepositWithdrawMoney(string[][] accounts, decimal[][] accountBalances, int userIndex)
    {
        Console.WriteLine("Vilket konto vill du sätta in eller ta ut pengar från?");
        for (int i = 0; i < accounts[userIndex].Length; i++)
        {
            Console.WriteLine($"{i + 1}. {accounts[userIndex][i]} - Saldo: {accountBalances[userIndex][i]:C}");
        }

        if (!int.TryParse(Console.ReadLine(), out int account) || account < 1 || account > accounts[userIndex].Length)
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        account--;

        Console.WriteLine("Vill du sätta in eller ta ut pengar?");
        Console.WriteLine("1. Sätta in pengar");
        Console.WriteLine("2. Ta ut pengar");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Ogiltigt val, försök igen.");
            return;
        }

        if (choice == 1)
        {
            Console.WriteLine("Hur mycket pengar vill du sätta in?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ogiltigt belopp, försök igen.");
                return;
            }

            accountBalances[userIndex][account] += amount;
            Console.WriteLine($"{amount:C} har satts in på {accounts[userIndex][account]}.");
            Console.WriteLine(
                $"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Hur mycket pengar vill du ta ut?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ogiltigt belopp, försök igen.");
                return;
            }

            if (accountBalances[userIndex][account] < amount)
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra uttaget.");
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
                Console.ReadKey();
                return;
            }

            accountBalances[userIndex][account] -= amount;
            Console.WriteLine($"{amount:C} har tagits ut från {accounts[userIndex][account]}.");
            Console.WriteLine($"Nytt saldo för {accounts[userIndex][account]}: {accountBalances[userIndex][account]:C}");
            Console.WriteLine("\nTryck på valfri tangent för att återgå till menyval");
            Console.ReadKey();
        }
    }
}