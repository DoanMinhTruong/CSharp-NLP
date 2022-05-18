
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
namespace vn_text_summarization
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                var endpoint = new Uri("https://minhtruong.tech/bert");
                var newPost = new Post()
                {
                    min_length = 30,
                    body = textBox1.Text,
                };
                var newPostJson = JsonConvert.SerializeObject(newPost);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
                JObject jobject = JObject.Parse(result);
                textBox2.Text = (string)jobject["summarized"];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
            MessageBox.Show("Copied");
        }
    }
    public class Post
    {
        public int min_length { get; set; }
        public string body { get; set; }


    }
}