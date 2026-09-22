using System.Collections.ObjectModel;
using System.Text.Json;
using ZXing.Net.Maui;

namespace To_Do_list
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Task> Tasks { get; set; }
        ObservableCollection<Done> Done_Tasks { get; set; }

        private string filePath =
            Path.Combine(FileSystem.AppDataDirectory, "tasks.json");

        public MainPage()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<Task>();
            Done_Tasks = new ObservableCollection<Done>();

            BindingContext = this;
        }



        


    }
}
