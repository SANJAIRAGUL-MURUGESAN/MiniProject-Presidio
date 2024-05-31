namespace RailwayReservationApp.Exceptions.TrainClassExceptions
{
    public class ClassAlreadyAddedException : Exception
    {
        string msg;
        public ClassAlreadyAddedException()
        {
            msg = "Particular Class is Already added to the Train admin!";
        }
        public override string Message => msg;
    }
}
