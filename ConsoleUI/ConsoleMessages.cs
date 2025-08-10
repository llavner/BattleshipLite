using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLite;

public static class ConsoleMessages
{
    public static void WelcomeMessage()
    {
        Console.WriteLine("**********************************************");
        Console.WriteLine("*                                            *");
        Console.WriteLine("* Welcome to Battleship Lite made by Marcus  *");
        Console.WriteLine("*                                            *");
        Console.WriteLine("**********************************************");
        Console.WriteLine();
    }

    public static void ShipLocationOptions()
    {
        Console.WriteLine("Valid Shiplocations are A-E and 1-5");
    }

    public static void GoodByeMessage()
    {
        Console.WriteLine("***************************************************");
        Console.WriteLine("*                                                 *");
        Console.WriteLine("* Goodbye & Thank you for using the application.  *");
        Console.WriteLine("*                                                 *");
        Console.WriteLine("***************************************************");
        Console.WriteLine();
    }
}
