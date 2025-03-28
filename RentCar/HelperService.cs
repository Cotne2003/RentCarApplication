namespace RentCar
{
    public static class HelperService
    {
        public static string GetFullMessage(this Exception ex)
        {
            if (ex == null)
                return string.Empty;

            var message = new List<string>();

            while(ex != null)
            {
                message.Add(ex.Message);
                ex = ex.InnerException;
            }

            return string.Join("=>", message);

        }
    }
}
