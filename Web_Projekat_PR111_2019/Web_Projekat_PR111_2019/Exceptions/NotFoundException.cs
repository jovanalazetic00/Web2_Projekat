namespace Web_Projekat_PR111_2019.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string poruka)
            : base(poruka)
        {
        }
    }
}
