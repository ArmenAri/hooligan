namespace Hooligan.Web.Client.Models;

public partial class HooliganResponse<T>
{
    public static readonly HooliganResponse<T> UnknownException = new()
    {
        Error = new HooliganException
        {
            StatusCode = 500,
            Message = "Unknown error occured"
        }
    };
}