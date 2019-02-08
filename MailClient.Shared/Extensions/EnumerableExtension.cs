using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared.Extensions
{
	public static class EnumerableExtension
	{
		public static DataTable ToDataTable<T>(this IEnumerable<T> pCollection) {
			
			//se obtiene el tipo base
			Type bType = typeof(T);
			//se obtienen las propiedades
			PropertyInfo[] bProperties = bType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			//se obtienen las columnas por cada propiedad
			DataColumn[] bColumns = bProperties
					.Select(bProperty => new DataColumn(bProperty.Name, bProperty.PropertyType))
					.ToArray();
			
			DataTable mDataTable = new DataTable(bType.Name);
			mDataTable.Columns.AddRange(bColumns);

			foreach (T item in pCollection)
			{
				object[] mValues = bProperties
					.Select(bProperty => bProperty.GetValue(item, null))
					.ToArray();

				mDataTable.Rows.Add(mValues);
			}

			return mDataTable;
		}
	}
}
