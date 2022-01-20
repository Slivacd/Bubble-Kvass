#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import "AppodealAppTrackingTransparencyObjCBridge.h"


void RequestTrackingAuthorization(
                                  AppodealAppTrackingTransparencyListenerNotDetermined
                                  appodealAppTrackingTransparencyListenerNotDetermined,
                                  AppodealAppTrackingTransparencyListenerRestricted
                                  appodealAppTrackingTransparencyListenerRestricted,
                                  AppodealAppTrackingTransparencyListenerDenied
                                  appodealAppTrackingTransparencyListenerDenied,
                                  AppodealAppTrackingTransparencyListenerAuthorized
                                  appodealAppTrackingTransparencyListenerAuthorized)
{
    if (@available(iOS 14, *)) {
#ifdef __IPHONE_14_0
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
            switch (status)
                {
                 case ATTrackingManagerAuthorizationStatusNotDetermined:
                        appodealAppTrackingTransparencyListenerNotDetermined();
                      break;
                 case ATTrackingManagerAuthorizationStatusRestricted:
                        appodealAppTrackingTransparencyListenerRestricted();
                      break;
                 case ATTrackingManagerAuthorizationStatusDenied:
                        appodealAppTrackingTransparencyListenerDenied();
                      break;
                 case ATTrackingManagerAuthorizationStatusAuthorized:
                        appodealAppTrackingTransparencyListenerAuthorized();
                     break;
             }
        }];
#endif
    }else {
        appodealAppTrackingTransparencyListenerNotDetermined();
    }
}









