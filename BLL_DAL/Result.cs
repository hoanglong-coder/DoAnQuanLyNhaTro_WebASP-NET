using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class Result:IComparable<Result>
    {
        float gia;
        double distance;

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

        public double Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }
        public Result() { }

        public int CompareTo(Result obj) // OverRight phương thức CompareTo của Interface IComparable
        {
            return this.Distance.CompareTo(obj.Distance); //Phương thức CompareTo này có sẵn với các kiểu cơ bản như Integer, String.
        }
    }
}
