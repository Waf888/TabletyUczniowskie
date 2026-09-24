namespace To_Do_list;

public partial class Archiwum : ContentPage
{
    public Archiwum()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public System.Collections.ObjectModel.ObservableCollection<Done> Done_Tasks
        => Dodawanie.Done_Tasks;

    private async void Powrot_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}