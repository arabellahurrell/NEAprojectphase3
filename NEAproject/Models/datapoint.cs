using System;
using System.Runtime.Serialization;
//using System.Runtime.Serialization.DataMemberAttribute;
// use a defined class to hod the json data for the graph 

namespace NEAproject.Models
{
    [DataContract]
    //indicates the contact between the class and the json data e
    public class datapoint
        
    {
        [DataMember(Name = "label")]
        //data annotation for member 
        public string label ;
        [DataMember(Name = "y")]
        //data annotation for member
        public double y;

        public datapoint(string inputlabel, double inputy)
            //parameterised constructer 
        {
            label = inputlabel;
            y = inputy;

        }
    }
}