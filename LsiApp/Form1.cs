using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LsiApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DownloadData();
        }

        //INFO//
        /* Model bazy danych o rozszerzeniu .bak znajduje się w repozytorium. W celu pobrania danych z bazy danych wykorzystano technologię LINQ oraz EntityFramework
            KOD TSQL do utworzenia bazy danych:

                            create database LSI_DB
                    use [LSI_DB]

                    CREATE TABLE LSI_APP (
                        ID int IDENTITY(1,1) PRIMARY KEY,
                        NazwaEksportu varchar(255) NOT NULL,
                        DataGodzina DateTime,
                        Uzytkownik varchar(255) NOT NULL,
	                    Lokal varchar(255) NOT NULL,
   
                    );
            Dane w bazie zostały uzupełnione poprzez edycje tabeli w narzędziu MSSQL

         */


        private void AcceptClick_Click(object sender, EventArgs e)
        {
            //zdefiniowanie zmiennych 
            var lokal = comboBoxEdit1.SelectedItem;
            var dataod = dateEdit1.DateTime;
            var datado = dateEdit2.DateTime;


            //utworznie zapytania za pomocą LINQ
            Model1 dc = new Model1();
            var result = from ed in dc.LSI_APP
                         where ed.Lokal == lokal.ToString() && ed.DataGodzina >= dataod && ed.DataGodzina <= datado
                         select new
                         {
                             Nazwa = ed.NazwaEksportu,
                             Data = ed.DataGodzina,
                             Uzytkownik = ed.Uzytkownik,
                             Lokal = ed.Lokal

                         };
            //załadowanie elementów do listy
            var elements = result.ToList();


            List<Elements> ListaElementow = new List<Elements>();
            //przeszukanie listy oraz przepisanie do zdefiniowanej przez parametry listy z uwzględnieniem godzin
            foreach (var item in elements)
            {
                ListaElementow.Add(new Elements
                {
                    Nazwa = item.Nazwa,
                    Lokal = item.Lokal,
                    Data = item.Data,
                    Godzina = item.Data.Value.TimeOfDay.ToString(),
                    Uzytkownik = item.Uzytkownik,


                });
            }


            //wypełnienie kontrolki GridView

            gridControl1.BeginUpdate();
            try
            {
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                gridControl1.DataSource = (ListaElementow);
            }
            finally
            {
                gridControl1.EndUpdate();
            }

        }

        public void DownloadData()
        {
            //pobieranie danych początkowych dla ComboBoxa, aby w wysuwanej liście pojawiały nam się elementy z bazy danych

            using (Model1 dc = new Model1())
            {
                //utworznie zapytania za pomocą LINQ
                var data = (from x in dc.LSI_APP
                            select x);

                var lista = data.ToList();

                List<LocalsModel> listaLokali = new List<LocalsModel>();

                foreach (var item in lista)
                {
                    listaLokali.Add(new LocalsModel { Lokal = item.Lokal });
                }

                foreach (var itemtwo in listaLokali)
                {
                    //ładowanie do kontrolki 
                    comboBoxEdit1.Properties.Items.Add(itemtwo.Lokal);
                }




            }
        }
    }
}
