namespace GalleryApp.Helpers
{
    public class DevicePermissions
    {
        public async Task CheckPermissions()
        {
            await CheckMediaPermission();
        }

        private async Task<PermissionStatus> CheckMediaPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Media>();

            if (status == PermissionStatus.Granted) return status;

            status = await Permissions.RequestAsync<Permissions.Media>();

            return status;
        }
    }
}
