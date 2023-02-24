namespace Qliro.MontyHall.Engine.Exceptions
{
    public class DoorOpenedBeforeException : Exception
    {
        public DoorOpenedBeforeException(int index) :
            base($"Can't select door number {index} . It has been selected before.")
        {

        }
    }
}
