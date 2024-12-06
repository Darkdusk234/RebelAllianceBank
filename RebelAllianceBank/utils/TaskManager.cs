using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebelAllianceBank.utils
{
    // A asynchronous class that will run tasks in the background
    public class TaskManager
    {
        // An instance of CancellationToken
        private readonly CancellationTokenSource _ctk = new CancellationTokenSource();
        // Method to run 
        public async Task TransactionTimer(Func<Task> method, TimeSpan timeSpan)
        {
            // Creates an instance of PeriodicTimer
            // that will keep track of the time interval between executes
            using var timer = new PeriodicTimer(timeSpan);
            // While loop will run as long as as token is true, not terminated.
            while (await timer.WaitForNextTickAsync(_ctk.Token))
            {
                // Will try to run the method that made the call
                try
                {
                    await method();
                }
                // catch if operation was canceled, like _ctk.Cancel()
                catch (OperationCanceledException) { }
            }
        }
        // will go through the queued list of transactions that will be made.
        public async Task TransactionFromQueue()
        {
            
        }
        public async Task ExampleMethod()
        {
            //Console.WriteLine("Detta är ett test.");
        }
        // When method is called, start these method at given time interval
        public async Task Start()
        {
            // create new tasks
            var task1 = TransactionTimer(TransactionFromQueue, TimeSpan.FromMinutes(15));
            var task2 = TransactionTimer(ExampleMethod, TimeSpan.FromSeconds(1));
            // run the tasks simultaneously
            await Task.WhenAll(task1, task2);
        }
        // Method to terminate the Token, closing the thread
        public async Task Stop()
        {
            _ctk.Dispose();
            _ctk.Cancel();
        }
    }
}
