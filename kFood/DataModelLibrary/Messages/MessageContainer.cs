namespace DataModelLibrary.Messages
{
    public static class MessageContainer
    {
        #region Actions (controller)
        public const string StartAction = "Src:{Source} -> Start action \"{actionName}\"";
        public const string EndActionSuccess = "Src:{Source} -> End action success: \"{actionName}\"";
        public const string EndActionNotFoundItem = "Src:{Source} -> End action \"{actionName}\". Not found item for ID: \"{id}\"";
        public const string EndActionError = "Src:{Source} -> End action \"{actionName}\". Occured exception";
        #endregion

        #region Outputs
        public const string OutputActionJSON = "OUTPUT: {outputJson}";
        #endregion

        #region Methods
        public const string CalledMethod = "Called: \"{methodName}\"";
        #endregion

        #region Exceptions
        public const string CaughtException = "An exception was caught. Throw higher.";
        #endregion
    }
}
