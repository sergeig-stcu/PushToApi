namespace PushToApi {
    [System.Serializable]
    public class PushToApiException : System.Exception
    {
        public PushToApiException() { }
        public PushToApiException(string message) : base(message) { }
        public PushToApiException(string message, System.Exception inner) : base(message, inner) { }
        protected PushToApiException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
} // end of namespace