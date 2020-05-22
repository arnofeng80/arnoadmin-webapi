﻿using ArnoAdminCore.Base;
using ArnoAdminCore.Utils.Extension;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class NPOIMemoryStream : MemoryStream
    {
        public bool IsColse
        {
            get;
            private set;
        }
        public NPOIMemoryStream(bool colse = false)
        {
            IsColse = colse;
        }
        public override void Close()
        {
            if (IsColse)
            {
                base.Close();
            }
        }
    }

    /// <summary>
    /// List匯出到Excel文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelHelper<T> where T : new()
    {

        #region List匯出到Excel文件
        /// <summary>
        /// List匯出到Excel文件
        /// </summary>
        /// <param name="sFileName"></param>
        /// <param name="sHeaderText"></param>
        /// <param name="list"></param>
        public string ExportToExcel(string sFileName, string sHeaderText, List<T> list, string[] columns)
        {
            sFileName = string.Format("{0}_{1}", sFileName, IdGenerator.GetId());
            string sRoot = GlobalContext.HostingEnvironment.ContentRootPath;
            string partDirectory = string.Format("Resource{0}Export{0}Excel", Path.DirectorySeparatorChar);
            string sDirectory = Path.Combine(sRoot, partDirectory);
            string sFilePath = Path.Combine(sDirectory, sFileName);
            if (!Directory.Exists(sDirectory))
            {
                Directory.CreateDirectory(sDirectory);
            }
            using (MemoryStream ms = CreateExportMemoryStream(list, sHeaderText, columns))
            {
                using (FileStream fs = new FileStream(sFilePath, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            return partDirectory + Path.DirectorySeparatorChar + sFileName;
        }

        /// <summary>  
        /// List匯出到Excel的MemoryStream  
        /// </summary>  
        /// <param name="list">資料來源</param>  
        /// <param name="sHeaderText">表頭文本</param>  
        /// <param name="columns">需要匯出的屬性</param>  
        public MemoryStream CreateExportMemoryStream(List<T> list, string sHeaderText, string[] columns)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            Type type = typeof(T);
            PropertyInfo[] properties = ReflectionHelper.GetProperties(type, columns);

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");

            #region 取得每列的列寬（最大寬度）
            int[] arrColWidth = new int[properties.Length];
            for (int columnIndex = 0; columnIndex < properties.Length; columnIndex++)
            {
                //GBK對應的code page是CP936
                arrColWidth[columnIndex] = properties[columnIndex].Name.Length;
            }
            #endregion
            for (int rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                #region 新建表，填充表頭，填充列頭，樣式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表頭及樣式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(sHeaderText);

                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;

                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, properties.Length - 1));
                    }
                    #endregion

                    #region 列頭及樣式
                    {
                        IRow headerRow = sheet.CreateRow(1);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        for (int columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                        {
                            // 類屬性如果有Description就用Description當做列名
                            DescriptionAttribute customAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(properties[columnIndex], typeof(DescriptionAttribute));
                            string description = properties[columnIndex].Name;
                            if (customAttribute != null)
                            {
                                description = customAttribute.Description;
                            }
                            headerRow.CreateCell(columnIndex).SetCellValue(description);
                            headerRow.GetCell(columnIndex).CellStyle = headStyle;

                            //設置列寬  
                            sheet.SetColumnWidth(columnIndex, (arrColWidth[columnIndex] + 1) * 256);
                        }
                    }
                    #endregion
                }
                #endregion

                #region 填充內容
                ICellStyle contentStyle = workbook.CreateCellStyle();
                contentStyle.Alignment = HorizontalAlignment.Left;
                IRow dataRow = sheet.CreateRow(rowIndex + 2); // 前面2行已被佔用
                for (int columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                {
                    ICell newCell = dataRow.CreateCell(columnIndex);
                    newCell.CellStyle = contentStyle;

                    string drValue = properties[columnIndex].GetValue(list[rowIndex], null).ParseToString();
                    switch (properties[columnIndex].PropertyType.ToString())
                    {
                        case "System.String":
                            newCell.SetCellValue(drValue);
                            break;

                        case "System.DateTime":
                        case "System.Nullable`1[System.DateTime]":
                            newCell.SetCellValue(drValue.ParseToDateTime());
                            newCell.CellStyle = dateStyle; //格式化顯示  
                            break;

                        case "System.Boolean":
                        case "System.Nullable`1[System.Boolean]":
                            newCell.SetCellValue(drValue.ParseToBool());
                            break;

                        case "System.Byte":
                        case "System.Nullable`1[System.Byte]":
                        case "System.Int16":
                        case "System.Nullable`1[System.Int16]":
                        case "System.Int32":
                        case "System.Nullable`1[System.Int32]":
                            newCell.SetCellValue(drValue.ParseToInt());
                            break;

                        case "System.Int64":
                        case "System.Nullable`1[System.Int64]":
                            newCell.SetCellValue(drValue.ParseToString());
                            break;

                        case "System.Double":
                        case "System.Nullable`1[System.Double]":
                            newCell.SetCellValue(drValue.ParseToDouble());
                            break;

                        case "System.Decimal":
                        case "System.Nullable`1[System.Decimal]":
                            newCell.SetCellValue(drValue.ParseToDouble());
                            break;

                        case "System.DBNull":
                            newCell.SetCellValue(string.Empty);
                            break;

                        default:
                            newCell.SetCellValue(string.Empty);
                            break;
                    }
                }
                #endregion
            }

            MemoryStream ms = new NPOIMemoryStream();
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            return ms;
        }
        #endregion

        #region Excel導入
        /// <summary>
        /// Excel導入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<T> ImportFromExcel(string filePath)
        {
            string absoluteFilePath = GlobalContext.HostingEnvironment.ContentRootPath + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            FileStream file = new FileStream(absoluteFilePath, FileMode.Open, FileAccess.Read);
            return ImportFromExcel(file);
        }
        /// <summary>
        /// Excel導入
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<T> ImportFromExcel(Stream file)
        {
            List<T> list = new List<T>();
            IWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheetAt(0); 
            IRow columnRow = sheet.GetRow(0); // 第二行為欄位名
            Dictionary<int, PropertyInfo> mapPropertyInfoDict = new Dictionary<int, PropertyInfo>();
            for (int j = 0; j < columnRow.LastCellNum; j++)
            {
                ICell cell = columnRow.GetCell(j);
                PropertyInfo propertyInfo = MapPropertyInfo(cell.ParseToString());
                if (propertyInfo != null)
                {
                    mapPropertyInfoDict.Add(j, propertyInfo);
                }
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                T entity = new T();
                for (int j = row.FirstCellNum; j < columnRow.LastCellNum; j++)
                {
                    if (mapPropertyInfoDict.ContainsKey(j))
                    {
                        if (row.GetCell(j) != null)
                        {
                            PropertyInfo propertyInfo = mapPropertyInfoDict[j];
                            switch (propertyInfo.PropertyType.ToString())
                            {
                                case "System.DateTime":
                                case "System.Nullable`1[System.DateTime]":
                                    if (row.GetCell(j).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                    {
                                        mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).DateCellValue);
                                    } else
                                    {
                                        mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToDateTime());
                                    }
                                    break;
                                case "System.Boolean":
                                case "System.Nullable`1[System.Boolean]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToBool());
                                    break;
                                case "System.Byte":
                                case "System.Nullable`1[System.Byte]":
                                    mapPropertyInfoDict[j].SetValue(entity, Byte.Parse(row.GetCell(j).ParseToString()));
                                    break;
                                case "System.Int16":
                                case "System.Nullable`1[System.Int16]":
                                    mapPropertyInfoDict[j].SetValue(entity, Int16.Parse(row.GetCell(j).ParseToString()));
                                    break;
                                case "System.Int32":
                                case "System.Nullable`1[System.Int32]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToInt());
                                    break;
                                case "System.Int64":
                                case "System.Nullable`1[System.Int64]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToLong());
                                    break;
                                case "System.Double":
                                case "System.Nullable`1[System.Double]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToDouble());
                                    break;
                                case "System.Decimal":
                                case "System.Nullable`1[System.Decimal]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString().ParseToDecimal());
                                    break;
                                default:
                                case "System.String":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ParseToString());
                                    break;
                            }
                        }
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 查找Excel列名對應的實體屬性
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private PropertyInfo MapPropertyInfo(string columnName)
        {
            PropertyInfo[] propertyList = ReflectionHelper.GetProperties(typeof(T));
            PropertyInfo propertyInfo = propertyList.Where(p => p.Name == columnName).FirstOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo;
            }
            else
            {
                foreach (PropertyInfo tempPropertyInfo in propertyList)
                {
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])tempPropertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        if (attributes[0].Description == columnName)
                        {
                            return tempPropertyInfo;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
