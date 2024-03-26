namespace NETCoreAPIConectaBarrio.Helpers
{
    public class SQLTypeConverter
    {
        public static string? ParseTypeToString(Type type, object value)
        {
            string? res = "";
            switch (type.Name)
            {
                case "Int32":
                case "Double":
                case "Float":
                    res = value.ToString();
                    break;
                case "DateTime":
                    res = "'" + String.Format("{0:yyyy-M-d HH:mm:ss}", value) + "'";
                    break;
                case "DateOnly":
                    res = "'" + String.Format("{0:yyyy-M-d}", value) + "'";
                    break;
                case "TimeOnly":
                    res = "'" + String.Format("{0:HH:mm:ss}", value) + "'";
                    break;
                case "String":
                    res = "'" + value.ToString() + "'";
                    break;
                default:
                    res = "'" + value.ToString() + "'";
                    break;
            }
            return res;
        }
    }
}
