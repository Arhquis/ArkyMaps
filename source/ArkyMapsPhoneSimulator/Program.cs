using System;

namespace ArkyMapsPhoneSimulator
{
    /// <summary>
    /// The class holds the entry point of simulator.
    /// </summary>
    class Program
    {
        #region constants
        private const string PRESS_ANY_KEY_MESSAGE = "Press any key to end simulation.";
        private const string ERROR_MESSAGE_FORMAT_STRING = "Error in simulator: {0}";
        #endregion


        #region main
        static void Main(string[] args)
        {
            if (args.Length != 6)
            {
                Console.WriteLine("Not enough parameters.");
            }

            try
            {
                Simulator simulator = new Simulator(args[0], args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), bool.Parse(args[5]));

                simulator.LoadData();
                simulator.Start();

                Console.WriteLine(PRESS_ANY_KEY_MESSAGE);
                Console.ReadKey();

                simulator.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(ERROR_MESSAGE_FORMAT_STRING, ex.Message));
            }
        }
        #endregion
    }
}
