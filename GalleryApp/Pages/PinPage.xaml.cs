using System.Text.RegularExpressions;

namespace GalleryApp.Pages;

public partial class PinPage : ContentPage
{
	private Entry[] _pinEntries;
	private string _pinCode;
	private string _storedPinCode;
	private bool _isPinValid;

	public bool IsPinValid 
	{
		get => _isPinValid;
		set { _isPinValid = value; OnPropertyChanged(); }
	}

	public PinPage()
	{
		InitializeComponent();
		GetStoredCode();

		_pinEntries = [pinOne, pinTwo, pinThree, pinFour];
    }

    private void GetStoredCode()
	{
		_storedPinCode = Preferences.Get("PIN", null);

		if (string.IsNullOrEmpty(_storedPinCode))
            pinLabel.Text = "Придумайте четырёхзначный код";
    }

    private void pinEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var entry  = sender as Entry;

		if (!string.IsNullOrEmpty(entry.Text))
            MoveFocus(entry, 1);

        _pinCode = string.Join("", _pinEntries.Select(x => x.Text));

        ValidatePinCode();
    }

	private void MoveFocus(Entry entry, int direction)
	{
        var nextIndex = Array.IndexOf(_pinEntries, entry) + direction;

        if (nextIndex >= 0 && nextIndex < _pinEntries.Length)
            _pinEntries[nextIndex].Focus();
    }

	private void ValidatePinCode()
	{
		var rgx = new Regex("\\d{4}");

        IsPinValid = rgx.IsMatch(_pinCode);
	}

	private bool GetPinCodeIsCorrect()
	{
		return _storedPinCode == _pinCode;
    }

    private async void OkButton_Clicked(object sender, EventArgs e)
    {
		if (string.IsNullOrEmpty(_storedPinCode))
		{
            Preferences.Set("PIN", _pinCode);
        }
		else if (!GetPinCodeIsCorrect())
		{
			await DisplayAlert("Ошибка", "Введён неверный код", "OK");

			ResetState();

			return;
        }

        await Navigation.PushAsync(new GalleryPage());

		return;
    }

	private void ResetState()
	{
		foreach (var entry in _pinEntries)
			entry.Text = null;

		_pinCode = null;

		pinOne.Focus();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		pinOne.Focus();
    }
}