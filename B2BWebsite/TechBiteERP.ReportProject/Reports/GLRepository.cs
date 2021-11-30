//using AutoMapper;

using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TechBiteERP.ReportProject.Reports
{
    public class GLRepository
    {
        public IConfiguration conf;
        //ReportHelper dbHelper = new ReportHelper(conf);
        public GLRepository(IConfiguration configuration)
        {
            conf = configuration;
        }
        public DataTable GetChartOfAccount(int companyId, int AccLevel)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spChartOfAccount";

            SqlParameter[] parameters =
            {
               new SqlParameter("@AccountLevel",SqlDbType.Int) {Value=AccLevel},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }

        public DataTable GetNoteOfAccount(int companyId, int AccLevel, DateTime fromDate, DateTime toDate, int BranchId)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spNoteOfAccount";

            SqlParameter[] parameters =
            {
                 new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@BranchId",SqlDbType.Int) {Value=BranchId},
                new SqlParameter("@AccountNote",SqlDbType.Int) {Value=AccLevel},
               new SqlParameter("@StartDate",SqlDbType.DateTime) {Value=fromDate},
               new SqlParameter("@EndDate",SqlDbType.DateTime) {Value=toDate},

            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }

        public DataTable GetTrialBalance(int companyId, int AccLevel, DateTime fromDate, DateTime toDate, int BranchId)
        {

            DataTable dataTable = new DataTable();
            string sql;   
            sql = @"SP_TrialBalance";

            SqlParameter[] parameters =
            {
               
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@BranchId",SqlDbType.Int) {Value=BranchId},
               new SqlParameter("@AccountLevel",SqlDbType.Int) {Value=AccLevel},
               new SqlParameter("@StartDate",SqlDbType.DateTime) {Value=fromDate},
               new SqlParameter("@EndDate",SqlDbType.DateTime) {Value=toDate},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }

        public DataTable GetBalanceSheet(int companyId, DateTime toDate, int BranchId)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"SP_BalanceSheet";

            SqlParameter[] parameters =
            {

               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@BranchId",SqlDbType.Int) {Value=BranchId},
               new SqlParameter("@EndDate",SqlDbType.DateTime) {Value=toDate},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetCommonReport(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"Select * from  " + fromtable + " ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@fromtable",SqlDbType.NVarChar) {Value=fromtable},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetCity(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spCity";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetCountry(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spCountry ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetProvince(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spProvince";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetDistrict(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spDistrict ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        }
        public DataTable GetArea(int companyId, string fromtable)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spArea ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;
        
        }

        public DataTable GetLedger(int BranchId,int companyId,int Posted,DateTime fromDate, DateTime toDate, int FromAccNo,int ToAccNo)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spLedger
             ";

            SqlParameter[] parameters =
            {
               //new SqlParameter("@fromtable",SqlDbType.NVarChar) {Value=fromtable},
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@BranchId",SqlDbType.Int) {Value=BranchId},
               new SqlParameter("@fromDate",SqlDbType.DateTime) {Value=fromDate},
               new SqlParameter("@toDate",SqlDbType.DateTime) {Value=toDate},
               new SqlParameter("@FromAccNo",SqlDbType.Int) {Value=FromAccNo},
               new SqlParameter("@ToAccNo",SqlDbType.Int) {Value=ToAccNo},
               new SqlParameter("@Posted",SqlDbType.Int) {Value=Posted},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetSaleRegisteration(int companyId,int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotRegisteration";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetPaymentHistory(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPaymentHistory";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetBokingCertificate(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spBookingCertificate ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }
        public DataTable GetPlotTransfer(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotTransfer ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        } 
        public DataTable GetPlotCancellation(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotCancellation ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetPlotSale(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotSale";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }  
        public DataTable GetPlotRecovery(int companyId, DateTime fromDate,DateTime toDate)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @" spPlotRecovery ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@fromDate",SqlDbType.DateTime) {Value=fromDate},
               new SqlParameter("@toDate",SqlDbType.DateTime) {Value=toDate},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }
        public DataTable GetPaymentPlan(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPaymentPlan ";

            SqlParameter[] parameters =
            {
                 new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetRegisterationforms(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spRegisterationForms ";

            SqlParameter[] parameters =
            {
                 new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }


        public DataTable GetItem(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spItem ";

            SqlParameter[] parameters =
            {
                 new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }
        public DataTable GetPlotFile(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotFile ";

            SqlParameter[] parameters =
            {
                new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }
        public DataTable GetPlotSaleBase(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotSaleBase ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }

        public DataTable GetPlotRecoveryBase(int companyId, int Id)
        {

            DataTable dataTable = new DataTable();
            string sql;
            sql = @"spPlotRecoveryBase ";

            SqlParameter[] parameters =
            {
               new SqlParameter("@companyId",SqlDbType.Int) {Value=companyId},
               new SqlParameter("@Id",SqlDbType.Int) {Value=Id},
            };
            var dbHelper = new ReportHelper(conf);
            dataTable = dbHelper.GetSPDataTable(sql, parameters);
            return dataTable;

        }


    }
}
