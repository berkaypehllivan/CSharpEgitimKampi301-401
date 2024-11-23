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
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }

        EgitimKampiEFTravelDbEntities db = new EgitimKampiEFTravelDbEntities();
        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            lblLocationCount.Text = db.Location.Count().ToString();
            lblSumCapacity.Text = db.Location.Sum(x => x.Capacity).ToString();
            lblGuideCount.Text = db.Guide.Count().ToString();
            lblAvgCapacity.Text = db.Location.Average(x => x.Capacity).ToString();

            //Burada derste istenilen fiyat bilgisinin yalnızca son iki rakamının getirilmesi için değişiklikler yapılmıştır.
            decimal avgPrice = (decimal)db.Location.Average(x => x.Price); //Burada öncelikle ortalama değeri decimal tipinde bir değişkene atadım. Sonrasında değerin sabit olarak decimal türünde işlem yapılmasını sağladım.
            lblAvgLocationPrice.Text = avgPrice.ToString("F2") + " ₺"; //Burada ise ortalaması alınan avgPrice değişkenini labela gönderip .ToString("F2") ifadesiyle de sayıyı 2 ondalık basamak formatına dönüştürdüm.

            int lastCountryId = db.Location.Max(x => x.LocationId);
            lblLastCountryName.Text = db.Location.Where(x => x.LocationId == lastCountryId).Select(y => y.Country).FirstOrDefault();

            lblMadridCapacity.Text = db.Location.Where(x => x.City == "Madrid").Select(y => y.Capacity).FirstOrDefault().ToString();

            lblAvgCapacityLocationItaly.Text = db.Location.Where(x => x.Country == "İtalya").Average(y => y.Capacity).ToString();

            var lyonGuideId = db.Location.Where(x => x.City == "Lyon").Select(y => y.GuideId).FirstOrDefault();
            lblGuideNameLocationLyon.Text = db.Guide.Where(x => x.GuideId == lyonGuideId).Select(y => y.Name + " " + y.Surname).FirstOrDefault().ToString();

            var maxCapacity = db.Location.Max(x => x.Capacity);
            lblMaxCapacityLocation.Text = db.Location.Where(x => x.Capacity == maxCapacity).Select(y => y.City).FirstOrDefault().ToString();

            var maxPrice = db.Location.Max(x => x.Price);
            lblMaxPriceLocation.Text = db.Location.Where(x => x.Price == maxPrice).Select(y => y.City).FirstOrDefault().ToString();

            var GuideIdByNameBerkayPehlivan = db.Guide.Where(x => x.Name == "Berkay" && x.Surname == "Pehlivan").Select(y => y.GuideId).FirstOrDefault();
            lblBerkayPehlivanLocationCount.Text = db.Location.Where(x => x.GuideId == GuideIdByNameBerkayPehlivan).Count().ToString();
        }
    }
}
