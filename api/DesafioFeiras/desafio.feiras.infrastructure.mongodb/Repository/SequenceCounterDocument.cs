using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.feiras.infrastructure.mongodb.Repository
{
    
    internal class SequenceCounterDocument
    {
        [BsonId]
        public string Key { get; set; }

        public int Sequence { get; set; }
    }
}
