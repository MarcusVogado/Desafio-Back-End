namespace Back_End.Repositories.Contracts
{
    public interface IEmailServices
    {
        Task SendToEmail(string title, string body);
    }
}
