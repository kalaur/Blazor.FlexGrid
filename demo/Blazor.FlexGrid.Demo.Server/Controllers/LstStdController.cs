using Blazor.FlexGrid.Demo.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
//using System.Data.Common.
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace Blazor.FlexGrid.Demo.Server.Controllers
{
    // this snippet was taken from 
    // https://stackoverflow.com/questions/208532/how-do-you-convert-a-datatable-into-a-generic-list  
    // https://gist.github.com/gaui/a0a615029f1327296cf8 
    // and slightly modified
    // BEGIN SNIPPET
    public static class DT2Lst
    {
        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                // my code
                DataRow row;

                //original
                //foreach (var row in table.AsEnumerable())
                for (int i= 0;i<table.Rows.Count;i++)
                {
                    // my code
                    row = table.Rows[i];
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
    // END SNIPPET

    [Route("api/[controller]")]
    public class LstStdController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<LstStd> LstStdGet()
        {
            //var rng = new Random();
            DataTable tdt = new DataTable();
            DataColumn tdc1 = new DataColumn("cnm", typeof(string));
            DataColumn tdc2 = new DataColumn("odt", typeof(DateTime));
            DataColumn tdc3 = new DataColumn("osum", typeof(int));
            tdt.Columns.AddRange(new DataColumn[] { tdc1, tdc2, tdc3 });
            tdt.Rows.Add(new object[] {"John Smith",new DateTime (2018,7,21),5000 });
            tdt.Rows.Add(new object[] { "Mary Poppins", new DateTime(2018, 7, 25), 7000 });
            tdt.Rows.Add(new object[] { "Jack Sparrow", new DateTime(2018, 6, 1), 3000 });
            var tvl = DT2Lst.DataTableToList<LstStd>(tdt);
            //LstStd[] tvr = tvl.ToArray<LstStd>;
            //var tvr= IEnumerable<LstStd>.
            var tvr = tvl.ToArray();
                //Enumerable.Range(1, 20).Select(index =>
                // new WeatherForecast
                // {
                //     Date = DateTime.Now.AddDays(index),
                //     TemperatureC = rng.Next(-20, 55),
                //     Summary = Summaries[rng.Next(Summaries.Length)]

                // });
            return tvr;
        }
    }
}