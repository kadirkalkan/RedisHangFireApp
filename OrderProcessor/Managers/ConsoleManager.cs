using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderProcessor.Managers
{
    class ConsoleManager
    {

        private static ConsoleManager _consoleManager;
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        private ConsoleManager() {}

        public static ConsoleManager GetInstance()
        {
            if (_consoleManager == null)
                _consoleManager = new ConsoleManager();
            return _consoleManager;
        }

        public void StopClosing()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            autoEvent.WaitOne();
        }

        public void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Consumer closed.");
            autoEvent.Set();
            Environment.Exit(0);
        }

    }
}
