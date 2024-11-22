namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class UserService : IUserService
    {
        #region IUserService Members

        public List<User> GetUsers()
        {
            return UserDataContext.Instance.GetUsers();
        }

        public User Login(string login, string password)
        {
            return UserDataContext.Instance.Login(login, password);
        }

        public Response InsertUser(User user)
        {
            return UserDataContext.Instance.InsertUser(user);
        }

        public Response UpdateUser(User user)
        {
            return UserDataContext.Instance.UpdateUser(user);
        }

        public Response UpdatePassword(User user)
        {
            return UserDataContext.Instance.UpdatePassword(user);
        }

        public Response UpdateStatus(int userId, bool obsolete)
        {
            return UserDataContext.Instance.UpdateStatus(userId, obsolete);
        }

        public User GetUserByCode(int userId)
        {
            return UserDataContext.Instance.GetUserByCode(userId);
        }

        public User GetUserByEmail(string email)
        {
            return UserDataContext.Instance.GetUserByEmail(email);
        }

        public User GetUserByUserName(string usu_login)
        {
            return UserDataContext.Instance.GetUserByUserName(usu_login);
        }

        public Response DeleteUser(int userId)
        {
            return UserDataContext.Instance.DeleteUser(userId);
        }

        #endregion
    }
}