using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
	public class Person {
		public string Email { get; set; }
		public string Name { get; set; }
		public string Initials { get; set; }
		//<membership>
		//  <id>1</id>
		//  <person>
		//    <email>picard@earth.ufp</email>
		//    <name>Jean-Luc Picard</name>
		//    <initials>jlp</initials>
		//  </person>
		//  <role>Owner</role>
	}
}
