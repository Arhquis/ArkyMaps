using System;

namespace ArkyMapService
{
    class Program
    {
        /// <summary>
        /// Initializes the service controller and start it.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ServiceController controller = ServiceController.Instance;

            bool servicesStarted = false;

            try
            {
                servicesStarted = controller.StartService();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(Messages.ERROR_SERVICE_START, ex.Message));
            }

            if (servicesStarted)
            {
                Console.WriteLine(Messages.MESSAGE_SERVICE_STARTED);
                Console.WriteLine(Messages.MESSAGE_PRESS_ANY_KEY);

                Console.ReadKey();

                controller.StopService();
            }
        }
    }
}
