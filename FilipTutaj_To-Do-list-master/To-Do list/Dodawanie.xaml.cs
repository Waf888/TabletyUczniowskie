using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;

namespace To_Do_list;

public partial class Dodawanie : ContentPage
{
    ObservableCollection<Task> Tasks { get; set; }
    ObservableCollection<Done> Done_Tasks { get; set; }
    private string filePath =
    Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
    public Dodawanie()
	{
		InitializeComponent();
        Tasks = new ObservableCollection<Task>();
        Done_Tasks = new ObservableCollection<Done>();

        BindingContext = this;
    }
    private void Add_Clicked(object sender, EventArgs e)
    {
        string nazwa = AddTask.Text;
        string opis = AddDescription.Text;

        if (string.IsNullOrEmpty(nazwa) ||
            string.IsNullOrEmpty(opis))
        {
            return;
        }

        AddTask.Text = "";
        AddDescription.Text = "";

        Tasks.Add(new Task
        {
            Name = nazwa,
            Description = opis
        });

        wyswietl.ItemsSource = Tasks;
        wyswietl_opis.Text = opis;
    }
    private void Done_Clicked(object sender, EventArgs e)
    {
        var selected = wyswietl.SelectedItem;

        if (selected == null)
        {
            return;
        }

        var task = (Task)selected;

        Tasks.Remove(task);

        Done_Tasks.Add(new Done
        {
            D_Name = task.Name,
            D_Desc = task.Description
        });

        wyswietl.ItemsSource = Tasks;
        wyswietl_opis.Text = "Description";
    }


    private void wyswietl_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        var selected = wyswietl.SelectedItem;

        if (selected == null)
        {
            wyswietl_opis.Text =
                "Nie wybrano ¿adnego zadania.";

            return;
        }

        var task = selected as Task;

        if (task != null)
        {
            wyswietl_opis.Text = task.Description;
        }
    }

    private async void Scan_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScannerPage(AddTask));
    }

    private void Powrot_Clicked(object sender, EventArgs e)
    {
        
    }
}