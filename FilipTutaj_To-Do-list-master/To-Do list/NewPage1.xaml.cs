using System.Collections.ObjectModel;

namespace To_Do_list;

public partial class NewPage1 : ContentPage
{
	private ObservableCollection<Done> _D_Tasks;
	public NewPage1(ObservableCollection<Done> D_Tasks)
	{
		InitializeComponent();
		_D_Tasks = D_Tasks;
        wyswietl.ItemsSource = _D_Tasks;
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}