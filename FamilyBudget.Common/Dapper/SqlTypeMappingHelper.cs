using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FamilyBudget.Common.Dapper
{
    public static class SqlTypeMappingHelper<TTableType>
    {
        internal static TvpParam GetParam<T>(Expression<Func<TTableType, T>> expression)
        {
            SqlDbType MapType(Type t)
            {
                if (!SqlTypeMappingHelper.SqlTypeMapping.TryGetValue(t, out var result))
                    throw new ArgumentException("Type cannot be mapped to SqlDbType");
                return result;
            }

            var memberExpression = expression.Body as MemberExpression ?? throw new ArgumentException(nameof(expression));

            switch (memberExpression.Member)
            {
                case PropertyInfo p:
                    return new TvpParam(MapType(p.PropertyType), p.Name);
                case FieldInfo f:
                    return new TvpParam(MapType(f.FieldType), f.Name);
                default:
                    throw new ArgumentException("Expression must be a property or field expression.");
            }
        }
    }

    public static class SqlTypeMappingHelper
    {
        internal static readonly IDictionary<Type, SqlDbType> SqlTypeMapping = new Dictionary<Type, SqlDbType>
            {
                { typeof(byte), SqlDbType.TinyInt },
                { typeof(sbyte), SqlDbType.TinyInt },
                { typeof(short), SqlDbType.SmallInt },
                { typeof(ushort), SqlDbType.SmallInt },
                { typeof(int), SqlDbType.Int },
                { typeof(uint), SqlDbType.Int },
                { typeof(long), SqlDbType.BigInt },
                { typeof(ulong), SqlDbType.BigInt },
                { typeof(float), SqlDbType.Float },
                { typeof(double), SqlDbType.Float },
                { typeof(decimal), SqlDbType.Decimal },
                { typeof(bool), SqlDbType.Bit },
                { typeof(string), SqlDbType.VarChar },
                { typeof(char), SqlDbType.Char },
                { typeof(Guid), SqlDbType.UniqueIdentifier },
                { typeof(DateTime), SqlDbType.DateTime },
                { typeof(DateTimeOffset), SqlDbType.DateTimeOffset },
                { typeof(byte[]), SqlDbType.VarBinary },
                { typeof(byte?), SqlDbType.TinyInt },
                { typeof(sbyte?), SqlDbType.TinyInt },
                { typeof(short?), SqlDbType.SmallInt },
                { typeof(ushort?), SqlDbType.SmallInt },
                { typeof(int?), SqlDbType.Int },
                { typeof(uint?), SqlDbType.Int },
                { typeof(long?), SqlDbType.BigInt },
                { typeof(ulong?), SqlDbType.BigInt },
                { typeof(float?), SqlDbType.Float },
                { typeof(double?), SqlDbType.Float },
                { typeof(decimal?), SqlDbType.Decimal },
                { typeof(bool?), SqlDbType.Bit },
                { typeof(char?), SqlDbType.Char },
                { typeof(Guid?), SqlDbType.UniqueIdentifier },
                { typeof(DateTime?), SqlDbType.DateTime },
                { typeof(DateTimeOffset?), SqlDbType.DateTimeOffset },
                { typeof(IEnumerable<>), SqlDbType.Structured },
                { typeof(List<>), SqlDbType.Structured },
                { typeof(DataTable), SqlDbType.Structured }
            };
    }

}
