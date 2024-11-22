namespace Dimatica.ContaPre.BLL.Interfaces
{
    #region NameSpaces

    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;
    using Dimatica.ContaPre.OL.Procedures;

    #endregion

    public interface IApplicationService
    {
        #region Public Methods

        List<Application> GetApplications(string type);

        Response UpdateApplication(Application application, int userId);

        Response UpdateStatus(Application application, int userId);

        Response DeleteApplication(Application application);

        List<PRE_CAPITULO> GetChapters(string type);

        List<PRE_ARTICULO> GetArticles(string type, int chapterCode);

        List<PRE_CONCEPTO> GetConcepts(string type, int articleCode);

        List<PRE_SUBCONCEPTO> GetSubconcepts(string type, int conceptCode);

        Response InsertChapter(PRE_CAPITULO chapter);

        Response InsertArticle(PRE_ARTICULO article);

        Response InsertConcept(PRE_CONCEPTO concept);

        Response InsertSuboncept(PRE_SUBCONCEPTO subconcept);

        #endregion
    }
}