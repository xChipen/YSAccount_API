using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace Helpers
{
    public class ExcelHelper
    {
        public static MemoryStream exampleImportExcel(string[] data)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            for (int ii = 0; ii < data.Length; ii++)
            {
                //(Optional) set the width of the columns
                sheet.SetColumnWidth(ii, 20 * 256);
                //
                headerRow.CreateCell(ii).SetCellValue(data[ii]);
            }

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return output;
        }

        public static DataTable excelToDataTable(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            DataTable dataTable = new DataTable();
            IWorkbook wb;
            ISheet sheet;
            IRow headerRow;
            int cellCount; //紀錄共有幾欄

            Stream stream = file.InputStream;       //使用Stream(流)對檔案進行操作
            try
            {
                //依excel版本，NPOI載入檔案
                if (file.FileName.ToUpper().EndsWith("XLSX"))
                    wb = new XSSFWorkbook(stream); // excel版本(.xlsx)
                else
                    wb = new HSSFWorkbook(stream); // excel版本(.xls)

                //取第一個頁籤   
                sheet = wb.GetSheetAt(0);

                //取第一個頁籤的第一列
                headerRow = sheet.GetRow(0);

                //計算出第一列共有多少欄位
                cellCount = headerRow.LastCellNum;

                //迴圈執行第一列的第一個欄位到最後一個欄位，將抓到的值塞進DataTable做完欄位名稱
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    dataTable.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));
                }

                //int j; //計算每一列讀到第幾個欄位
                int column = 1; //計算每一列讀到第幾個欄位

                // 略過第零列(標題列)，一直處理至最後一列
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    //取目前的列(row)
                    IRow row = sheet.GetRow(i);

                    //若該列的第一個欄位無資料，break跳出
                    if (string.IsNullOrEmpty(row.Cells[0].ToString().Trim()))
                    {
                        break;
                    }

                    //宣告DataRow
                    DataRow dataRow = dataTable.NewRow();
                    //宣告ICell
                    ICell cell;

                    try
                    {
                        //依先前取得，依每一列的欄位數，逐一設定欄位內容
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            //計算每一列讀到第幾個欄位(秀在錯誤訊息上)
                            column = j + 1;

                            //設定cell為目前第j欄位
                            cell = row.GetCell(j);

                            if (cell != null) //若cell有值
                            {
                                //用cell.CellType判斷資料的型別
                                //再依照欄位屬性，用StringCellValue、DateCellValue、NumericCellValue、DateCellValue取值
                                switch (cell.CellType)
                                {
                                    //字串型態欄位
                                    case NPOI.SS.UserModel.CellType.String:
                                        //設定dataRow第j欄位的值，cell以字串型態取值
                                        dataRow[j] = cell.StringCellValue;
                                        break;

                                    //數字型態欄位
                                    case NPOI.SS.UserModel.CellType.Numeric:

                                        if (HSSFDateUtil.IsCellDateFormatted(cell)) //日期格式
                                        {
                                            //設定dataRow第j欄位的值，cell以日期格式取值
                                            dataRow[j] = DateTime.FromOADate(cell.NumericCellValue).ToString("yyyy/MM/dd HH:mm");
                                        }
                                        else //非日期格式
                                        {
                                            //設定dataRow第j欄位的值，cell以數字型態取值
                                            dataRow[j] = cell.NumericCellValue;
                                        }
                                        break;

                                    //布林值
                                    case NPOI.SS.UserModel.CellType.Boolean:

                                        //設定dataRow第j欄位的值，cell以布林型態取值
                                        dataRow[j] = cell.BooleanCellValue;
                                        break;

                                    //空值
                                    case NPOI.SS.UserModel.CellType.Blank:

                                        dataRow[j] = "";
                                        break;

                                    // 預設
                                    default:

                                        dataRow[j] = cell.StringCellValue;
                                        break;
                                }
                            }
                        }
                        //DataTable加入dataRow
                        dataTable.Rows.Add(dataRow);
                    }
                    catch (Exception ex)
                    {
                        //錯誤訊息
                        Log.Info("第 " + i + "列第" + column + "欄，資料格式有誤:\r\r" + ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
            }
            finally
            {
                //釋放資源
                sheet = null;
                wb = null;
                stream.Dispose();
                stream.Close();
            }

            return dataTable;
        }
    }
}