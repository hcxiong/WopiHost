using System.IO;

namespace Netnr.WopiHandler
{
    enum RequestType
    {
        None,

        CheckFileInfo,
        PutRelativeFile,

        Lock,
        Unlock,
        RefreshLock,
        UnlockAndRelock,

        ExecuteCobaltRequest,

        DeleteFile,
        ReadSecureStore,
        GetRestrictedLink,
        RevokeRestrictedLink,

        CheckFolderInfo,

        GetFile,
        PutFile,

        EnumerateChildren,
    }

    static class WopiHeaders
    {
        public const string RequestType = "X-WOPI-Override";
        public const string ItemVersion = "X-WOPI-ItemVersion";

        public const string Lock = "X-WOPI-Lock";
        public const string OldLock = "X-WOPI-OldLock";
        public const string LockFailureReason = "X-WOPI-LockFailureReason";
        public const string LockedByOtherInterface = "X-WOPI-LockedByOtherInterface";

        public const string SuggestedTarget = "X-WOPI-SuggestedTarget";
        public const string RelativeTarget = "X-WOPI-RelativeTarget";
        public const string OverwriteRelativeTarget = "X-WOPI-OverwriteRelativeTarget";
    }

    class WopiRequest
    {
        public RequestType Type { get; set; }

        public string AccessToken { get; set; }

        public string Id { get; set; }

        public string FullPath
        {
            get { return System.Web.HttpUtility.UrlDecode(Path.Combine(WopiConfig.LocalStoragePath, Id)); }
        }

        private string _UserId;
        public string UserId
        {
            get
            {
                return System.Web.HttpUtility.UrlDecode(_UserId);
            }
            set
            {
                _UserId = value;
            }
        }

        private string _UserName;
        public string UserName
        {
            get
            {
                return System.Web.HttpUtility.UrlDecode(_UserName);
            }
            set
            {
                _UserName = value;
            }
        }
    }
}
