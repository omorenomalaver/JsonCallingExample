using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonCallingExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://api.sexgap.sexualwellbeingfoundation.org/api/help";
            Console.WriteLine();
            string jsonValue = GET(url);
            Rootobject myGeoNames = JsonConvert.DeserializeObject<Rootobject>(jsonValue);
            List<Help> myHelpList = myGeoNames.value.help.ToList<Help>();
            Console.WriteLine("endl");

        }

        public class Rootobject
        {
            public Value value { get; set; }
            public object[] formatters { get; set; }
            public object[] contentTypes { get; set; }
            public int statusCode { get; set; }
        }

        public class Value
        {
            public Help[] help { get; set; }
        }

        public class Help
        {
            public int id { get; set; }
            public string name { get; set; }
            public string info { get; set; }
        }

        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }
    }
}
