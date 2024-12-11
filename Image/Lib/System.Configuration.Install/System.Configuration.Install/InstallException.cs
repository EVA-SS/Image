namespace System.Configuration.Install
{
    public class InstallException : SystemException
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Install.InstallException" /> class.</summary>
        public InstallException()
        {
            HResult = -2146232057;
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Install.InstallException" /> class, and specifies the message to display to the user.</summary>
        /// <param name="message">The message to display to the user. </param>
        public InstallException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Install.InstallException" /> class, and specifies the message to display to the user, and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The message to display to the user. </param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
        public InstallException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}