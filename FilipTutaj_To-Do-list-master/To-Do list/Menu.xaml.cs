namespace To_Do_list;

public partial class Menu : ContentPage
{
    public Menu()
    {
        InitializeComponent();
    }

    private async void Start_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Dodawanie());
    }

    private async void Archiwum_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Archiwum());
    }

}