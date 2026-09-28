using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;

namespace To_Do_list;

public partial class Dodawanie : ContentPage
{
    private static readonly ObservableCollection<Task> _tasks = new();
    public ObservableCollection<Task> Tasks => _tasks;
    public static ObservableCollection<Done> Done_Tasks { get; } = new();
    private static readonly DatabaseService _db = new();
    private static bool _archiwumWczytane = false;
    private string filePath =
    Path.Combine(FileSystem.AppDataDirectory, "tasks.json");
    public Dodawanie()
    {
        InitializeComponent();

        BindingContext = this;

        WczytajArchiwum();
    }

    // Wczytuje zarchiwizowane zadania z pliku bazy danych (tylko raz na start aplikacji).
    private async void WczytajArchiwum()
    {
        if (_archiwumWczytane)
        {
            return;
        }

        _archiwumWczytane = true;

        var zapisane = await _db.GetDoneTasksAsync();

        foreach (var zadanie in zapisane)
        {
            Done_Tasks.Add(zadanie);
        }
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
        wyswietl_opis.Text = opis;
        wyswietl_numer.Text = numer;
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

        var zakonczone = new Done
        {
            D_Name = task.Name,
            D_Desc = task.Description,
            D_Number = task.Number,
            D_Start = task.StartTime,
            D_End = DateTime.Now
        };

        Done_Tasks.Add(zakonczone);

        // Zapisz zarchiwizowane zadanie także do pliku bazy danych.
        _ = _db.AddDoneTaskAsync(zakonczone);

        wyswietl.ItemsSource = Tasks;
        wyswietl_opis.Text = "Description";
        wyswietl_numer.Text = "";
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
            wyswietl_numer.Text = "";

            return;
        }

        var task = selected as Task;

        if (task != null)
        {
            wyswietl_opis.Text = task.Description;
            wyswietl_numer.Text = task.Number;
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