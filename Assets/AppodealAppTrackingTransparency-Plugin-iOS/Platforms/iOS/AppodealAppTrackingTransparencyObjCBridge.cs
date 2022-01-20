using System.Runtime.InteropServices;

#if UNITY_IOS

namespace AppodealAppTracking.Platforms.iOS
{
    public static class AppodealAppTrackingTransparencyObjCBridge
    {
        internal delegate void AppodealAppTrackingTransparencyListenerNotDetermined();
        internal delegate void AppodealAppTrackingTransparencyListenerRestricted();
        internal delegate void AppodealAppTrackingTransparencyListenerDenied();
        internal delegate void AppodealAppTrackingTransparencyListenerAuthorized();
        
        [DllImport("__Internal")]
        internal static extern void RequestTrackingAuthorization(
            AppodealAppTrackingTransparencyListenerNotDetermined appodealAppTrackingTransparencyListenerNotDetermined,
            AppodealAppTrackingTransparencyListenerRestricted appodealAppTrackingTransparencyListenerRestricted,
            AppodealAppTrackingTransparencyListenerDenied appodealAppTrackingTransparencyListenerDenied, 
            AppodealAppTrackingTransparencyListenerAuthorized appodealAppTrackingTransparencyListenerAuthorized);
    }
}
#endif