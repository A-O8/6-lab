using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wf_Mongo_1
{
    public partial class Form1 : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("dirictions");
        static IMongoCollection<diriction> collection = db.GetCollection<diriction>("diriction");


        public void ReadAllDocuments()
        {

            List<diriction> list = collection.AsQueryable().ToList<diriction>();
            dataGridView1.DataSource=list;
            textBox1.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
        }
        public Form1()
        {
            InitializeComponent();
            ReadAllDocuments();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            diriction s = new diriction(textBox2.Text, Double.Parse(textBox3.Text));
            collection.InsertOne(s);
            ReadAllDocuments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var updateDef = Builders<diriction>.Update.Set("name", textBox2.Text).Set("age", textBox3.Text);
            collection.UpdateOne(s => s.Id == ObjectId.Parse(textBox1.Text),updateDef);
            ReadAllDocuments();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            collection.DeleteOne(s => s.Id == ObjectId.Parse(textBox1.Text));
            ReadAllDocuments();
        }
    }
}
