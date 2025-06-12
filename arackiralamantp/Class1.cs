
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AracKiralamaSistemi
{
    public class Customer
    {

        public int Id { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string EhliyetNo { get; set; }

        public Car AktifAraba { get; set; }
        public double ToplamUcret { get; set; }

        
        public Customer(int id, string ad, string soyad, string telefon, string email, string ehliyetNo)
        {
            Id = id;
            Ad = ad;
            Soyad = soyad;
            Telefon = telefon;
            Email = email;
            EhliyetNo = ehliyetNo;
            AktifAraba = null;
            ToplamUcret = 0;
        }



        public bool Kirala(Car car, int gun)
        {

            if (AktifAraba != null)
            {
                MessageBox.Show("Zaten kiralanmış bir aracınız bulunmakta!");
                return false;
            }



            if (car.Musteri == null)
            {
                car.Musteri = this;
                AktifAraba = car;
                double ucret = car.GunlukUcret * gun;
                car.KiralamaBitisTarihi = DateTime.Now.AddDays(gun);
                ToplamUcret = ucret;
                return true;
            }
            Console.WriteLine("Kiralama başarısız: Bu araç zaten kiralanmış");
            return false;
        }


        public void IadeEt(Car car)
        {

            car.Musteri = null;
            AktifAraba = null;
            car.KiralamaBitisTarihi = null;
            ToplamUcret = 0;
           

        }

        public override string ToString()
        {
            return $"{Ad} {Soyad}";
        }



    }

    public class Car
    {

        public Car(int id, string marka, string model, int yil, string plaka, double gunlukUcret)
        {
            Id = id;
            Marka = marka;
            Model = model;
            Yil = yil;
            Plaka = plaka;
            GunlukUcret = gunlukUcret;
            Musteri = null;

        }

        public int Id { get; set; }


        public string Marka { get; set; }


        public string Model { get; set; }


        public int Yil { get; set; }


        public string Plaka { get; set; }


        public double GunlukUcret { get; set; }


        public Customer Musteri { get; set; }
        public DateTime? KiralamaBitisTarihi { get; set; }


        public override string ToString()
        {
            return $"{Marka} {Model} ({Plaka})";
        }





    }





}
