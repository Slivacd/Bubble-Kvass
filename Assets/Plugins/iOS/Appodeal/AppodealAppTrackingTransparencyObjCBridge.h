
#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void (AppodealAppTrackingTransparencyListenerNotDetermined)(void);
typedef void (AppodealAppTrackingTransparencyListenerRestricted)(void);
typedef void (AppodealAppTrackingTransparencyListenerDenied)(void);
typedef void (AppodealAppTrackingTransparencyListenerAuthorized)(void);

FOUNDATION_EXPORT void RequestConsentInfoUpdate(
                                                AppodealAppTrackingTransparencyListenerNotDetermined appodealAppTrackingTransparencyListenerNotDetermined,
                                                AppodealAppTrackingTransparencyListenerRestricted appodealAppTrackingTransparencyListenerRestricted,
                                                AppodealAppTrackingTransparencyListenerDenied appodealAppTrackingTransparencyListenerDenied,
                                                AppodealAppTrackingTransparencyListenerAuthorized appodealAppTrackingTransparencyListenerAuthorized);

NS_ASSUME_NONNULL_END
