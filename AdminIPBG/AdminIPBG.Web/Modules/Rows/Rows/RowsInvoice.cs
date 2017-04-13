﻿using AdminIPBG.Administration;

namespace AdminIPBG.Rows
{
    using Entities;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Reporting;
    using System;
    using System.Collections.Generic;

    [Report("Rows.RowsInvoice")]
    [ReportDesign(MVC.Views.Rows.RowsInvoice)]
    [RequiredPermission(PermissionKeys.Security)]
    public class RowsInvoice : IReport, ICustomizeHtmlToPdf
    {
        public Int32 RowId { get; set; }

        public object GetData()
        {
            var data = new RowsInvoiceData();

            using (var connection = SqlConnections.NewFor<RowsRow>())
            {
                var o = RowsRow.Fields;
                var row = connection.TryById<RowsRow>(this.RowId, r => r.SelectTableFields());
                var rowForeign = connection.TryById<RowsRow>(this.RowId, r => r.SelectForeignFields(row));
                data.Phase = rowForeign.PhaseName;
            }
            return data;
        }

        public void Customize(IHtmlToPdfOptions options)
        {
            // you may customize HTML to PDF converter (WKHTML) parameters here, e.g. 
            // options.MarginsAll = "2cm";
        }
    }

    public class RowsInvoiceData
    {
        public string Object { get; set; }

        public string Part { get; set; }
        public string Phase { get; set; }

        public string Client { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string UserTake { get; set; }

        public string UserGive { get; set; }

        public int ProjectCounts { get; set; }

        public int FoldersCount { get; set; }

        public int TotalCount { get; set; }
        
    }
}