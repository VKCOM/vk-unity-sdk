using System.Collections.Generic;

namespace VK.Unity
{
    public enum Scope
    {
        Notify,
        Friends,
        Photos,
        Audio,
        Video,
        Pages,
        Status,
        Notes,
        Messages,
        Wall,
        Ads,
        Offline,
        Docs,
        Groups,
        Notifications,
        Stats,
        Email,
        Market     
    }

    public class ScopeExtensions
    {
        public static List<Scope> Parse(int permissionsValue)
        {
            var res = new List<Scope>();

            if ((permissionsValue & 1) > 0) res.Add(Scope.Notify);
            if ((permissionsValue & 2) > 0) res.Add(Scope.Friends);
            if ((permissionsValue & 4) > 0) res.Add(Scope.Photos);
            if ((permissionsValue & 8) > 0) res.Add(Scope.Audio);
            if ((permissionsValue & 16) > 0) res.Add(Scope.Video);
            if ((permissionsValue & 128) > 0) res.Add(Scope.Pages);
            if ((permissionsValue & 1024) > 0) res.Add(Scope.Status);
            if ((permissionsValue & 2048) > 0) res.Add(Scope.Notes);
            if ((permissionsValue & 4096) > 0) res.Add(Scope.Messages);
            if ((permissionsValue & 8192) > 0) res.Add(Scope.Wall);
            if ((permissionsValue & 32768) > 0) res.Add(Scope.Ads);
            if ((permissionsValue & 65536) > 0) res.Add(Scope.Offline);
            if ((permissionsValue & 131072) > 0) res.Add(Scope.Docs);
            if ((permissionsValue & 262144) > 0) res.Add(Scope.Groups);
            if ((permissionsValue & 524288) > 0) res.Add(Scope.Notifications);
            if ((permissionsValue & 1048576) > 0) res.Add(Scope.Stats);
            if ((permissionsValue & 4194304) > 0) res.Add(Scope.Email);
            if ((permissionsValue & 134217728) > 0) res.Add(Scope.Market);

            return res;
        }
    }
}
