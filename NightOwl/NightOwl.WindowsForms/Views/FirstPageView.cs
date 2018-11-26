using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NightOwl.WindowsForms.BusinessLogic;
using NightOwl.WindowsForms.Components;
using NightOwl.WindowsForms.Models;
using NightOwl.WindowsForms.Services;

namespace NightOwl.WindowsForms.Views
{
    public partial class FirstPageView : Form
    {
        public static FirstPageView self;

        public FirstPageView()
        {
            InitializeComponent();
            self = this;
            //TestAsync();
        }

        public async void TestAsync()
        {
            PersonsService service = new PersonsService();

            Person2 newPerson = new Person2(new User(), "Jonaitis", "199-11-21", "2018-11-21", "test");
            Console.WriteLine("person: " + newPerson);
            try
            {
                var response = await service.AddNewPersonAsync(newPerson);

                if (response != null)
                    Console.WriteLine("Done");
                else
                    Console.WriteLine("Error");

            }
            catch(Exception ex)
            {
                Console.WriteLine("error: " + ex);
            }
            /*     var response2 = await service.GetPersonsList();

                  foreach (Person2 person in response2)
                      Console.WriteLine(person.Name);*/
        }

        public static void CloseMainForm()
        {
            Application.Exit();
        }

        private void SelectVideoButton_Click(object sender, EventArgs e)
        {
            VideoRecognitionView videoRecognition = new VideoRecognitionView
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            videoRecognition.Show();
            Hide();
        }

        private void WatchCameraButton_Click(object sender, EventArgs e)
        {
            LiveView webCamForm = new LiveView
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            webCamForm.Show();
            Hide();
        }

        private void ShowMapButton_Click(object sender, EventArgs e)
        {
            MapView lsmForm = new MapView(new PersonModel())
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            lsmForm.Show();
            Hide();
        }

        private void AddPersonButton_Click(object sender, EventArgs e)
        {
            AddPersonView AddPersonForm = new AddPersonView(new PersonModel())
            {
                StartPosition = FormStartPosition.Manual,
                Location = Location
            };
            AddPersonForm.Show();
            Hide();
        }

        private void FirstPageView_Load(object sender, EventArgs e)
        {
           
        }

    }
}
