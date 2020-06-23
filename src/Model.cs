using System;
using System.Dynamic;

namespace PushToApi
{
    public class Model
    {
        public DateTime Now {get;set;}
        public Guid Uuid {get;set;}

        public String NowUtc {get {
            return Now.ToString();
        }}

        public ExpandoObject Ext {get;set;}

        public void Init() {
            if (this.Now == DateTime.MinValue) {
                this.Now = DateTime.Now;
            }
            if (this.Uuid == Guid.Empty) {
                this.Uuid = Guid.NewGuid();
            }
        }
    }
}