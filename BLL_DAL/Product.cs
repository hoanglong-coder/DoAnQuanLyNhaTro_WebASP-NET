using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class Product
    {
        string Diachi;
        int Dientich;
        float gia;
        int location;
        public string Diachi1
        {
            get
            {
                return Diachi;
            }

            set
            {
                Diachi = value;
            }
        }

        public int Dientich1
        {
            get
            {
                return Dientich;
            }

            set
            {
                Dientich = value;
            }
        }

        public float Gia
        {
            get
            {
                return gia;
            }

            set
            {
                gia = value;
            }
        }

        public int Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public Product() { }

    }
}
