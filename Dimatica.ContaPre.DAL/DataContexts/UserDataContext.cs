namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    #endregion

    public class UserDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<UserDataContext> Context = new Lazy<UserDataContext>(() => new UserDataContext());

        #endregion

        #region Public Properties

        public static UserDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<User> GetUsers()
        {
            try
            {
                var result = new List<User>();

                var cmd = Db.GetStoredProcCommand("USP_Users_GetAll");

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var user = new User
                                           {
                                                   USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                                                   USU_NOMBRE = this.DbString(reader["USU_NOMBRE"]),
                                                   USU_APELLIDOS = this.DbString(reader["USU_APELLIDOS"]),
                                                   USU_LOGIN = this.DbString(reader["USU_LOGIN"]),
                                                   USU_EMAIL = this.DbString(reader["USU_EMAIL"]),
                                                   USU_I_G = this.DbString(reader["USU_I_G"]),
                                                   USU_NIVEL = this.DbByte(reader["USU_NIVEL"]),
                                                   Obsolete = this.DbBooleanString(reader["Obsolete"])
                                           };

                        result.Add(user);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Login(string login, string password)
        {
            try
            {
                User result = null;

                var cmd = Db.GetStoredProcCommand("USP_Users_Login");

                Db.AddInParameter(cmd, "@login", DbType.String, login);
                Db.AddInParameter(cmd, "@password", DbType.String, password);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new User
                                         {
                                                 USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                                                 USU_NOMBRE = this.DbString(reader["USU_NOMBRE"]),
                                                 USU_APELLIDOS = this.DbString(reader["USU_APELLIDOS"]),
                                                 USU_LOGIN = this.DbString(reader["USU_LOGIN"]),
                                                 USU_EMAIL = this.DbString(reader["USU_EMAIL"]),
                                                 USU_I_G = this.DbString(reader["USU_I_G"]),
                                                 USU_NIVEL = this.DbByte(reader["USU_NIVEL"])
                        };

                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertUser(User user)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Users_Insert");
                Db.AddInParameter(cmd, "@name", DbType.String, user.USU_NOMBRE);
                Db.AddInParameter(cmd, "@lastName", DbType.String, user.USU_APELLIDOS);
                Db.AddInParameter(cmd, "@login", DbType.String, user.USU_LOGIN);
                Db.AddInParameter(cmd, "@email", DbType.String, user.USU_EMAIL);
                Db.AddInParameter(cmd, "@password", DbType.String, user.USU_PASSWORD);
                Db.AddInParameter(cmd, "@type", DbType.String, user.USU_I_G);
                Db.AddInParameter(cmd, "@level", DbType.Int32, user.USU_NIVEL);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, user.USU_CODIGO_MODIFICACION);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateUser(User user)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Users_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, user.USU_CODIGO);
                Db.AddInParameter(cmd, "@name", DbType.String, user.USU_NOMBRE);
                Db.AddInParameter(cmd, "@lastName", DbType.String, user.USU_APELLIDOS);
                Db.AddInParameter(cmd, "@login", DbType.String, user.USU_LOGIN);
                Db.AddInParameter(cmd, "@email", DbType.String, user.USU_EMAIL);
                Db.AddInParameter(cmd, "@type", DbType.String, user.USU_I_G);
                Db.AddInParameter(cmd, "@level", DbType.Int32, user.USU_NIVEL);
                Db.AddInParameter(cmd, "@userId", DbType.Int32, user.USU_CODIGO_MODIFICACION);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.Found;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdatePassword(User user)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Users_UpdatePassword");

                Db.AddInParameter(cmd, "@id", DbType.Int32, user.USU_CODIGO);
                Db.AddInParameter(cmd, "@password", DbType.String, user.USU_PASSWORD);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.NotFound;

                                break;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response UpdateStatus(int userId, bool obsolete)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Users_UpdateStatus");

                Db.AddInParameter(cmd, "@id", DbType.Int32, userId);
                Db.AddInParameter(cmd, "@obsolete", DbType.Boolean, obsolete);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.NotFound;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                        }
                    }
                }

                return new Response
                               {
                                       ResponseCode = code
                               };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserByCode(int userId) 
        {
            try
            {
                User result = null;

                var cmd = Db.GetStoredProcCommand("USP_Users_GetByCode");

                Db.AddInParameter(cmd, "@user_id", DbType.Int32, userId);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new User
                        {
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            USU_NOMBRE = this.DbString(reader["USU_NOMBRE"]),
                            USU_APELLIDOS = this.DbString(reader["USU_APELLIDOS"]),
                            USU_LOGIN = this.DbString(reader["USU_LOGIN"]),
                            USU_EMAIL = this.DbString(reader["USU_EMAIL"]),
                            USU_I_G = this.DbString(reader["USU_I_G"]),
                            USU_NIVEL = this.DbByte(reader["USU_NIVEL"]),
                            USU_PASSWORD = this.DbString(reader["USU_PASSWORD"]),
                            Obsolete = this.DbBooleanString(reader["Obsolete"])
                        };

                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                User result = null;

                var cmd = Db.GetStoredProcCommand("USP_Users_GetByEmail");

                Db.AddInParameter(cmd, "@email", DbType.String, email);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new User
                        {
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            USU_NOMBRE = this.DbString(reader["USU_NOMBRE"]),
                            USU_APELLIDOS = this.DbString(reader["USU_APELLIDOS"]),
                            USU_LOGIN = this.DbString(reader["USU_LOGIN"]),
                            USU_EMAIL = this.DbString(reader["USU_EMAIL"]),
                            USU_I_G = this.DbString(reader["USU_I_G"]),
                            USU_NIVEL = this.DbByte(reader["USU_NIVEL"]),
                            Obsolete = this.DbBooleanString(reader["Obsolete"])
                        };

                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserByUserName(string usu_login)
        {
            try
            {
                User result = null;

                var cmd = Db.GetStoredProcCommand("USP_Users_GetByUserName");

                Db.AddInParameter(cmd, "@usu_login", DbType.String, usu_login);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        result = new User
                        {
                            USU_CODIGO = this.DbInteger(reader["USU_CODIGO"]),
                            USU_NOMBRE = this.DbString(reader["USU_NOMBRE"]),
                            USU_APELLIDOS = this.DbString(reader["USU_APELLIDOS"]),
                            USU_LOGIN = this.DbString(reader["USU_LOGIN"]),
                            USU_PASSWORD = this.DbString(reader["USU_PASSWORD"]),
                            USU_EMAIL = this.DbString(reader["USU_EMAIL"]),
                            USU_I_G = this.DbString(reader["USU_I_G"]),
                            USU_NIVEL = this.DbByte(reader["USU_NIVEL"]),
                            Obsolete = this.DbBooleanString(reader["Obsolete"])
                        };

                        break;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // la tabla de usuarios tiene relacion con otras tablas. Entonces no se puede eliminar un usuario si tiene registros relacionados
        public Response DeleteUser(int userId)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Users_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, userId);

                var code = ResponseCode.Invalid;

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var result = this.DbInteger(reader["Result"]);

                        switch (result)
                        {
                            case 0:
                                code = ResponseCode.NotFound;

                                break;
                            case 1:
                                code = ResponseCode.Ok;

                                break;
                            case 2:
                                code = ResponseCode.Invalid;

                                break;
                        }
                    }
                }

                return new Response
                {
                    ResponseCode = code
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}