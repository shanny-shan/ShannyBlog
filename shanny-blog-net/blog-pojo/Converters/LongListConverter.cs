using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;


namespace blog_pojo.Converters
{
    public class LongListConverter : ValueConverter<List<long>, string>
    {
        public LongListConverter() : base(
            attribute => ConvertToDbColumn(attribute), 
            dbData => ConvertToEntityAttr(dbData)){}

        private static string ConvertToDbColumn(List<long> attribute)
        {
            try
            {
                return JsonConvert.SerializeObject(attribute);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private static List<long> ConvertToEntityAttr(string dbData)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<long>>(dbData)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
