using Android.Content;
using ENV = Android.OS;
using NET = Android.Net;
using Android.Provider;

namespace GalleryApp.Platforms.Android.Utils
{
    public static class PermissionChecker
    {
        public static async Task<bool> CheckExternalStoragePermission(Func<string, string, string, Task> showAlert)
        {
            if (ENV.Build.VERSION.SdkInt >= ENV.BuildVersionCodes.R)
            {
                var res = ENV.Environment.IsExternalStorageManager;

                if (!res)
                {
                    await showAlert("Внимание!", $"Приложению {AppInfo.Current.Name} требуется разрешение на доступ к файлам.{Environment.NewLine}" +
                        $"Сейчас вы будете перенаправлены на экран настроек доступа.", "OK");

                    var manage = Settings.ActionManageAllFilesAccessPermission;
                    var intent = new Intent(manage);

                    Platform.CurrentActivity.StartActivity(intent);
                }

                return res;
            }

            return false;
        }
    }
}
