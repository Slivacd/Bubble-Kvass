#if UNITY_IOS
using AOT;
using AppodealAppTracking.Unity.Common;
#pragma warning disable 649


namespace AppodealAppTracking.Platforms.iOS
{
    public class AppodealAppTrackingTransparencyClient
    {
        
        #region Singleton

        private AppodealAppTrackingTransparencyClient()
        {
        }

        public static AppodealAppTrackingTransparencyClient Instance { get; } = new AppodealAppTrackingTransparencyClient();

        #endregion
        
        private static IAppodealAppTrackingTransparencyListener _appTrackingTransparencyListener;
        
        [MonoPInvokeCallback(typeof(AppodealAppTrackingTransparencyObjCBridge.AppodealAppTrackingTransparencyListenerNotDetermined))]
        private static void AppodealAppTrackingTransparencyListenerNotDetermined()
        {
            _appTrackingTransparencyListener?.AppodealAppTrackingTransparencyListenerNotDetermined();
        }
        
        [MonoPInvokeCallback(typeof(AppodealAppTrackingTransparencyObjCBridge.AppodealAppTrackingTransparencyListenerRestricted))]
        private static void AppodealAppTrackingTransparencyListenerRestricted()
        {
            _appTrackingTransparencyListener?.AppodealAppTrackingTransparencyListenerRestricted();
        }
        
        [MonoPInvokeCallback(typeof(AppodealAppTrackingTransparencyObjCBridge.AppodealAppTrackingTransparencyListenerDenied))]
        private static void AppodealAppTrackingTransparencyListenerDenied()
        {
            _appTrackingTransparencyListener?.AppodealAppTrackingTransparencyListenerDenied();
        }
        
        [MonoPInvokeCallback(typeof(AppodealAppTrackingTransparencyObjCBridge.AppodealAppTrackingTransparencyListenerAuthorized))]
        private static void AppodealAppTrackingTransparencyListenerAuthorized()
        {
            _appTrackingTransparencyListener?.AppodealAppTrackingTransparencyListenerAuthorized();
        }
        
        public void RequestTrackingAuthorization(IAppodealAppTrackingTransparencyListener listener)
        {
            _appTrackingTransparencyListener = listener;
            AppodealAppTrackingTransparencyObjCBridge.RequestTrackingAuthorization(AppodealAppTrackingTransparencyListenerNotDetermined, AppodealAppTrackingTransparencyListenerRestricted,
                AppodealAppTrackingTransparencyListenerDenied, AppodealAppTrackingTransparencyListenerAuthorized);
        }
    }
}
#endif