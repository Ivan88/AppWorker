using MessageBus;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppWorker
{
	public class Program
	{
		private static readonly MessageBrocker _messageBrocker = new MessageBrocker("localhost");

		private static readonly List<string> docs = new List<string>();

		static void Main(string[] args)
		{
			_messageBrocker.SubscribeForReplying("getDocumentsQueue", HandleSending);
			_messageBrocker.Subscribe("documentsQueue", HandleReceiving);
		}

		private static void HandleReceiving(MessageWrapper messageWrapper)
		{
			docs.Add(messageWrapper.Headers["filename"].ToString());
			//send to elastic search
		}

		private static byte[] HandleSending(MessageWrapper messageWrapper)
		{
			//get from elastic search
			var doc = docs.FirstOrDefault(x => x == messageWrapper.Headers["filename"].ToString());
			return Encoding.UTF8.GetBytes(doc);
		}
	}
}
