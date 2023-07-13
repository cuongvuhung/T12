using T12.SERVICE;

namespace T12
{
    internal class Program
    {
        static void Main()
        {
            ScreenService screenService = new();
            screenService.LoginScreen();
        }
    }
}