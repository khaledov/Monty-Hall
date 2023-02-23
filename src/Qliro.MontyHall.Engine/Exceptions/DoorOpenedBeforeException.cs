using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qliro.MontyHall.Engine.Exceptions
{
    public class DoorOpenedBeforeException:Exception
    {
        public DoorOpenedBeforeException(int index) :
            base($"Can't select door number {index} . It has been selected before.")
        {

        }
    }
}
