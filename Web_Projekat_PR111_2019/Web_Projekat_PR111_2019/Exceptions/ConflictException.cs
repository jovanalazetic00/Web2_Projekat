namespace Web_Projekat_PR111_2019.Exceptions
{
    public class ConflictException:Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string poruka)
            : base(poruka)
        {
        }
    }
}
