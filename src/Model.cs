using System;
using System.Dynamic;

namespace PushToApi {

    public class EP {
        public string PathSuffix { get; set; }
        // HTTP Method: GET, POST, PUT, DELETE.
        public string Method {get; set;}
        public string ContentType { get; set; }
        public void Init() {
            if (string.IsNullOrEmpty(this.PathSuffix)) {
                throw new PushToApiException("EP PathSuffix must be set");
            }
            if (string.IsNullOrEmpty(this.Method)) {
                throw new PushToApiException("EP Method must be set");
            }
            if (string.IsNullOrEmpty(this.ContentType)) {
                this.ContentType = "application/json";
            }
        }
    }

    public class Model {
        public DateTime Now { get; set; }
        public Guid Id { get; set; }

        public Int32 RelationshipId { get; set; }

        public Int32 LoanNumber { get; set; }

        public String Description { get; set; }

        public String RecordedTimestamp {
            get {
                // need format 
                // 2020-06-24T00:07:19.417Z
                return Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }
        }

        public String MilestoneCurrentDateUtc {
            get {
                // 2020-06-24T00:06:47Z
                return Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
        }

        public EP EP { get; set; }

        public ExpandoObject Ext { get; set; }

        public void Init() {
            if (this.Now == DateTime.MinValue) {
                this.Now = DateTime.Now;
            }
            if (this.Id == Guid.Empty) {
                this.Id = Guid.NewGuid();
            }
            var rand = new Random();
            if (this.RelationshipId == 0) {
                this.RelationshipId = rand.Next();
            }
            if (this.LoanNumber == 0) {
                this.LoanNumber = rand.Next();
            }
            if (this.EP == null) {
                this.EP = new EP();
            }
            this.EP.Init();
        }
    }
}