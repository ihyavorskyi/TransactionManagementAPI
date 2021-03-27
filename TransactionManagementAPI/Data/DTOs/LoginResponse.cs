namespace TransactionManagementAPI.Data.DTOs
{
    /// <summary>
    /// Model for response after user login
    /// </summary>
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }
}