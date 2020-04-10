using com.simplemaple.shared.Models;

namespace com.simplemaple.service.contracts
{
    public interface IUserService
    {



        bool ValidateUser(string wxlogincode);
    }
}
