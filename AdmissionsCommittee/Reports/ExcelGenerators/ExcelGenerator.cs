using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Reports.ExcelGenerators
{
    public class ExcelGenerator
    {
        public byte[] Generate(EnrolleeReport report)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets
                .Add("EnrolleeReport");

            sheet.Cells["C1"].Value = "�����������";
            sheet.Cells["A2"].Value = "���";
            sheet.Cells["B2"].Value = "�������";
            sheet.Cells["C2"].Value = "��������";
            sheet.Cells["D2"].Value = "���";
            sheet.Cells["E2"].Value = "���� ��������";
            sheet.Cells["F2"].Value = "�������";
            sheet.Cells["G2"].Value = "�����";
            sheet.Cells["H2"].Value = "��� �����������";
            sheet.Cells["I2"].Value = "������";
            sheet.Cells["J2"].Value = "��������";
            sheet.Cells["K2"].Value = "�����������";
            sheet.Cells["L2"].Value = "�����������";
            sheet.Cells["M2"].Value = "������������";
            sheet.Cells["N2"].Value = "������� ��� ���������";
            sheet.Cells["O2"].Value = "�������� ���������";
            sheet.Cells["P2"].Value = "���������";
            sheet.Cells["Q2"].Value = "������� ����������";
            sheet.Cells["R2"].Value = "�����";
            
            report.Enrollees.ForEach(enrollee =>
            {
                int index = report.Enrollees.IndexOf(enrollee) + 3;
                sheet.Cells[index, 1].Value = enrollee.Name;
                sheet.Cells[index, 2].Value = enrollee.Surname;
                sheet.Cells[index, 3].Value = enrollee.Patronymic;
                sheet.Cells[index, 4].Value = enrollee.Gender;
                sheet.Cells[index, 5].Value = enrollee.DateOfBirth;
                sheet.Cells[index, 6].Value = enrollee.Age;
                sheet.Cells[index, 7].Value = enrollee.Snils;
                sheet.Cells[index, 8].Value = enrollee.YearOfAdmission;
                sheet.Cells[index, 9].Value = enrollee.IsBudget;
                sheet.Cells[index, 10].Value = enrollee.IsEnlisted;
                sheet.Cells[index, 11].Value = enrollee.Citizenship.Country;
                sheet.Cells[index, 12].Value = enrollee.Education.DisplayName;
                sheet.Cells[index, 13].Value = "�����������";
                if (enrollee.Disability.Document != null)
                {
                    sheet.Cells[index, 13].Value = "����";
                }
                sheet.Cells[index, 14].Value = enrollee.Certificate.AvarageScore;
                sheet.Cells[index, 15].Value = enrollee.Certificate.Original;
                sheet.Cells[index, 16].Value = "���";
                if (enrollee.Ward.Document != null)
                {
                    sheet.Cells[index, 16].Value = "��";
                }
                sheet.Cells[index, 17].Value = enrollee.PlaceOfResidence.Name;
                sheet.Cells[index, 18].Value = enrollee.District.Name;
                return;
            });

            sheet.Column(5).Style.Numberformat.Format = "yyyy-mm-dd";
            sheet.Column(7).Style.Numberformat.Format = "yyyy-mm-dd";

            sheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
