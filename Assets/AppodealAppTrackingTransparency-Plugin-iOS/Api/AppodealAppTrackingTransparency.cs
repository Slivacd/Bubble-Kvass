using System.Diagnostics.CodeAnalysis;
#if UNITY_IOS
using AppodealAppTracking.Platforms.iOS;
#endif
using AppodealAppTracking.Unity.Common;

namespace AppodealAppTracking.Unity.Api
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class AppodealAppTrackingTransparency
    {
        public static void RequestTrackingAuthorization(IAppodealAppTrackingTransparencyListener listener)
        {
#if UNITY_IOS
            AppodealAppTrackingTransparencyClient.Instance.RequestTrackingAuthorization(listener);
#endif
        }
    }
}