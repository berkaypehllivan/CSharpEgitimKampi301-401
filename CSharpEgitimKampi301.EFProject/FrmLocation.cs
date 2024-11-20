using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EFProject
{
    public partial class FrmLocation : Form
    {
        public FrmLocation()
        {
            InitializeComponent();
        }

        EgitimKampiEFTravelDbEntities db = new EgitimKampiEFTravelDbEntities();

        private void btnList_Click(object sender, EventArgs e)
        {
            var values = db.Location.ToList();
            dataGridView1.DataSource = values;
        }

        private void FrmLocation_Load(object sender, EventArgs e)
        {
            var values = db.Guide.Select(x => new
            {
                FullName = x.Name + " " + x.Surname,
                x.GuideId
            }).ToList();

            cmbGuide.DisplayMember = "FullName";
            cmbGuide.ValueMember = "GuideId";
            cmbGuide.DataSource = values;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Location location = new Location();
            location.Capacity = byte.Parse(nudCapacity.Text.ToString());
            location.City = txtCity.Text;
            location.Country = txtCountry.Text;
            location.Price = decimal.Parse(txtPrice.Text.ToString());
            location.DayNight = txtDayNight.Text;
            location.GuideId = int.Parse(cmbGuide.SelectedValue.ToString());
            db.Location.Add(location);
            db.SaveChanges();
            MessageBox.Show("Ekleme işlemi başarılı");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text.ToString());
            var deleteValue = db.Location.Find(id);
            db.Location.Remove(deleteValue);
            db.SaveChanges();
            MessageBox.Show("Silme işlemi başarılı");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text.ToString());
            var updatedValue = db.Location.Find(id);
            updatedValue.DayNight = txtDayNight.Text.ToString();
            updatedValue.Price = decimal.Parse(txtPrice.Text);
            updatedValue.Capacity = byte.Parse(nudCapacity.Value.ToString());
            updatedValue.City = txtCity.Text.ToString();
            updatedValue.Country = txtCountry.Text.ToString();
            updatedValue.GuideId = int.Parse(cmbGuide.SelectedValue.ToString());
            db.SaveChanges();
            MessageBox.Show("Güncelleme işlemi başarılı");
        }
    }
}
