namespace OrderManagementApplication.Mapper
{
    public class MapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(MappingProfile),
            };
        }
    }
}
