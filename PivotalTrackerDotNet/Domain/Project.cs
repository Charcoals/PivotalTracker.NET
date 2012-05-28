using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerDotNet.Domain {
    public class Project {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IterationLength { get; set; }
        public DayOfWeek WeekStartDay { get; set; }
        public List<Membership> Memberships { get; set; }
        public string VelocityScheme { get; set; }
        public int CurrentVelocity { get; set; }
        public string LastActivityAt { get; set; }
        //        <project>
        //  <id>1</id>
        //  <name>Sample Project</name>
        //  <iteration_length type="integer">2</iteration_length>
        //  <week_start_day>Monday</week_start_day>
        //  <point_scale>0,1,2,3</point_scale>
        //  <velocity_scheme>Average of 4 iterations</velocity_scheme>
        //  <current_velocity>10</current_velocity>
        //  <initial_velocity>10</initial_velocity>
        //  <number_of_done_iterations_to_show>12</number_of_done_iterations_to_show>
        //  <labels>shields,transporter</labels>
        //  <allow_attachments>true</allow_attachments>
        //  <public>false</public>
        //  <use_https>true</use_https>
        //  <bugs_and_chores_are_estimatable>false</bugs_and_chores_are_estimatable>
        //  <commit_mode>false</commit_mode>
        //  <last_activity_at type="datetime">2010/01/16 17:39:10 CST</last_activity_at>
        //  <memberships type="array">
        //    <membership>
        //      <id>1006</id>
        //      <person>
        //        <email>kirkybaby@earth.ufp</email>
        //        <name>James T. Kirk</name>
        //        <initials>JTK</initials>
        //      </person>
        //      <role>Owner</role>
        //    </membership>
        //  </memberships>
        //  <integrations type="array">
        //    <integration>
        //      <id type="integer">3</id>
        //      <type>Other</type>
        //      <name>United Federation of Planets Bug Tracker</name>
        //      <field_name>other_id</field_name>
        //      <field_label>United Federation of Planets Bug Tracker Id</field_label>
        //      <active>true</active>
        //    </integration>
        //  </integrations>
        //</project>
    }
}
