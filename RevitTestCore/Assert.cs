using System;
using System.Diagnostics;

namespace RevitTestCore
{
    public class Assert
    {
        public static void Fail(string msg)
        {
            //Debug.Assert
            throw new Exception(msg);
        }

        public static void ThrowsException<T>(Action action) where T:Exception
        {
            try
            {
                action();
            }
            catch(Exception ex)
            {
                if(!ex.GetType().Equals(typeof(T)))
                {
                    throw ex;
                }
                
            }
        }
    }
}
