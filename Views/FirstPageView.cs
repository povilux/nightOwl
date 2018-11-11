using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using nightOwl.Exceptions;
using nightOwl.Models;
using nightOwl.Services;

namespace nightOwl.Views
{
    public partial class FirstPageView : Form
    {
        public static FirstPageView self;

        public FirstPageView()
        {
            InitializeComponent();
            self = this;
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

        private async void FirstPageView_Load(object sender, EventArgs e)
        {
            try
            {
                var created = await TestAsync("Petras");

                if (created != null)
                {
                    Console.WriteLine("Vardas " + created.Name + " id: " + created.ID);
                }
            }
            catch(BadHttpRequestException ex)
            {
                Console.WriteLine("Error: " + ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error has occured. " + ex);
                return;
            }
        }

        public async Task<Person> TestAsync(string name)
        {
            try {
                var response = await HttpClientService.Instance.GetAsync<Person>("https://localhost:5001/api/Person/Get/" +  name);
                return response;
            }
            catch(BadHttpRequestException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
