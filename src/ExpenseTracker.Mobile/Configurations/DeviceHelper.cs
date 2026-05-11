namespace ExpenseTracker.Mobile.Configurations;

public static class DeviceHelper
{
    public static bool IsAndroidEmulator()
    {
        return DeviceInfo.Platform == DevicePlatform.Android
               && DeviceInfo.DeviceType == DeviceType.Virtual;
    }
}
