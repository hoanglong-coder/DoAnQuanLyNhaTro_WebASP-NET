using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class FileNameReportcs
    {
        string filename;
        DateTime time;
        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }
    }
}
