using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.SqlServer.Server;

namespace FamilyBudget.Common.Dapper
{
    /// <summary>
    /// Stores information about a Table-Valued Type parameter.
    /// </summary>
    public class TvpParam
    {

        #region properties

        /// <summary>
        /// Name of the SQL parameter's column.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Name of the object's property that stores the value.
        /// </summary>
        public string PropertyName
        {
            get { return this.propertyName ?? this.ColumnName; }
            set { this.propertyName = value; }
        }

        /// <summary>
        /// Database type for the parameter.
        /// </summary>
        public SqlDbType SqlType { get; set; }

        /// <summary>
        /// Maximum length for character types.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Precision for numeric types.
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Scale for numeric types.
        /// </summary>
        public byte Scale { get; set; }

        #endregion properties

        #region fields

        /// <summary>
        /// Name of the object property that stores the value.
        /// If it's <c>null</c>, <see cref="ColumnName"/> is used.
        /// </summary>
        private string propertyName;

        /// <summary>
        /// Whether this type uses <see cref="MaxLength"/>.
        /// </summary>
        private bool requiresLength;

        /// <summary>
        /// Whether this type uses <see cref="Precision"/> and <see cref="Scale"/>.
        /// </summary>
        private bool requiresPrecision;

        /// <summary>
        /// Maximum length of ANSI character column.
        /// </summary>
        private const int maxAnsiLength = 8000;

        /// <summary>
        /// Maximum length of Unicode character column.
        /// </summary>
        private const int maxUnicodeLength = 4000;

        /// <summary>
        /// Maximum precision for Decimals.
        /// </summary>
        private const byte maxDecimalPrecision = 38;

        /// <summary>
        /// Default scale for Decimals.
        /// </summary>
        private const byte defaultDecimalScale = 14;

        #endregion properties

        #region constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlType">
        ///     Database type for the parameter.
        ///     For decimals, precision of 38 and scale of 14 is used.
        ///     For character types, the maximm length is used (4000 for Unicode types and 8000 for ANSI types).
        /// </param>
        /// <param name="propertyName">
        ///     Name of the SQL parameter's column and the object's property that stores the value.
        ///     Omit when the value itself must be passed to the database instead of its property.
        /// </param>
        public TvpParam(SqlDbType sqlType, string propertyName = null)
        {
            switch (sqlType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    this.requiresLength = true;
                    this.MaxLength = maxAnsiLength;
                    break;

                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                    this.requiresLength = true;
                    this.MaxLength = maxUnicodeLength;
                    break;

                case SqlDbType.Decimal:
                    this.requiresPrecision = true;
                    this.Precision = maxDecimalPrecision;
                    this.Scale = defaultDecimalScale;
                    break;

                case SqlDbType.Structured:
                case SqlDbType.Udt:
                    throw new ArgumentException($"Type ‘{sqlType}’ is not supported.", nameof(sqlType));
            }

            this.ColumnName = propertyName;
            this.SqlType = sqlType;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlType">Database type for the parameter. It must be a character or binary type.</param>
        /// <param name="maxLength">Maximum length for the type. Use <c>-1</c> for MAX.</param>
        /// <param name="propertyName">
        ///     Name of the SQL parameter's column and the object's property that stores the value.
        ///     Omit when the value itself must be passed to the database instead of its property.
        /// </param>
        public TvpParam(SqlDbType sqlType, int maxLength, string propertyName = null)
        {
            switch (sqlType)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    this.requiresLength = true;
                    this.MaxLength = maxLength;
                    break;

                default:
                    throw new ArgumentException($"Type ‘{sqlType}’ does not support length.", nameof(sqlType));
            }

            this.ColumnName = propertyName;
            this.SqlType = sqlType;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sqlType">Database type for the parameter. It must be the decimal type.</param>
        /// <param name="precision">Precision for the type.</param>
        /// <param name="scale">Scale for the type.</param>
        /// <param name="propertyName">
        ///     Name of the SQL parameter's column and the object's property that stores the value.
        ///     Omit when the value itself must be passed to the database instead of its property.
        /// </param>
        public TvpParam(SqlDbType sqlType, byte precision, byte scale, string propertyName = null)
        {
            switch (sqlType)
            {
                case SqlDbType.Decimal:
                    this.requiresPrecision = true;
                    this.Precision = precision;
                    this.Scale = scale;
                    break;

                default:
                    throw new ArgumentException($"Type ‘{sqlType}’ does not support precision.", nameof(sqlType));
            }

            this.ColumnName = propertyName;
            this.SqlType = sqlType;
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// Sets the property name to be different from the column name.
        /// </summary>
        /// <param name="property">Name of the object's property that stores the value.</param>
        /// <returns>Reference to this object to allow chaining.</returns>
        public TvpParam From(string property)
        {
            this.propertyName = property;
            return this;
        }

        /// <summary>
        /// Returns an instance of SqlMetaData based on the values in this object.
        /// </summary>
        /// <returns>Instance of meta data.</returns>
        public SqlMetaData GetMetaData()
        {
            if (this.requiresLength)
                return new SqlMetaData(this.ColumnName ?? String.Empty, this.SqlType, this.MaxLength);
            else if (this.requiresPrecision)
                return new SqlMetaData(this.ColumnName ?? String.Empty, this.SqlType, this.Precision, this.Scale);
            else
                return new SqlMetaData(this.ColumnName ?? String.Empty, this.SqlType);
        }

        #endregion methods
    }
}
