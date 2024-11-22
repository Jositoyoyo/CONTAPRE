namespace Dimatica.ContaPre.BLL.Services
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.DAL.DataContexts;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public class ApplicationService : IApplicationService
    {
        #region IApplicationService Members

        public List<Application> GetApplications(string type)
        {
            return ApplicationDataContext.Instance.GetApplications(type);
        }

        public Response UpdateApplication(Application application, int userId)
        {
            return ApplicationDataContext.Instance.UpdateApplication(application, userId);
        }

        public Response UpdateStatus(Application application, int userId)
        {
            return ApplicationDataContext.Instance.UpdateStatus(application, userId);
        }

        public Response DeleteApplication(Application application)
        {
            return ApplicationDataContext.Instance.DeleteApplication(application);
        }

        public List<PRE_CAPITULO> GetChapters(string type)
        {
            return ApplicationDataContext.Instance.GetChapters(type);
        }

        public List<PRE_ARTICULO> GetArticles(string type, int chapterCode)
        {
            return ApplicationDataContext.Instance.GetArticles(type, chapterCode);
        }

        public List<PRE_CONCEPTO> GetConcepts(string type, int articleCode)
        {
            return ApplicationDataContext.Instance.GetConcepts(type, articleCode);
        }

        public List<PRE_SUBCONCEPTO> GetSubconcepts(string type, int conceptCode)
        {
            return ApplicationDataContext.Instance.GetSubconcepts(type, conceptCode);
        }

        public Response InsertChapter(PRE_CAPITULO chapter)
        {
            return ApplicationDataContext.Instance.InsertChapter(chapter);
        }

        public Response InsertArticle(PRE_ARTICULO article)
        {
            return ApplicationDataContext.Instance.InsertArticle(article);
        }

        public Response InsertConcept(PRE_CONCEPTO concept)
        {
            return ApplicationDataContext.Instance.InsertConcept(concept);
        }

        public Response InsertSuboncept(PRE_SUBCONCEPTO subconcept)
        {
            return ApplicationDataContext.Instance.InsertSuboncept(subconcept);
        }

        #endregion
    }
}