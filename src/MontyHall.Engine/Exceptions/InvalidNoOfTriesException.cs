using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHall.Engine.Exceptions
{
    public class InvalidNoOfTriesException :Exception
    {
        public InvalidNoOfTriesException(long tries) :
            base($"Games count  {tries} is invalid. It should be greater than zero. ")
        {

        }
    }
}
