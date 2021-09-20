using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica.Core
{
    /// <summary>
    /// Exception type class to control own exceptions
    /// </summary>
    public class TException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TException()
        {
        }

        /// <summary>
        /// Method to control own exceptions
        /// </summary>
        /// <param name="message"></param>
        public TException(string message) : base(message)
        {
        }
    }
}