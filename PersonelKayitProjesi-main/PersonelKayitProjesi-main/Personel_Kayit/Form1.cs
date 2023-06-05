
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Kayit
{
    public partial class Form1 : Form
    {
        public PersonelService PersonelService = new PersonelService();
        public MapHelper MapHelper = new MapHelper();

        public Form1()
        {

            InitializeComponent();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            this.CreateOrRefreshTable();
            this.CleanForm();
            var cities = PersonelService.GetAllCity();
            foreach (var item in cities)
            {
                this.cmbCities.Items.Add(item.name);
            }


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.txtId.Text == "")
                {
                    var cityList = PersonelService.GetAllCity();

                    var personelDto = new PersonelDto()
                    {
                        İsim = this.TxtAd.Text,
                        Soyisim = this.TxtSoyad.Text,
                        Şehir = this.cmbCities.Text,
                        Meslek = this.TxtMeslek.Text,
                        MedeniHal = this.martialStatus.Checked,
                        Maaş = Convert.ToInt32(this.TxtMaas.Text)
                    };

                    var personel = MapHelper.MapPersonel(personelDto, cityList);
                    PersonelService.AddPersonel(personel);

                    MessageBox.Show("Başarılı");
                    CreateOrRefreshTable();

                }
                else
                {
                    MessageBox.Show("Lütfen Yeni Bir Personel Giriniz!");
                    this.CleanForm();
                }
              

            }
            catch (Exception)
            {
                MessageBox.Show("Başarısız");

                throw;
            }


        }


        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            this.CleanForm();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.txtId.Text;
                if (id != "")
                {
                    PersonelService.DeletePersonel(Convert.ToInt32(id));
                    this.CreateOrRefreshTable();
                    this.CleanForm();
                    MessageBox.Show("Silindi");
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Personel Seçiniz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu!");
                throw;
            }
         
           
        }

  
        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var cities = PersonelService.GetAllCity();
                string id = this.txtId.Text;
                if (id != "")
                {
                  
                    var personelDto = this.GetPersonelDto();
                    var mappedPersonel = MapHelper.MapPersonel(personelDto, cities);

                    PersonelService.UpdatePersonel(mappedPersonel);
                    this.CreateOrRefreshTable();
                    this.CleanForm();
                    MessageBox.Show("Güncellendi");
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Personel Seçiniz");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bir Hata Oluştu!");
                throw;
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void CreateOrRefreshTable()
        {
          
            var personelList = PersonelService.GetAllPersonels();
            var cityList = PersonelService.GetAllCity();


            if (personelList.Count > 0)
            {

              
                var data = MapHelper.MapPersonelDto(personelList, cityList);
                dataGridView1.DataSource = data;

            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                int Id = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                var selectedPersonel= new PersonelDto()
                {
                    Id = Id,
                    İsim= Convert.ToString(selectedRow.Cells["İsim"].Value),
                    Soyisim = Convert.ToString(selectedRow.Cells["Soyisim"].Value),
                    Maaş = Convert.ToInt32(selectedRow.Cells["Maaş"].Value),
                    MedeniHal = Convert.ToBoolean(selectedRow.Cells["MedeniHal"].Value),
                    Meslek = Convert.ToString(selectedRow.Cells["Meslek"].Value),
                    Şehir = Convert.ToString(selectedRow.Cells["Şehir"].Value)
                };

                this.SetSelectedPersonel(selectedPersonel);
            }

        }

        private void SetSelectedPersonel(PersonelDto personelDto)
        {
            this.txtId.Text = personelDto.Id.ToString();
            this.TxtAd.Text = personelDto.İsim;
            this.TxtSoyad.Text = personelDto.Soyisim;
            this.TxtMeslek.Text = personelDto.Meslek;
            this.TxtMaas.Text =personelDto.Maaş.ToString();
            this.cmbCities.Text = personelDto.Şehir;
            this.martialStatus.Checked = personelDto.MedeniHal;
        }

        private PersonelDto GetPersonelDto()
        {
            PersonelDto personelDto = new PersonelDto()
            {
                Id = Convert.ToInt32(this.txtId.Text),
                İsim = this.TxtAd.Text,
                Soyisim = this.TxtSoyad.Text,
                Maaş = Convert.ToInt32(this.TxtMaas.Text),
                MedeniHal = this.martialStatus.Checked,
                Meslek = this.TxtMeslek.Text,
                Şehir = this.cmbCities.Text


            };

            return personelDto;
        }

        private void CleanForm()
        {
            this.txtId.Text = "";
            this.TxtAd.Text = "";
            this.TxtSoyad.Text = "";
            this.TxtMeslek.Text = "";
            this.TxtMaas.Text = "";
            this.cmbCities.Text = "";
            this.martialStatus.Checked = false;


        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }
    }
}
