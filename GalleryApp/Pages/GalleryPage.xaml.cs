using System.Collections.ObjectModel;

namespace GalleryApp.Pages;

public partial class GalleryPage : ContentPage
{
    private bool _itemSelected;

    public bool ItemSelected
    {
        get => _itemSelected;
        set { _itemSelected = value; OnPropertyChanged(); }
    }
    public ObservableCollection<FileInfo> Pictures { get; set; } = new ObservableCollection<FileInfo>();

	public GalleryPage()
	{
        InitializeComponent();

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        ItemSelected = false;
#if ANDROID
        await GalleryApp.Platforms.Android.Utils.PermissionChecker.CheckExternalStoragePermission(DisplayAlert);
#endif
        GetImages();

        base.OnAppearing();
    }

    private void GetImages()
    {
        var pictures = Directory.GetFiles("/storage/emulated/0/Pictures");

        Pictures.Clear();

        foreach (var p in pictures)
        {
            var fileInfo = new FileInfo(p);

            if (!fileInfo.Name.StartsWith(".trashed"))
                Pictures.Add(fileInfo);
        }
    }

    private async void openButton_Clicked(object sender, EventArgs e)
    {
        var image = listView.SelectedItem as FileInfo;

        await Navigation.PushAsync(new ImagePage(image));
    }

    private async void removeButton_Clicked(object sender, EventArgs e)
    {
        var image = listView.SelectedItem as FileInfo;

        if (await DisplayAlert(null, "Точно удалить?", "Да", "Нет"))
        {
            File.Delete(image.FullName);

            Pictures.Remove(image);
        }
    }

    private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        ItemSelected = ((ListView)sender).SelectedItem != null;
    }
}