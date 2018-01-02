namespace AutoService.App
{
    //public delegate void MyEventHandler(string newValue);

    //class EventExample
    //{


    //    private string theValue;
    //    public event MyEventHandler valueChanged;

    //    public string Val
    //    {
    //        set
    //        {
    //            this.theValue = value;
    //            this.valueChanged(theValue);
    //        }
    //    }
    //}

    class StartUp
    {

        static void Main()
        {
            AutoService.Core.Engine.Instance.Run();
        }
    }
}
