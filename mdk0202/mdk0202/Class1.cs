using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdk0202
{
    internal class Class1
    {
        Entities db = new Entities();
        public int MaterialAm(int productTypeId, int materialTypeId, int requiredAmPr, int wareHouseAmPr, double width, double height)
        {
            // Получаем коэффициент типа продукта из базы данных
            double? kfTypeProduct = db.ProductType.FirstOrDefault(x => x.ID == productTypeId).kfTypeProduct;
            // Получаем процент брака для типа материала из базы данных
            double? procentBrak = db.MaterialType.FirstOrDefault(x => x.ID == materialTypeId).defectRate;

            if (kfTypeProduct != null || procentBrak != null)
            {
                double? materialAmountOnUnit = width * height * kfTypeProduct;
                double? brakAmount = (materialAmountOnUnit * procentBrak) + materialAmountOnUnit;
                double? vipuskProducts = requiredAmPr - wareHouseAmPr;
                double? missingProducts = brakAmount * vipuskProducts;
                int itogAmount = (int)Math.Round(Convert.ToDouble(missingProducts));

                return itogAmount;
            }
            else
            {
                return -1;
            }
        }
    }
}
