using System.Data;
using LogBg3Armory.Models;

namespace LogBg3Armory.Repositories
{
    public class ItemRepository
    {
        private readonly IDbConnection _connection;

        public ItemRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<Item> GetAllItems()
        {
            var items = new List<Item>();

            using var command = _connection.CreateCommand();
            command.CommandText = @"
    SELECT i.id, i.name,
           t.type_name,
           r.rarity_name,
           p.property_name,
           l.location_name,
           ac.name AS act_name,
           d.text AS description
    FROM items i
    LEFT JOIN types t ON i.type_id = t.id
    LEFT JOIN rarities r ON i.rarity_id = r.id
    LEFT JOIN properties p ON i.property_id = p.id
    LEFT JOIN locations l ON i.location_id = l.id
    LEFT JOIN acts ac ON i.act_id = ac.id
    LEFT JOIN descriptions d ON i.description_id = d.id";

            _connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new Item
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Type = new ItemType
                    {
                        TypeName = reader.IsDBNull(reader.GetOrdinal("type_name")) ? null :
                            reader.GetString(reader.GetOrdinal("type_name"))
                    },
                    Rarity = new Rarity
                    {
                        RarityName = reader.IsDBNull(reader.GetOrdinal("rarity_name")) ? null :
                            reader.GetString(reader.GetOrdinal("rarity_name"))
                    },
                    Property = new Property
                    {
                        PropertyName = reader.IsDBNull(reader.GetOrdinal("property_name")) ? null :
                            reader.GetString(reader.GetOrdinal("property_name"))
                    },
                    Location = new Location
                    {
                        LocationName = reader.IsDBNull(reader.GetOrdinal("location_name")) ? null :
                            reader.GetString(reader.GetOrdinal("location_name"))
                    },
                    Act = new Act
                    {
                        ActName = reader.IsDBNull(reader.GetOrdinal("act_name")) ? null :
                            reader.GetString(reader.GetOrdinal("act_name"))
                    },
                    Description = new Description
                    {
                        Text = reader.IsDBNull(reader.GetOrdinal("description")) ? null :
                            reader.GetString(reader.GetOrdinal("description"))
                    }
                };

                items.Add(item);
            }

            return items;
        }
        public Item? GetItemById(int id)
{
    using var command = _connection.CreateCommand();
    command.CommandText = @"
        SELECT i.id, i.name,
               t.type_name,
               r.rarity_name,
               p.property_name,
               l.location_name,
               ac.name AS act_name,
               d.text AS description
        FROM items i
        LEFT JOIN types t ON i.type_id = t.id
        LEFT JOIN rarities r ON i.rarity_id = r.id
        LEFT JOIN properties p ON i.property_id = p.id
        LEFT JOIN locations l ON i.location_id = l.id
        LEFT JOIN acts ac ON i.act_id = ac.id
        LEFT JOIN descriptions d ON i.description_id = d.id
        WHERE i.id = @id";

    var parameter = command.CreateParameter();
    parameter.ParameterName = "@id";
    parameter.Value = id;
    command.Parameters.Add(parameter);

    _connection.Open();
    using var reader = command.ExecuteReader();

    if (reader.Read())
    {
        return new Item
        {
            Id = reader.GetInt32(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Type = new ItemType
            {
                TypeName = reader.IsDBNull(reader.GetOrdinal("type_name")) ? null :
                    reader.GetString(reader.GetOrdinal("type_name"))
            },
            Rarity = new Rarity
            {
                RarityName = reader.IsDBNull(reader.GetOrdinal("rarity_name")) ? null :
                    reader.GetString(reader.GetOrdinal("rarity_name"))
            },
            Property = new Property
            {
                PropertyName = reader.IsDBNull(reader.GetOrdinal("property_name")) ? null :
                    reader.GetString(reader.GetOrdinal("property_name"))
            },
            Location = new Location
            {
                LocationName = reader.IsDBNull(reader.GetOrdinal("location_name")) ? null :
                    reader.GetString(reader.GetOrdinal("location_name"))
            },
            Act = new Act
            {
                ActName = reader.IsDBNull(reader.GetOrdinal("act_name")) ? null :
                    reader.GetString(reader.GetOrdinal("act_name"))
            },
            Description = new Description
            {
                Text = reader.IsDBNull(reader.GetOrdinal("description")) ? null :
                    reader.GetString(reader.GetOrdinal("description"))
            }
        };
    }

    return null;
}
    }
}