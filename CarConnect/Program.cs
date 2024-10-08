using CarConnect.MainModule;
using CarConnect.Model;
using CarConnect.Dao;

namespace CarConnect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarConnectMenu carConnectMenu = new CarConnectMenu();
            carConnectMenu.Run();
        }
    }
}
