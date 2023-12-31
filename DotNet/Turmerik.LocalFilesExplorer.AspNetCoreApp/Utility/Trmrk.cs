namespace Turmerik.LocalFilesExplorer.AspNetCoreApp.Utility
{
    public static class Trmrk
    {
        public static readonly string TrmrkPfx = nameof(Trmrk);
        public static readonly string TrmrkGuidStr;
        public static readonly string TrmrkGuidStrNoDash;
        public static readonly Guid TrmrkGuid;

        static Trmrk()
        {
            TrmrkGuidStr = "f1131f3d-a28f-444e-b816-82a2fd94b1a6";
            TrmrkGuidStrNoDash = TrmrkGuidStr.Split('-').JoinNotNullStr(null, false);
            TrmrkGuid = Guid.Parse(TrmrkGuidStr);
        }

        public static TTrmrkObj Obj<TTrmrkObj>(
            Action<TTrmrkObj> buildAction = null) where TTrmrkObj : TrmrkObj
        {
            var obj = Activator.CreateInstance<TTrmrkObj>();
            obj.TrmrkGuid = TrmrkGuid;

            buildAction?.Invoke(obj);
            return obj;
        }
    }
}
