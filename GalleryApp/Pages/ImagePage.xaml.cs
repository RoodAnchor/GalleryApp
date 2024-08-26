namespace GalleryApp.Pages;

public partial class ImagePage : ContentPage
{
	public string ImageName { get; set; }
	public string ImagePath { get; set; }
	public string DateCreated { get; set; }

	public ImagePage(FileInfo fileInfo)
	{
		InitializeComponent();

		ImageName = fileInfo.Name;
		ImagePath = fileInfo.FullName;
		DateCreated = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm");

		BindingContext = this;
	}
}