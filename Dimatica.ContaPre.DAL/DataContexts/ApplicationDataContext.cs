namespace Dimatica.ContaPre.DAL.DataContexts
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Data;

    using Dimatica.ContaPre.DAL.Models;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class ApplicationDataContext : ContextModel
    {
        #region Static Fields and Constants

        protected static readonly Lazy<ApplicationDataContext> Context = new Lazy<ApplicationDataContext>(() => new ApplicationDataContext());

        #endregion

        #region Public Properties

        public static ApplicationDataContext Instance
        {
            get
            {
                return Context.Value;
            }
        }

        #endregion

        #region Public Methods

        public List<Application> GetApplications(string type)
        {
            try
            {
                var result = new List<Application>();

                var cmd = Db.GetStoredProcCommand("USP_Applications_GetByType");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new Application
                                                  {
                                                          ApplicationId = this.DbInteger(reader["ID"]),
                                                          Level = this.DbString(reader["NIVEL"]),
                                                          Chapter = this.DbString(reader["CAP_NUMERO"]),
                                                          Article = this.DbString(reader["ART_NUMERO"]),
                                                          Concept = this.DbString(reader["CON_NUMERO"]),
                                                          SubConcept = this.DbString(reader["SUB_NUMERO"]),
                                                          Description = this.DbString(reader["DESCRIPCION"]),
                                                          AccountId = this.DbIntegerNullable(reader["CUEP_CODIGO"]),
                                                          AccountNumber = this.DbString(reader["CUEP_NUMERO"]),
                                                          Active = this.DbBooleanBit(reader["CAP_ACTIVO_PRE"])
                                                  };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response UpdateApplication(Application application, int userId)
        {
            try
            {
                if (application?.ApplicationId == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Applications_Update");

                Db.AddInParameter(cmd, "@id", DbType.Int32, application.ApplicationId);
                Db.AddInParameter(cmd, "@level", DbType.String, application.Level);
                Db.AddInParameter(cmd, "@description", DbType.String, application.Description);
                Db.AddInParameter(cmd, "@accountPgcp", DbType.Int32, application.AccountId);
                Db.AddInParameter(cmd, "@updateBy", DbType.Int32, userId);

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

        public Response UpdateStatus(Application application, int userId)
        {
            try
            {
                if (application?.ApplicationId == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Applications_UpdateStatus");

                Db.AddInParameter(cmd, "@id", DbType.Int32, application.ApplicationId);
                Db.AddInParameter(cmd, "@active", DbType.Boolean, application.Active);
                Db.AddInParameter(cmd, "@activeBy", DbType.Int32, userId);

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

        public Response DeleteApplication(Application application)
        {
            try
            {
                if (application?.ApplicationId == null)
                {
                    return new Response
                                   {
                                           ResponseCode = ResponseCode.Invalid
                                   };
                }

                var cmd = Db.GetStoredProcCommand("USP_Applications_Delete");

                Db.AddInParameter(cmd, "@id", DbType.Int32, application.ApplicationId);
                Db.AddInParameter(cmd, "@level", DbType.String, application.Level);

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
                            case 3:
                                code = ResponseCode.Found;

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

        public List<PRE_CAPITULO> GetChapters(string type)
        {
            try
            {
                var result = new List<PRE_CAPITULO>();

                var cmd = Db.GetStoredProcCommand("USP_Applications_GetChapters");

                Db.AddInParameter(cmd, "@type", DbType.String, type);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_CAPITULO
                                                  {
                                                          CAP_CODIGO = this.DbByte(reader["CAP_CODIGO"]),
                                                          CAP_CODIGO_AUX = this.DbByte(reader["CAP_CODIGO"]),
                                                          CAP_NUMERO = this.DbByte(reader["CAP_NUMERO"]),
                                                          CAP_DESCRIPCION = this.DbString(reader["CAP_DESCRIPCION"]),
                                                          PRO_CODIGO = this.DbByteNullable(reader["PRO_CODIGO"]),
                                                          CUEP_CODIGO = this.DbIntegerNullable(reader["CUEP_CODIGO"]),
                                                          CAP_ACTIVO_PRE = this.DbBooleanBit(reader["CAP_ACTIVO_PRE"]),
                                                          CAPITULO_LABEL = this.DbString(reader["CAPITULO"])
                                                  };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_ARTICULO> GetArticles(string type, int chapterCode)
        {
            try
            {
                var result = new List<PRE_ARTICULO>();

                var cmd = Db.GetStoredProcCommand("USP_Applications_GetArticles");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@chapterCode", DbType.Int32, chapterCode);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_ARTICULO
                                                  {
                                                          ART_CODIGO = this.DbByte(reader["ART_CODIGO"]),
                                                          ART_CODIGO_AUX = this.DbInteger(reader["ART_CODIGO"]),
                                                          ART_NUMERO = this.DbByte(reader["ART_NUMERO"]),
                                                          ART_DESCRIPCION = this.DbString(reader["ART_DESCRIPCION"]),
                                                          ARTICULO_LABEL = this.DbString(reader["ARTICULO"])
                                                  };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_CONCEPTO> GetConcepts(string type, int articleCode)
        {
            try
            {
                var result = new List<PRE_CONCEPTO>();

                var cmd = Db.GetStoredProcCommand("USP_Applications_GetConcepts");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@articleCode", DbType.Int32, articleCode);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_CONCEPTO
                                                  {
                                                          CON_CODIGO = this.DbInteger(reader["CON_CODIGO"]),
                                                          CON_CODIGO_AUX = this.DbInteger(reader["CON_CODIGO"]),
                                                          CON_NUMERO = this.DbByte(reader["CON_NUMERO"]),
                                                          CON_DESCRIPCION = this.DbString(reader["CON_DESCRIPCION"]),
                                                          CONCEPTO_LABEL = this.DbString(reader["CONCEPTO"])
                                                  };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PRE_SUBCONCEPTO> GetSubconcepts(string type, int conceptCode)
        {
            try
            {
                var result = new List<PRE_SUBCONCEPTO>();

                var cmd = Db.GetStoredProcCommand("USP_Applications_GetSubconcepts");

                Db.AddInParameter(cmd, "@type", DbType.String, type);
                Db.AddInParameter(cmd, "@conceptCode", DbType.Int32, conceptCode);

                using (var reader = Db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        var application = new PRE_SUBCONCEPTO
                                                  {
                                                          SUB_CODIGO = this.DbByte(reader["SUB_CODIGO"]),
                                                          SUB_CODIGO_AUX = this.DbInteger(reader["SUB_CODIGO"]),
                                                          SUB_NUMERO = this.DbString(reader["SUB_NUMERO"]),
                                                          SUB_DESCRIPCION = this.DbString(reader["SUB_DESCRIPCION"]),
                                                          SUBCONCEPTO_LABEL = this.DbString(reader["SUBCONCEPTO"])
                                                  };

                        result.Add(application);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response InsertChapter(PRE_CAPITULO chapter)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Applications_InsertChapter");

                Db.AddInParameter(cmd, "@type", DbType.String, chapter.CAP_I_G);
                Db.AddInParameter(cmd, "@number", DbType.Int32, chapter.CAP_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, chapter.CAP_DESCRIPCION);
                Db.AddInParameter(cmd, "@accountPgcp", DbType.Int32, chapter.CUEP_CODIGO);
                Db.AddInParameter(cmd, "@userID", DbType.Int32, chapter.USU_CODIGO);

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
                                code = ResponseCode.Found;

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

        public Response InsertArticle(PRE_ARTICULO article)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Applications_InsertArticle");

                Db.AddInParameter(cmd, "@type", DbType.String, article.ART_I_G);
                Db.AddInParameter(cmd, "@number", DbType.Int32, article.ART_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, article.ART_DESCRIPCION);
                Db.AddInParameter(cmd, "@chapterCode", DbType.Int32, article.CAP_CODIGO);
                Db.AddInParameter(cmd, "@accountPgcp", DbType.Int32, article.CUEP_CODIGO);
                Db.AddInParameter(cmd, "@userID", DbType.Int32, article.USU_CODIGO);

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
                                code = ResponseCode.Found;

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

        public Response InsertConcept(PRE_CONCEPTO concept)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Applications_InsertConcept");

                Db.AddInParameter(cmd, "@type", DbType.String, concept.CON_I_G);
                Db.AddInParameter(cmd, "@number", DbType.Int32, concept.CON_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, concept.CON_DESCRIPCION);
                Db.AddInParameter(cmd, "@articleCode", DbType.Int32, concept.ART_CODIGO);
                Db.AddInParameter(cmd, "@accountPgcp", DbType.Int32, concept.CUEP_CODIGO);
                Db.AddInParameter(cmd, "@userID", DbType.Int32, concept.USU_CODIGO);

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
                                code = ResponseCode.Found;

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

        public Response InsertSuboncept(PRE_SUBCONCEPTO subconcept)
        {
            try
            {
                var cmd = Db.GetStoredProcCommand("USP_Applications_InsertSubconcept");

                Db.AddInParameter(cmd, "@type", DbType.String, subconcept.SUB_I_G);
                Db.AddInParameter(cmd, "@number", DbType.Int32, subconcept.SUB_NUMERO);
                Db.AddInParameter(cmd, "@description", DbType.String, subconcept.SUB_DESCRIPCION);
                Db.AddInParameter(cmd, "@conceptCode", DbType.Int32, subconcept.CON_CODIGO);
                Db.AddInParameter(cmd, "@accountPgcp", DbType.Int32, subconcept.CUEP_CODIGO);
                Db.AddInParameter(cmd, "@userID", DbType.Int32, subconcept.USU_CODIGO);

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
                                code = ResponseCode.Found;

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