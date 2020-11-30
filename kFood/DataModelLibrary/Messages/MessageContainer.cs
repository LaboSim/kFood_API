namespace DataModelLibrary.Messages
{
    public static class MessageContainer
    {
        #region Actions
        public const string StartAction = "Start action \"{actionName}\"";
        public const string EndActionSuccess = "End action success: \"{actionName}\"";
        public const string EndActionNotFoundItem = "End action \"{actionName}\". Not found item for ID: \"{id}\"";
        public const string EndActionError = "End action \"{actionName}\". Occured exception"; 
        #endregion
    }
}
