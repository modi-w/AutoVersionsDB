using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Helpers
{
    public static class ReflectionUtils
    {
        public static bool TryCopyPropertiesFromObjectToDataRow(object prmSource, DataRow dataRow)
        {
            bool isConverted = false;

            var targetProperties = TypeDescriptor.GetProperties(prmSource.GetType()).Cast<PropertyDescriptor>();

            foreach (DataColumn dCol in dataRow.Table.Columns)
            {
                var convertProperty = targetProperties.FirstOrDefault(prop => prop.Name == dCol.ColumnName);

                if (convertProperty != null)
                {
                    object val = null;

                    if (convertProperty.PropertyType == dCol.DataType
                        || (convertProperty.PropertyType.IsGenericType
                            && convertProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                            && convertProperty.PropertyType.GetGenericArguments()[0] == dCol.DataType))
                    {
                        val = convertProperty.GetValue(prmSource);

                        if (val == null)
                        {
                            val = DBNull.Value;
                        }

                        dataRow[dCol.ColumnName] = val;

                        isConverted = true;
                    }

                }
            }

            return isConverted;

        }

    }
}
