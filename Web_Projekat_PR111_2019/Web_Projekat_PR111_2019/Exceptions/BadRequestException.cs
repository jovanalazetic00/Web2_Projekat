namespace Web_Projekat_PR111_2019.Exceptions
{
    public class BadRequestException:Exception
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string poruka)
            : base(poruka)
        {
        }
    }
}
