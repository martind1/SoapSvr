namespace SdblDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //System.Diagnostics.Debug.WriteLine("Hello, World!");
            var dataService = new DataService();
            dataService.UploadSDB("aha");
        }
    }
}