namespace AutoService.App
{
    class StartUp
    {
        static void Main()
        {
            var engine = new AutoService.Core.Engine();
            engine.Run();
        }
    }
}
