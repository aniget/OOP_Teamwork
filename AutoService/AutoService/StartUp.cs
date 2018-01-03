using System.Threading.Tasks;

namespace AutoService
{
    class StartUp
    {
        static void Main()
        {
            AutoService.Core.Engine.Instance.Run();
        }
    }
}
