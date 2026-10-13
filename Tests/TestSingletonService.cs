namespace Tests
{
    internal class TestSingletonService
    {
        private int counter = 0;

        public TestSingletonService()
        {
            Console.WriteLine($"singleton service ctor call executed");
        }

        public void DoWork()
        {
            counter++;
            Console.WriteLine($"singleton service do work executed");
            Console.WriteLine($"counter: {counter}");
        }
    }
}
