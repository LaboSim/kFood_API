namespace DataModelLibrary.Messages
{
    public static class MessageContainer
    {
        #region Actions (controller)
        public const string StartAction = "Start action \"{actionName}\"";
        public const string EmptyParam = "Lack of required parameter";

        public const string EndActionSuccess = "End action success: \"{actionName}\"";
        public const string EndActionResourceCreated = "End action success: \"{actionName}\". Resource had been created";
        public const string EndActionResourceNotCreated = "End action success: \"{actionName}\". Resource hadn't been created";
        public const string EndActionNotFoundItem = "End action \"{actionName}\". Not found item for ID: \"{id}\"";
        public const string EndActionError = "End action \"{actionName}\". Occured exception";
        #endregion

        #region Inputs
        public const string InputActionJSON = "INPUT: {inputJson}";
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
