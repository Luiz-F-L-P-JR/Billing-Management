
using Billing.Management.Application.FileHandler.Excel.Interface;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection;

namespace Billing.Management.Application.FileHandler.Excel
{
    public class ExcelBuilder<T> : IExcelBuilder<T>
    {
        public byte[] CreateFile(IList<T> entities)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet");
            var rowHeader = sheet.CreateRow(0);

            var properties = typeof(T).GetProperties();

            CreateHeader(workbook, properties, rowHeader);
            CreateContent(entities, sheet, properties);

            var stream = new MemoryStream();
            workbook.Write(stream);
            var content = stream.ToArray();

            return content;
        }

        private void CreateHeader(XSSFWorkbook workbook, PropertyInfo[] properties, IRow rowHeader)
        {
            var font = workbook.CreateFont();
            font.IsBold = true;
            var style = workbook.CreateCellStyle();
            style.SetFont(font);

            var colIndex = 0;
            foreach (var property in properties)
            {
                var cell = rowHeader.CreateCell(colIndex);
                cell.SetCellValue(property.Name);
                cell.CellStyle = style;
                colIndex++;
            }
        }

        private void CreateContent(IList<T> entities, ISheet sheet, PropertyInfo[] properties)
        {
            var rowNum = 1;
            foreach (var item in entities)
            {
                var rowContent = sheet.CreateRow(rowNum);

                var colContentIndex = 0;
                foreach (var property in properties)
                {
                    var cellContent = rowContent.CreateCell(colContentIndex);
                    var value = property.GetValue(item, null);

                    if (value == null)
                    {
                        cellContent.SetCellValue("");
                    }
                    else if (property.PropertyType == typeof(string))
                    {
                        cellContent.SetCellValue(value.ToString());
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        cellContent.SetCellValue(Convert.ToInt32(value));
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        cellContent.SetCellValue(Convert.ToDouble(value));
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        var dateValue = (DateTime)value;
                        cellContent.SetCellValue(dateValue.ToString("g"));
                    }
                    else if (property.PropertyType == typeof(List<T>))
                    {
                        var models = (List<T>)value;

                        foreach(var model in models)
                        {
                            cellContent.SetCellValue(model.ToString());
                        }
                    }
                    else cellContent.SetCellValue(value.ToString());

                    colContentIndex++;
                }

                rowNum++;
            }
        }
    }
}
