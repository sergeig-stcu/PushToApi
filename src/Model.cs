using System;

namespace PushToApi
{
    public class Model
    {
        public DateTime Now {get;set;}
        public Guid Uuid {get;set;}

        public String NowUtc {get {
            return Now.ToString();
        }}

        public dynamic Ext {get;set;}
    }
}