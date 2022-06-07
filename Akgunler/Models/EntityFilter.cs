using System;
using System.Collections.Generic;

namespace Akgunler.Models
{
	public enum FilterOperation
	{
		Equal = 0,
		NotEqual = 1,
		LessThan = 2,
		LessThanOrEqual = 3,
		GreaterThan = 4,
		GreaterThanOrEqual = 5,
		Between = 6,
		Contains = 7,
		EndsWith = 8,
		NotContains = 9,
		StartsWith = 10
	}

	public class FilterOptions
	{
		public bool RequireTotalCount = false;
		public int Skip = 0;
		public int Take = 100;
		public IList<EntityFilter> Filters = null;
		public string PrimaryKey = "Id";
		public string DefaultSort => PrimaryKey;
	}

	//public class FilterResult
	//{
	//	public IList<T> Data;
	//	public int TotalCount;
	//}

	public class EntityFilter
	{
		public string Selector;
		public FilterOperation Operation;
		public object Value;
		public object Value2;

		public string ToSql()
		{
			if (Operation == FilterOperation.Equal)
			{
				return Selector + " = " + GetVal(Value);
			}
			else if (Operation == FilterOperation.NotEqual)
			{
				return Selector + " <> " + GetVal(Value);
			}
			else if (Operation == FilterOperation.LessThan)
			{
				return Selector + " < " + GetVal(Value);
			}
			else if (Operation == FilterOperation.LessThanOrEqual)
			{
				return Selector + " <= " + GetVal(Value);
			}
			else if (Operation == FilterOperation.GreaterThan)
			{
				return Selector + " > " + GetVal(Value);
			}
			else if (Operation == FilterOperation.GreaterThanOrEqual)
			{
				return Selector + " >= " + GetVal(Value);
			}
			else if (Operation == FilterOperation.Between)
			{
				return Selector + " BETWEEN " + GetVal(Value) + " AND " + GetVal(Value2);
			}
			else if (Operation == FilterOperation.Contains)
			{
				return Selector + " LIKE '%" + Value.ToString() + "%'";
			}
			else if (Operation == FilterOperation.NotContains)
			{
				return Selector + " NOT LIKE '%" + Value.ToString() + "%'";
			}
			else if (Operation == FilterOperation.StartsWith)
			{
				return Selector + " LIKE '%" + Value.ToString() + "'";
			}
			else if (Operation == FilterOperation.EndsWith)
			{
				return Selector + " LIKE '" + Value.ToString() + "%'";
			}

			return "";
		}

		private string GetVal(object value)
		{
			if (value.GetType() == typeof(string))
			{
				return "'" + value + "'";
			}
			else if (value.GetType() == typeof(DateTime))
			{
				return "'" + ((DateTime)value).ToString("yyyy-MM-dd") + "'";
			}
			else if (IsNumeric(value))
			{
				return value.ToString();
			}
			return "";
		}

		private bool IsNumeric(object value)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}
		}
	}
}