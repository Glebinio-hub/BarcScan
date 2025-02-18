using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeScanner.Model
{
    public class Barcodes
    {
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


	}
}
