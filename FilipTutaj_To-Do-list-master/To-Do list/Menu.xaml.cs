namespace To_Do_list;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private void Start_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Dodawanie");
    }

    private void Archiwum_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Archiwum");
    }

}