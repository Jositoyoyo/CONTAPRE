namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public interface IUserService
    {
        #region Public Methods

        List<User> GetUsers();

        User Login(string login, string password);

        Response InsertUser(User user);

        Response UpdateUser(User user);

        Response UpdatePassword(User user);

        Response UpdateStatus(int userId, bool obsolete);

        Response DeleteUser(int userId);

        User GetUserByCode(int userId);

        User GetUserByEmail(string email);

        User GetUserByUserName(string usu_login);

        #endregion
    }
}