using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;

namespace To_Do_list;

public partial class Dodawanie : ContentPage
{
    private static readonly ObservableCollection<Task> _tasks = new();
    public ObservableCollection<Task> Tasks => _tasks;
    public static ObservableCollection<Done> Done_Tasks { get; } = new();
    private string filePath =
    Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
    public Dodawanie()
    {
        InitializeComponent();

        BindingContext = this;
    }
    private void Add_Clicked(object sender, EventArgs e)
    {
        string nazwa = AddTask.Text;
        string opis = AddDescription.Text;
        string numer = AddDescription1.Text;

        if (string.IsNullOrEmpty(nazwa) ||
            string.IsNullOrEmpty(opis) ||
            string.IsNullOrEmpty(numer))
        {
            return;
        }

        AddTask.Text = "";
        AddDescription.Text = "";
        AddDescription1.Text = "";

        Tasks.Add(new Task
        {
            Name = nazwa,
            Description = opis,
            Number = numer,
            StartTime = DateTime.Now
        });

        wyswietl.ItemsSource = Tasks;
        wyswietl_opis.Text = opis + " " + numer;
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
            D_Desc = task.Description,
            D_Number = task.Number,
            D_Start = task.StartTime,
            D_End = DateTime.Now
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
                "Nie wybrano żadnego zadania.";

            return;
        }

        var task = selected as Task;

        if (task != null)
        {
            wyswietl_opis.Text = $"{task.Description} {task.Number}";
        }
    }

    private async void Scan_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScannerPage(AddTask));
    }

    private async void Powrot_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Archiwum_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Archiwum());
    }
}