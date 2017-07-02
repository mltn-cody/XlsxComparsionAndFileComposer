using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Management.Framework;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/ff963547.aspx
    /// </remarks>>
    public class DataTableComparer : ICompare<DataTable>
    {
        private readonly DataTable _theirData;
        private readonly DataTable _ourData;
        private readonly object syncLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theirData"></param>
        /// <param name="ourData"></param>
        public DataTableComparer(DataTable theirData, DataTable ourData)
        {
            _theirData = theirData;
            _ourData = ourData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="theirColumnKey"></param>
        /// <param name="ourColumnKey"></param>
        /// <returns></returns>
        public Task<DataTable> CompareAsync(string theirColumnKey, string ourColumnKey)
        {
            // Todo use data parallelism to split this task. 
            // Todo this maybe a canidate for map/reduce algorithm
            return Task.Run(() =>
            {
                var outputTable = new DataTable();

                Parallel.ForEach(_ourData.AsEnumerable(), row => LoadData(theirColumnKey, ourColumnKey, row, outputTable));

                return outputTable;
            });
        }

        private void LoadData(string theirColumnKey, string ourColumnKey, DataRow row, DataTable outputTable)
        {
            var matchingRows = _theirData.AsEnumerable()
                .Where(r => Regex.IsMatch(r[theirColumnKey].ToString(), row[ourColumnKey].ToString()));
            if (!matchingRows.Any()) return;

            lock (syncLock)
            {
                outputTable.Merge(matchingRows.CopyToDataTable());
            }

        }


        private IEnumerable<DataRow> Map(string theirColumnKey, string ourColumnKey, DataRow row)
        {
            return
            _theirData.AsEnumerable()
                .Where(r => Regex.IsMatch(r[theirColumnKey].ToString(), row[ourColumnKey].ToString()))
                .AsEnumerable()
                .Join(new[] {row},
                    tData => tData,
                    oData => oData,
                    (DataRow tData, DataRow oData) =>
                    {
                        var tempOurDataTable = new DataTable("Temp");
                        var tempRow = tempOurDataTable.NewRow();
                        var theirColumns = tData.Table.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => tData.Field<string>(col.ColumnName));

                        var ourColumns = oData.Table.Columns.Cast<DataColumn>()
                            .ToDictionary(col => col.ColumnName, col => oData.Field<string>(col.ColumnName));



                        //foreach (var kvp in columns)
                        //{
                        //    tempRow[kvp.Key] = kvp.Value;
                        //}

                        //tempOurDataTable.Rows.Add(tempRow);

                        //var tempTheirDataTable = new DataTable("Temp2");

                        return tempOurDataTable.Rows[0];


                    });
            throw new NotImplementedException();
        }

        private Task Reduce()
        {
            return Task.Run(() => { });
        }

        private void AddColumns(DataTable source, DataTable target)
        {
            foreach (DataColumn c in source.Columns)
            {
                target.Columns.Add($"{source.TableName}_{c.ColumnName}", c.DataType);
            }
        }

        private void SetMergedRowValue(DataRow source, DataRow target, int index)
        {
            var columnName = $"{source.Table.TableName}_{source.Table.Columns[index]}";
            target[columnName] = source[index];
        }


        public DataTable joinTables(DataTable t1, DataTable t2)
        {
            DataTable t = new DataTable();
            AddColumns(t1, t);
            AddColumns(t2, t);

            for (int i = 0; i < t1.Rows.Count; i++)
            {
                DataRow newRow = t.NewRow();

                for (int j = 0; j < t1.Columns.Count; j++)
                {
                    SetMergedRowValue(t1.Rows[i], newRow, j);
                    SetMergedRowValue(t2.Rows[i], newRow, j);
                }

                t.Rows.Add(newRow);
            }

            t.AcceptChanges();
            return t;
        }
    }
}
