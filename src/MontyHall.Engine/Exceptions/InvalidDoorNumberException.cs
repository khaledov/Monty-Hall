namespace MontyHall.Engine.Exceptions
{
    public class InvalidDoorNumberException : Exception
    {
        public InvalidDoorNumberException(int index) :
            base($"Door number {index} is invalid. It should be 0,1,2 ")
        {

        }
    }
}
