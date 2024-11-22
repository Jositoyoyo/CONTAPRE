namespace Dimatica.ContaPre.BLL.Configs
{
    #region NameSpaces

    using LightInject;

    #endregion

    public class DependencyFactory
    {
        #region Public Static Methods

        public static T GetInstance<T>()
        {
            return new ServiceContainer().GetInstance<T>();
        }

        #endregion
    }
}