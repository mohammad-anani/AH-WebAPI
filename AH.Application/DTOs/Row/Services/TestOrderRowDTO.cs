using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Application.DTOs.Row
{
    public class TestOrderRowDTO
    {
        public int ID { get; set; }

        public string PatientFullName { get; set; }

        public string TestName { get; set; }

        public TestOrderRowDTO(int id, string patientFullName, string testName)
        {
            ID = id;
            PatientFullName = patientFullName;
            TestName = testName;
        }

        public TestOrderRowDTO()
        {
            ID = -1;
            PatientFullName = string.Empty;
            TestName = string.Empty;
        }

    }
}