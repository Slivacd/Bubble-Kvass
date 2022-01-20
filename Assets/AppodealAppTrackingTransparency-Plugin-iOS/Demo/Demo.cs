using AppodealAppTracking.Unity.Api;
using AppodealAppTracking.Unity.Common;
using UnityEngine;

namespace AppodealAppTracking.Unity.Demo
{
    public class Demo : MonoBehaviour, IAppodealAppTrackingTransparencyListener
    {
        private void Awake()
        {
            AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);
        }

        public void AppodealAppTrackingTransparencyListenerNotDetermined()
        {
            Debug.Log("AppodealAppTrackingTransparencyListenerNotDetermined");
        }

        public void AppodealAppTrackingTransparencyListenerRestricted()
        {
            Debug.Log("AppodealAppTrackingTransparencyListenerRestricted");
        }

        public void AppodealAppTrackingTransparencyListenerDenied()
        {
            Debug.Log("AppodealAppTrackingTransparencyListenerDenied");
        }

        public void AppodealAppTrackingTransparencyListenerAuthorized()
        {
            Debug.Log("AppodealAppTrackingTransparencyListenerAuthorized");
        }
    }
}