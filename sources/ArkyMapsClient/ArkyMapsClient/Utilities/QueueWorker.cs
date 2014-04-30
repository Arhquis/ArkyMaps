using System;
using System.Collections.Generic;
using System.Threading;

namespace ArkyMapsClient
{
    /// <summary>
    /// Utility class which extends <see cref="Queue{T}"/> class to process queued elements if there is any.
    /// </summary>
    /// <typeparam name="T">Type of elements queued in the collection.</typeparam>
    internal class QueueWorker<T> : Queue<T>
    {
        #region attributes
        private Thread m_workerThread;
        private int m_idleTime;
        private Action<T> m_workMethod;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueWorker{T}"/> class.
        /// </summary>
        /// <param name="idleTime">Idle time in millisec if there is no item in the queue to process.</param>
        /// <param name="workMethod">Delegate which will be called for processing elements.</param>
        internal QueueWorker(int idleTime, Action<T> workMethod)
            :base()
        {
            m_idleTime = idleTime;
            m_workMethod = workMethod;
        }
        #endregion


        #region controllers
        /// <summary>
        /// Starts the queue worker.
        /// </summary>
        internal void Start()
        {
            m_workerThread = new Thread(Work);
            m_workerThread.IsBackground = true;
            m_workerThread.Start();
        }


        /// <summary>
        /// Calls the worker delegate with the dequeued element if any.
        /// </summary>
        private void Work()
        {
            while (true)
            {
                if (this.Count > 0)
                {
                    m_workMethod(this.Dequeue());
                }
                else
                {
                    Thread.Sleep(m_idleTime);
                }
            }
        }


        /// <summary>
        /// Stops the queue worker.
        /// </summary>
        internal void Stop()
        {
            m_workerThread.Abort();
        }
        #endregion
    }
}
