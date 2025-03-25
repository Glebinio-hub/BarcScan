using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.Model
{
    public class Plants
    {

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        private string barcode;

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        private int articul;

        public int Articul
        {
            get { return articul; }
            set { articul = value; }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int retailPrice;

        public int RetailPrice
        {
            get { return retailPrice; }
            set { retailPrice = value; }
        }

        private int wholesalePrice;

        public int WholesalePrice
        {
            get { return wholesalePrice; }
            set { wholesalePrice = value; }
        }

    }

}

