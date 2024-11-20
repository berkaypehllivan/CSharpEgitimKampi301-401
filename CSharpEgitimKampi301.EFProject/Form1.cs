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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        EgitimKampiEFTravelDbEntities db = new EgitimKampiEFTravelDbEntities(); // Bu kısımda bir önceki derste oluşturulan Sınıfı referans aldık ve bu bilgiyi db değişkenine atadık.

        private void btnList_Click(object sender, EventArgs e) //Bu form uygulamamızda oluşturduğumuz Listele butonunun metodudur.
        {
            var values = db.Guide.ToList(); //Bu kodla values adında lokal bir değişken oluşturduk ve bu değişkene db içerisindeki Guide sınıfındaki bilgileri aktaran ToList metodunu çağırdık. ToListMetodu Entity Framework'ün içerisinde olan bir metottur.
            dataGridView1.DataSource = values; // Bu kodla da form kısmında oluşturduğumuz datagridview'in DataSource yani içerisindeki bilgileri tutan kısmına da ToList ile eklediğimiz verileri tutan values değişkenini atadık.
        }

        private void btnAdd_Click(object sender, EventArgs e) //Bu form uygulamamızda oluşturduğumuz Ekle butonunun metodudur.
        {
            Guide guide = new Guide(); //Burada Guide sınıfından küçük bir guide objesi oluşturduk.
            guide.Name = txtName.Text; //Burada oluşturduğumuz guide'ın içerisindeki Name bilgisine textBoxa girilen bilginin metnini aktardık.
            guide.Surname = txtSurname.Text; //Burada da aynı Name'de yapılan işlemi Surname için de yaptık.
            db.Guide.Add(guide); //Burada da db içerisindeki Guide sınıfına Add metodunu kullanarak ve içerisine oluşturup name ve surname verilerini girdiğimiz guide objesini gönderdik.
            db.SaveChanges(); //Bu kod parçası yapılan işlemlerin kaydedilmesini sağlayıp bizim tekrardan listele dediğimizde datagridview'de görüntüleyebilmemizi sağlıyor.
            MessageBox.Show("Rehber Başarıyla Eklendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Bu kod parçası bir bilgi mesajı oluşturmaya yarıyor.
        }

        private void btnDelete_Click(object sender, EventArgs e) //Bu form uygulamamızda oluşturduğumuz Sil butonunun metodudur.
        {
            int id = int.Parse(txtId.Text); //Burada id adında bir lokal değişken oluşturup onun içerisine Id textboxuna girilen değeri int olarak dönüştürüp atamasını yapıyor.
            var removeValue = db.Guide.Find(id); //Burada removeValue adında silinecek olan veriyi tutacak değişken tanımlanıp içerisine de db içerisindeki Guide sınıfına Find metodunu kullanarak ve içerisine oluşturduğumuz id bilgisi parametre olarak gönderilip ataması yapılıyor.
            db.Guide.Remove(removeValue); //Buradaki kod parçası da atamasını yaptığımız removeValue değişkeninin silinmesini sağlayan Remove metoduna parametre olarak ekleniyor.
            db.SaveChanges(); //Bu kod parçası yapılan işlemlerin kaydedilmesini sağlayıp bizim tekrardan listele dediğimizde datagridview'de görüntüleyebilmemizi sağlıyor.
            MessageBox.Show("Rehber Başarıyla Silindi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Bu kod parçası bir bilgi mesajı oluşturmaya yarıyor.
        }

        private void btnUpdate_Click(object sender, EventArgs e) //Bu form uygulamamızda oluşturduğumuz Güncelle butonunun metodudur.
        {
            int id = int.Parse(txtId.Text); //Aynı id işlemi tekrar kullanılıyor.
            var updateValue = db.Guide.Find(id); //Burada updateValue adında güncellenecek olan veriyi tutacak değişken tanımlanıp içerisine de db içerisindeki Guide sınıfına Find metodunu kullanarak ve içerisine oluşturduğumuz id bilgisi parametre olarak gönderilip ataması yapılıyor.
            updateValue.Name = txtName.Text; //Bu kod parçasında atanan verinin içerisindeki name bilgisini isim textboxuna girilen veriyi iletiyor.
            updateValue.Surname = txtSurname.Text; //Bu kod parçasında atanan verinin içerisindeki surname bilgisini soyisim textboxuna girilen veriyi iletiyor.
            db.SaveChanges();
            MessageBox.Show("Rehber Başarıyla Güncellendi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGetById_Click(object sender, EventArgs e) //Bu form uygulamamızda oluşturduğumuz Id'ye Göre Getir butonunun metodudur.
        {
            int id = int.Parse(txtId.Text); //Aynı id işlemi tekrar kullanılıyor.
            var values = db.Guide.Where(x => x.GuideId == id).ToList(); //Burada Entity Framework içerisinde bulunan lambda ifadesi kullanılıyor. Her bir satır (x) üzerinde GuideId değerinin id ile eşit olup olmadığı sorgulanıyor ve ToList metodu kullanılıyor. Yapılan sorgudan çıkan bilgi de values adındaki değişkene tanımlanıyor.
            dataGridView1.DataSource = values; //Bu kod parçası datagridview'in DataSource kısmına yani içerisindeki bilgileri tutan yere values değişkenini tanımlıyor.
        }
    }
}
