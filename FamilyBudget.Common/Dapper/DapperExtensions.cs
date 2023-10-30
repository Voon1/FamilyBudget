using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.SqlServer.Server;


namespace FamilyBudget.Common.Dapper
{

    public static class DapperExtensions
    {
        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> enumerable,
            IEnumerable<TvpParam> columnsDefinition) => AsTableValuedParameter(enumerable,
                (typeof(T).GetCustomAttribute(typeof(TableTypeAttribute)) as TableTypeAttribute
                 ?? throw new ArgumentException(
                     $"Type {typeof(T)} has no attribute of type {typeof(TableTypeAttribute)}")
                ).SqlTypeName, columnsDefinition);


        public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> enumerable, string typeName, IEnumerable<TvpParam> columnsDefinition)
        {
            var columns = columnsDefinition as TvpParam[] ?? columnsDefinition?.ToArray();
            if (columns == null || !columns.Any())
                throw new ArgumentException(nameof(columnsDefinition));

            var data = enumerable as T[] ?? enumerable?.ToArray();
            if (data == null || !data.Any())
                return new IgnoredQueryParameter();

            var isSimpleType = typeof(T).IsValueType || typeof(T) == typeof(string);

            var properties = columns.Select(x =>
            {
                var metadata = x.GetMetaData();

                if (String.IsNullOrEmpty(x.PropertyName))
                {
                    if (!isSimpleType)
                        throw new DapperExtensionException("Property name must be specified for a reference-type-based collection for a Table-Valued Parameter.");
                }
                else if (isSimpleType)
                    throw new DapperExtensionException("Property name must not be specified for a value-type-based collection for a Table-Valued Parameter.");

                var property = !String.IsNullOrEmpty(x.PropertyName) ? typeof(T).GetProperty(x.PropertyName) : null;

                if (property == null && !isSimpleType)
                    throw new DapperExtensionException($"Invalid object property name: {x.PropertyName}, unable to create TVP.");

                return Tuple.Create(property, metadata);
            }).ToArray();

            var sqlMetaData = properties.Select(x => x.Item2)
                                        .ToArray();

            var items = data.Select(x =>
            {
                var sqlDataElem = new SqlDataRecord(sqlMetaData);

                var values = properties.Select(propertyInfo => propertyInfo.Item1 != null ? propertyInfo.Item1.GetValue(x, null) : x)
                                       .ToArray();

                sqlDataElem.SetValues(values);

                return sqlDataElem;
            });

            return items.AsTableValuedParameter(typeName);
        }
    }
}

